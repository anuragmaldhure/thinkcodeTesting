
using FluentValidation;

namespace thinkbridge.Grp2BackendAN.Services.Services;
public class ToDoMasterService : IToDoMasterService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public ToDoMasterService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allToDoMasters or fetchesToDoMasters with pagination based on requestDto
     public async Task<ListResponse<ToDoMasterResDto>> GetAll(GetAllToDoMasterReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<ToDoMaster>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<ToDoMaster, GetAllToDoMasterReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<ToDoMasterResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<ToDoMasterResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new ToDoMaster to the database
     public async Task<SingleResponse<ToDoMasterResDto>> Save(AddToDoMasterReqDto requestDto)
     {
        var entity = _mapper.Map<ToDoMaster>(requestDto); // Maps the request DTO to the ToDoMaster entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<ToDoMasterResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<ToDoMasterResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing ToDoMaster in the database
     public async Task<SingleResponse<ToDoMasterResDto>> Update(UpdateToDoMasterReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<ToDoMaster,int>(requestDto.ToDoTaskId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.ToDoTaskId)); // Fetches the existing ToDoMaster by ToDoTaskId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<ToDoMasterResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<ToDoMasterResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a ToDoMaster by ToDoTaskId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int ToDoTaskId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<ToDoMaster>(x => x.ToDoTaskId == ToDoTaskId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ToDoTaskId));// Fetches the  ToDoMaster by ToDoTaskId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<ToDoMasterResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<ToDoMasterResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  ToDoMaster record
     public async Task<BaseResponse> Delete(int ToDoTaskId)
     {
        var existing = await _baseRepository.GetByIdAsync<ToDoMaster, int>(ToDoTaskId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ToDoTaskId)); 
        // This record will be permanently deleted from the database and cannot be recovered.
        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }


    /// <summary>
    /// Marks a ToDo task as deleted.
    /// </summary>
    /// <param name="toDoTaskId">The ID of the ToDo task to mark as deleted.</param>
    /// <param name="cancellationToken">Token for cancelling the operation.</param>
    /// <returns>The status of the operation wrapped in a SingleResponse object.</returns>
    public async Task<SingleResponse<string>> MarkAsDeleted(int toDoTaskId, CancellationToken cancellationToken)
    {
        if (toDoTaskId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(toDoTaskId), "Task ID must be greater than zero.");
        }

        try
        {
            var toDoTask = await _dbContext.ToDoMasters
                .Where(t => t.ToDoTaskId == toDoTaskId)
                .FirstOrDefaultAsync(cancellationToken);

            if (toDoTask == null)
            {
                return new SingleResponse<string>
                {
                    Status = HttpStatusCode.NotFound,
                    Messages = new List<ResponseMessage> { new ResponseMessage { Message = "ToDo task not found." } }
                };
            }

            toDoTask.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SingleResponse<string>
            {
                Data = "ToDo task marked as deleted.",
                Status = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            // Log exception here
            return new SingleResponse<string>
            {
                Status = HttpStatusCode.InternalServerError,
                Messages = new List<ResponseMessage> { new ResponseMessage { Message = "An error occurred while processing your request." } }
            };
        }
    }


  

        /// <summary>
        /// Marks a task as completed if applicable.
        /// </summary>
        /// <param name="toDoTaskId">The ID of the task to be marked complete.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A single response containing the result of the operation.</returns>
        /// <exception cref="ArgumentException">Thrown when toDoTaskId is less than or equal to zero.</exception>
        public async Task<SingleResponse<MarkTaskCompletedDTO>> MarkTaskCompleted(int toDoTaskId, CancellationToken cancellationToken)
        {
            if (toDoTaskId <= 0)
            {
                throw new ArgumentException("Task ID must be greater than zero.", nameof(toDoTaskId));
            }

            var toDoMaster = await _dbContext.ToDoMasters
                                             .Where(tm => tm.ToDoTaskId == toDoTaskId && !tm.IsDeleted)
                                             .FirstOrDefaultAsync(cancellationToken);

            if (toDoMaster == null)
            {
                return new SingleResponse<MarkTaskCompletedDTO>
                {
                    Status = HttpStatusCode.NotFound,
                    Messages = new List<ResponseMessage> { new ResponseMessage { Message = "Task not found." } }
                };
            }

            if (toDoMaster.HasChecklist)
            {
                var allChecklistCompleted = await _dbContext.ToDoChecklists
                                                            .Where(tc => tc.ToDoTaskId == toDoTaskId && !tc.IsDeleted)
                                                            .AllAsync(tc => tc.IsComplete, cancellationToken);

                if (!allChecklistCompleted)
                {
                    return new SingleResponse<MarkTaskCompletedDTO>
                    {
                        Status = HttpStatusCode.BadRequest,
                        Messages = new List<ResponseMessage> { new ResponseMessage { Message = "All checklist items must be completed." } }
                    };
                }
            }

            toDoMaster.IsComplete = true;
            toDoMaster.CompletedDate = DateTime.UtcNow;
            toDoMaster.PercentageCompleted = 100;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SingleResponse<MarkTaskCompletedDTO>
            {
                Data = new MarkTaskCompletedDTO { ToDoTaskId = toDoTaskId, IsComplete = toDoMaster.IsComplete },
                Status = HttpStatusCode.OK
            };
        }


    /// <summary>
    /// Updates a ToDo Master task.
    /// </summary>
    /// <param name="toDoTaskId">Task ID to be updated.</param>
    /// <param name="updateToDoMasterDTO">New data for the task.</param>
    /// <param name="cancellationToken">Cancellation token for async operations.</param>
    /// <returns>SingleResponse with updated data or error information.</returns>
    public async Task<SingleResponse<UpdateToDoMasterDTO>> UpdateToDoMaster(int toDoTaskId, UpdateToDoMasterDTO updateToDoMasterDTO, CancellationToken cancellationToken)
    {
        if (updateToDoMasterDTO == null)
        {
            throw new ArgumentNullException(nameof(updateToDoMasterDTO));
        }

        var validator = new UpdateToDoMasterDTOValidator();
        var validationResult = validator.Validate(updateToDoMasterDTO);

        if (!validationResult.IsValid)
        {
            return new SingleResponse<UpdateToDoMasterDTO>
            {
                Status = HttpStatusCode.BadRequest,
                Messages = validationResult.Errors.Select(e => new ResponseMessage { Message = e.ErrorMessage }).ToList()
            };
        }

        var toDoMaster = await _dbContext.ToDoMasters.FindAsync(new object[] { toDoTaskId }, cancellationToken);

        if (toDoMaster == null || toDoMaster.IsDeleted)
        {
            return new SingleResponse<UpdateToDoMasterDTO>
            {
                Status = HttpStatusCode.NotFound,
                Messages = new List<ResponseMessage> { new ResponseMessage { Message = "ToDoMaster not found." } }
            };
        }

        toDoMaster.AssignedToId = updateToDoMasterDTO.AssignedToId;
        toDoMaster.IsAssignedToTeam = updateToDoMasterDTO.IsAssignedToTeam;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new SingleResponse<UpdateToDoMasterDTO>
        {
            Data = updateToDoMasterDTO,
            Status = HttpStatusCode.OK
        };
    }


    public class UpdateToDoMasterDTOValidator : AbstractValidator<UpdateToDoMasterDTO>
    {
        public UpdateToDoMasterDTOValidator()
        {
            RuleFor(x => x.AssignedToId)
                .NotEmpty().WithMessage("AssignedToId is required.")
                .MaximumLength(450).WithMessage("AssignedToId cannot exceed 450 characters.")
                .Must((dto, assignedToId) =>
                {
                    if (dto.IsAssignedToTeam)
                    {
                        return int.TryParse(assignedToId, out _);
                    }
                    return true; // Valid if not assigned to a team.
                }).WithMessage("AssignedToId must be an integer when IsAssignedToTeam is true.");

            RuleFor(x => x.IsAssignedToTeam)
                .NotNull().WithMessage("IsAssignedToTeam is required.");
        }
    }



    //Report 1

    /// <summary>
    /// Retrieves a list of overdue tasks based on the given criteria.
    /// </summary>
    /// <param name="userId">ID of the user to filter tasks by. Null or empty to ignore filter.</param>
    /// <param name="startDate">Start date for filtering tasks. Null to ignore filter.</param>
    /// <param name="endDate">End date for filtering tasks. Null to ignore filter.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A list of overdue tasks as a <see cref="ListResponse{ToDoMasterDto}"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when userId is not a valid user id.</exception>
    public async Task<ListResponse<ToDoMasterDto>> GetOverdueTasks(string? userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId) && startDate == null && endDate == null)
            throw new ArgumentException("At least one filtering parameter must be provided.");

        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var query = _dbContext.ToDoMasters
                                    .Where(t => !t.IsComplete && t.DueDate.HasValue && t.DueDate.Value < DateTime.UtcNow && !t.IsDeleted);

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(t => t.AssignedToId == userId);
            }

            if (startDate.HasValue)
            {
                query = query.Where(t => t.DueDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.DueDate <= endDate.Value);
            }

            if (string.IsNullOrEmpty(userId))
            {
                query = query.OrderBy(t => t.AssignedToId);
            }

            var overdueTasks = await query.Select(t => new ToDoMasterDto
            {
                ToDoTaskId = t.ToDoTaskId,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                AssignedToId = t.AssignedToId
            }).ToListAsync(cancellationToken);

            return new ListResponse<ToDoMasterDto> { Data = overdueTasks };
        }
        catch (Exception ex)
        {
            // Log exception here
            throw new Exception("An error occurred while retrieving overdue tasks.", ex);
        }
    }


    //Report 4



    /// <summary>
    /// Retrieves the number of tasks completed by each user in the past 7 days.
    /// </summary>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <returns>A list response containing task completion count per user.</returns>
    public async Task<ListResponse<TaskCompletionPerUserDto>> GetTasksCompletedLast7DaysPerUser(CancellationToken cancellationToken)
    {
        if (cancellationToken == null) throw new ArgumentNullException(nameof(cancellationToken));

        try
        {
            var sevenDaysAgo = DateTime.UtcNow.AddDays(-7).Date;
            var yesterday = DateTime.UtcNow.Date;

            var completedTasks = await _dbContext.ToDoMasters
                .Where(t => t.CompletedDate.HasValue && t.CompletedDate.Value >= sevenDaysAgo && t.CompletedDate.Value < yesterday && !t.IsDeleted)
                .GroupBy(t => t.AssignedToId)
                .Select(userGroup => new TaskCompletionPerUserDto
                {
                    UserId = userGroup.Key,
                    CompletedTaskCount = userGroup.Count(),
                    UserNameOrTeamName = _dbContext.AspNetUsers
                        .Where(u => u.Id == userGroup.Key)
                        .Select(u => u.FirstName + " " + u.LastName)
                        .FirstOrDefault() ?? _dbContext.Teams
                        .Where(tm => tm.TeamId.ToString() == userGroup.Key.ToString())
                        .Select(tm => tm.TeamName)
                        .FirstOrDefault() ?? "Unknown"
                })
                .ToListAsync(cancellationToken);

            return new ListResponse<TaskCompletionPerUserDto> { Data = completedTasks };
        }
        catch (Exception ex)
        {
            // Log exception here as per the logging strategy
            throw new ApplicationException("An error occurred while retrieving completed tasks.", ex);
        }
    }


}




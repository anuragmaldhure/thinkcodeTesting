
namespace thinkbridge.Grp2BackendAN.Services.Services;
public class ToDoChecklistService : IToDoChecklistService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public ToDoChecklistService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allToDoChecklists or fetchesToDoChecklists with pagination based on requestDto
     public async Task<ListResponse<ToDoChecklistResDto>> GetAll(GetAllToDoChecklistReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<ToDoChecklist>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<ToDoChecklist, GetAllToDoChecklistReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<ToDoChecklistResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<ToDoChecklistResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new ToDoChecklist to the database
     public async Task<SingleResponse<ToDoChecklistResDto>> Save(AddToDoChecklistReqDto requestDto)
     {
        var entity = _mapper.Map<ToDoChecklist>(requestDto); // Maps the request DTO to the ToDoChecklist entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<ToDoChecklistResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<ToDoChecklistResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing ToDoChecklist in the database
     public async Task<SingleResponse<ToDoChecklistResDto>> Update(UpdateToDoChecklistReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<ToDoChecklist,int>(requestDto.ToDoChecklistId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.ToDoChecklistId)); // Fetches the existing ToDoChecklist by ToDoChecklistId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<ToDoChecklistResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<ToDoChecklistResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a ToDoChecklist by ToDoChecklistId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int ToDoChecklistId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<ToDoChecklist>(x => x.ToDoChecklistId == ToDoChecklistId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ToDoChecklistId));// Fetches the  ToDoChecklist by ToDoChecklistId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<ToDoChecklistResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<ToDoChecklistResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  ToDoChecklist record
     public async Task<BaseResponse> Delete(int ToDoChecklistId)
     {
        var existing = await _baseRepository.GetByIdAsync<ToDoChecklist, int>(ToDoChecklistId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ToDoChecklistId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }


    // Added this service
    /// <summary>
    /// Marks a single checklist item as completed.
    /// </summary>
    /// <param name="toDoChecklistId">ID of the ToDo checklist to mark as completed.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Response indicating the success status and result of the operation.</returns>
    /// <exception cref="ArgumentException">Thrown if the toDoChecklistId is less than or equal to zero.</exception>
    public async Task<SingleResponse<MarkSingleChecklistCompletedDTO>> MarkSingleChecklistCompleted(int toDoChecklistId, CancellationToken cancellationToken)
    {
        if (toDoChecklistId <= 0)
        {
            throw new ArgumentException("Invalid checklist Id.", nameof(toDoChecklistId));
        }

        try
        {
            var checklistEntry = await _dbContext.ToDoChecklists
                .FirstOrDefaultAsync(tc => tc.ToDoChecklistId == toDoChecklistId && !tc.IsDeleted, cancellationToken);

            if (checklistEntry == null)
            {
                return new SingleResponse<MarkSingleChecklistCompletedDTO>
                {
                    Status = HttpStatusCode.NotFound,
                    Messages = new List<ResponseMessage> { new ResponseMessage { Message = "Checklist entry not found." } }
                };
            }

            var toDoMaster = await _dbContext.ToDoMasters
                .FirstOrDefaultAsync(tm => tm.ToDoTaskId == checklistEntry.ToDoTaskId && !tm.IsDeleted && !tm.IsComplete, cancellationToken);

            if (toDoMaster == null)
            {
                return new SingleResponse<MarkSingleChecklistCompletedDTO>
                {
                    Status = HttpStatusCode.BadRequest,
                    Messages = new List<ResponseMessage> { new ResponseMessage { Message = "Task is either completed or deleted." } }
                };
            }

            checklistEntry.IsComplete = true;
            checklistEntry.UpdatedDate = DateTime.UtcNow;

            var checklists = _dbContext.ToDoChecklists
                .Where(tc => tc.ToDoTaskId == checklistEntry.ToDoTaskId && !tc.IsDeleted);

            var totalChecklists = await checklists.CountAsync(cancellationToken);
            var completedChecklists = await checklists.CountAsync(tc => tc.IsComplete, cancellationToken);

            toDoMaster.PercentageCompleted = (decimal)completedChecklists / totalChecklists * 100;

            if (toDoMaster.PercentageCompleted == 100)
            {
                toDoMaster.IsComplete = true;
                toDoMaster.CompletedDate = DateTime.UtcNow;
                toDoMaster.UpdatedDate = DateTime.UtcNow;
            }


            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SingleResponse<MarkSingleChecklistCompletedDTO>
            {
                Data = new MarkSingleChecklistCompletedDTO { ToDoChecklistId = toDoChecklistId, IsComplete = checklistEntry.IsComplete },
                Status = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            // Log the exception in the logging system
            throw new ApplicationException("An error occurred while marking the checklist as complete.", ex);
        }
    }
}




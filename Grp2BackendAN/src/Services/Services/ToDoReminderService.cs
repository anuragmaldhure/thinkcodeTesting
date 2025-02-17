
namespace thinkbridge.Grp2BackendAN.Services.Services;
public class ToDoReminderService : IToDoReminderService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public ToDoReminderService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allToDoReminders or fetchesToDoReminders with pagination based on requestDto
     public async Task<ListResponse<ToDoReminderResDto>> GetAll(GetAllToDoReminderReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<ToDoReminder>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<ToDoReminder, GetAllToDoReminderReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<ToDoReminderResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<ToDoReminderResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new ToDoReminder to the database
     public async Task<SingleResponse<ToDoReminderResDto>> Save(AddToDoReminderReqDto requestDto)
     {
        var entity = _mapper.Map<ToDoReminder>(requestDto); // Maps the request DTO to the ToDoReminder entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<ToDoReminderResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<ToDoReminderResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing ToDoReminder in the database
     public async Task<SingleResponse<ToDoReminderResDto>> Update(UpdateToDoReminderReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<ToDoReminder,int>(requestDto.ReminderId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.ReminderId)); // Fetches the existing ToDoReminder by ReminderId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<ToDoReminderResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<ToDoReminderResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a ToDoReminder by ReminderId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int ReminderId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<ToDoReminder>(x => x.ReminderId == ReminderId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ReminderId));// Fetches the  ToDoReminder by ReminderId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<ToDoReminderResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<ToDoReminderResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  ToDoReminder record
     public async Task<BaseResponse> Delete(int ReminderId)
     {
        var existing = await _baseRepository.GetByIdAsync<ToDoReminder, int>(ReminderId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ReminderId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }



    /// <summary>
    /// Retrieves reminders set for a specified date.
    /// </summary>
    /// <param name="date">The date for which to retrieve reminders.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of reminder DTOs.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided date is a default value.</exception>
    public async Task<ListResponse<ReminderDto>> GetRemindersForDate(DateTime date, CancellationToken cancellationToken)
    {
        if (date == default)
            throw new ArgumentException("Date cannot be default value.", nameof(date));

        var reminders = await _dbContext.ToDoReminders
            .Where(r => r.SetForDateTime.Date == date.Date)
            .Join(_dbContext.AspNetUsers, r => r.SetById, u => u.Id, (r, u) => new { r, SetByName = u.FirstName + " " + u.LastName })
            .Join(_dbContext.ToDoMasters, ru => ru.r.ToDoTaskId, t => t.ToDoTaskId, (ru, t) => new { ru.r, ru.SetByName, t })
            .Join(_dbContext.AspNetUsers, rut => rut.t.AssignedToId, a => a.Id, (rut, a) => new ReminderDto
            {
                ReminderId = rut.r.ReminderId,
                SetById = rut.r.SetById,
                SetByName = rut.SetByName,
                ToDoTaskId = rut.t.ToDoTaskId,
                TaskTitle = rut.t.Title,
                AssignedToId = rut.t.AssignedToId,
                AssignedToName = a.FirstName + " " + a.LastName,
                IsActive = rut.r.IsActive
            })
            .ToListAsync(cancellationToken);

        return new ListResponse<ReminderDto> { Data = reminders };
    }
    
}




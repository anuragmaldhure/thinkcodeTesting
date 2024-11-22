
namespace thinkbridge.Grp2BackendAN.Services.Services;
public class AspNetUserService : IAspNetUserService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public AspNetUserService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allAspNetUsers or fetchesAspNetUsers with pagination based on requestDto
     public async Task<ListResponse<AspNetUserResDto>> GetAll(GetAllAspNetUserReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<AspNetUser>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<AspNetUser, GetAllAspNetUserReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<AspNetUserResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<AspNetUserResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new AspNetUser to the database
     public async Task<SingleResponse<AspNetUserResDto>> Save(AddAspNetUserReqDto requestDto)
     {
        var entity = _mapper.Map<AspNetUser>(requestDto); // Maps the request DTO to the AspNetUser entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<AspNetUserResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<AspNetUserResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing AspNetUser in the database
     public async Task<SingleResponse<AspNetUserResDto>> Update(UpdateAspNetUserReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<AspNetUser,string>(requestDto.Id) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.Id)); // Fetches the existing AspNetUser by Id 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<AspNetUserResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<AspNetUserResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a AspNetUser by Id with optional details
     public async Task<SingleResponse<dynamic>> GetById(string Id, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<AspNetUser>(x => x.Id == Id ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, Id));// Fetches the  AspNetUser by Id or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<AspNetUserResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<AspNetUserResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  AspNetUser record
     public async Task<BaseResponse> Delete(string Id)
     {
        var existing = await _baseRepository.GetByIdAsync<AspNetUser, string>(Id)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, Id)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }
       
}




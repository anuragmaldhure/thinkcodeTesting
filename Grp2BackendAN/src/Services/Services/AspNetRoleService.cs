
namespace thinkbridge.Grp2BackendAN.Services.Services;
public class AspNetRoleService : IAspNetRoleService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public AspNetRoleService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allAspNetRoles or fetchesAspNetRoles with pagination based on requestDto
     public async Task<ListResponse<AspNetRoleResDto>> GetAll(GetAllAspNetRoleReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<AspNetRole>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<AspNetRole, GetAllAspNetRoleReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<AspNetRoleResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<AspNetRoleResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new AspNetRole to the database
     public async Task<SingleResponse<AspNetRoleResDto>> Save(AddAspNetRoleReqDto requestDto)
     {
        var entity = _mapper.Map<AspNetRole>(requestDto); // Maps the request DTO to the AspNetRole entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<AspNetRoleResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<AspNetRoleResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing AspNetRole in the database
     public async Task<SingleResponse<AspNetRoleResDto>> Update(UpdateAspNetRoleReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<AspNetRole,string>(requestDto.Id) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.Id)); // Fetches the existing AspNetRole by Id 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<AspNetRoleResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<AspNetRoleResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a AspNetRole by Id with optional details
     public async Task<SingleResponse<dynamic>> GetById(string Id, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<AspNetRole>(x => x.Id == Id ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, Id));// Fetches the  AspNetRole by Id or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<AspNetRoleResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<AspNetRoleResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  AspNetRole record
     public async Task<BaseResponse> Delete(string Id)
     {
        var existing = await _baseRepository.GetByIdAsync<AspNetRole, string>(Id)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, Id)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }
       
}




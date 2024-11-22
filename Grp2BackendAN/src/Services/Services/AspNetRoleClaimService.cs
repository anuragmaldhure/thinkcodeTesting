
namespace thinkbridge.Grp2BackendAN.Services.Services;
public class AspNetRoleClaimService : IAspNetRoleClaimService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public AspNetRoleClaimService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allAspNetRoleClaims or fetchesAspNetRoleClaims with pagination based on requestDto
     public async Task<ListResponse<AspNetRoleClaimResDto>> GetAll(GetAllAspNetRoleClaimReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<AspNetRoleClaim>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<AspNetRoleClaim, GetAllAspNetRoleClaimReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<AspNetRoleClaimResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<AspNetRoleClaimResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new AspNetRoleClaim to the database
     public async Task<SingleResponse<AspNetRoleClaimResDto>> Save(AddAspNetRoleClaimReqDto requestDto)
     {
        var entity = _mapper.Map<AspNetRoleClaim>(requestDto); // Maps the request DTO to the AspNetRoleClaim entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<AspNetRoleClaimResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<AspNetRoleClaimResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing AspNetRoleClaim in the database
     public async Task<SingleResponse<AspNetRoleClaimResDto>> Update(UpdateAspNetRoleClaimReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<AspNetRoleClaim,int>(requestDto.Id) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.Id)); // Fetches the existing AspNetRoleClaim by Id 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<AspNetRoleClaimResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<AspNetRoleClaimResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a AspNetRoleClaim by Id with optional details
     public async Task<SingleResponse<dynamic>> GetById(int Id, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<AspNetRoleClaim>(x => x.Id == Id ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, Id));// Fetches the  AspNetRoleClaim by Id or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<AspNetRoleClaimResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<AspNetRoleClaimResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  AspNetRoleClaim record
     public async Task<BaseResponse> Delete(int Id)
     {
        var existing = await _baseRepository.GetByIdAsync<AspNetRoleClaim, int>(Id)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, Id)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }
       
}




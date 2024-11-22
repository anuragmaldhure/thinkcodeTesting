
namespace thinkbridge.Grp2BackendAN.Services.Services;
public class AspNetUserClaimService : IAspNetUserClaimService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public AspNetUserClaimService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allAspNetUserClaims or fetchesAspNetUserClaims with pagination based on requestDto
     public async Task<ListResponse<AspNetUserClaimResDto>> GetAll(GetAllAspNetUserClaimReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<AspNetUserClaim>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<AspNetUserClaim, GetAllAspNetUserClaimReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<AspNetUserClaimResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<AspNetUserClaimResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new AspNetUserClaim to the database
     public async Task<SingleResponse<AspNetUserClaimResDto>> Save(AddAspNetUserClaimReqDto requestDto)
     {
        var entity = _mapper.Map<AspNetUserClaim>(requestDto); // Maps the request DTO to the AspNetUserClaim entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<AspNetUserClaimResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<AspNetUserClaimResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing AspNetUserClaim in the database
     public async Task<SingleResponse<AspNetUserClaimResDto>> Update(UpdateAspNetUserClaimReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<AspNetUserClaim,int>(requestDto.Id) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.Id)); // Fetches the existing AspNetUserClaim by Id 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<AspNetUserClaimResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<AspNetUserClaimResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a AspNetUserClaim by Id with optional details
     public async Task<SingleResponse<dynamic>> GetById(int Id, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<AspNetUserClaim>(x => x.Id == Id ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, Id));// Fetches the  AspNetUserClaim by Id or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<AspNetUserClaimResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<AspNetUserClaimResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  AspNetUserClaim record
     public async Task<BaseResponse> Delete(int Id)
     {
        var existing = await _baseRepository.GetByIdAsync<AspNetUserClaim, int>(Id)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, Id)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }
       
}




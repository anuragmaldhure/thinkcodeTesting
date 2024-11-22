
namespace thinkbridge.Grp2BackendAN.Services.Services;
public class TeamService : ITeamService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public TeamService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allTeams or fetchesTeams with pagination based on requestDto
     public async Task<ListResponse<TeamResDto>> GetAll(GetAllTeamReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<Team>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<Team, GetAllTeamReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<TeamResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<TeamResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new Team to the database
     public async Task<SingleResponse<TeamResDto>> Save(AddTeamReqDto requestDto)
     {
        var entity = _mapper.Map<Team>(requestDto); // Maps the request DTO to the Team entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<TeamResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<TeamResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing Team in the database
     public async Task<SingleResponse<TeamResDto>> Update(UpdateTeamReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<Team,int>(requestDto.TeamId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.TeamId)); // Fetches the existing Team by TeamId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<TeamResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<TeamResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a Team by TeamId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int TeamId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<Team>(x => x.TeamId == TeamId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, TeamId));// Fetches the  Team by TeamId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<TeamResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<TeamResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  Team record
     public async Task<BaseResponse> Delete(int TeamId)
     {
        var existing = await _baseRepository.GetByIdAsync<Team, int>(TeamId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, TeamId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }
       
}




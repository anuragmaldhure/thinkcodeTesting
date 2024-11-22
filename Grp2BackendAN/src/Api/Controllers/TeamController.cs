
namespace thinkbridge.Grp2BackendAN.Api.Controllers
{
    public class TeamController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  TeamController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all Teams, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of Teams with pagination info.</returns>    
        [HttpPost("GetAllTeam")]
        public async Task<ActionResult<ListResponse<TeamResDto>>> GetAllTeam(GetAllTeamReqDto? requestDto)
        { 
            var result = await _servicesCollection.TeamServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a Team by TeamId, with optional detailed response.
        /// </summary>
        /// <param name="Id"> Team Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> Team details.</returns>
        [HttpGet("GetTeamById/{TeamId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<TeamResDetailDto>>> GetTeamById(int  TeamId,  bool withDetails = false)
        {
           var result = await _servicesCollection.TeamServices.GetById(TeamId, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new Team.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a Team.</param>
        /// <returns>Added Team details.</returns>
        [HttpPost("AddTeam")]
        public async Task<ActionResult<SingleResponse<TeamResDto>>> AddTeam(AddTeamReqDto requestDto)
        {
            var result = await _servicesCollection.TeamServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing Team.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a Team.</param>
        /// <returns>Updated Team details.</returns>
        [HttpPost("UpdateTeam")]
        public async Task<ActionResult<SingleResponse<TeamResDto>>> UpdateTeam(UpdateTeamReqDto requestDto)
        {
            var result = await _servicesCollection.TeamServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a Team.
        /// </summary>
        /// <param name="Id">Team TeamId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteTeam/{TeamId}")]
        public async Task<IActionResult> DeleteTeam(int  TeamId)
        {
           var result = await _servicesCollection.TeamServices.Delete(TeamId);
           return HandleResult(result);
        }

    }
}





namespace thinkbridge.Grp2BackendAN.Api.Controllers
{
    public class AspNetRoleClaimController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  AspNetRoleClaimController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all AspNetRoleClaims, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of AspNetRoleClaims with pagination info.</returns>    
        [HttpPost("GetAllAspNetRoleClaim")]
        public async Task<ActionResult<ListResponse<AspNetRoleClaimResDto>>> GetAllAspNetRoleClaim(GetAllAspNetRoleClaimReqDto? requestDto)
        { 
            var result = await _servicesCollection.AspNetRoleClaimServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a AspNetRoleClaim by Id, with optional detailed response.
        /// </summary>
        /// <param name="Id"> AspNetRoleClaim Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> AspNetRoleClaim details.</returns>
        [HttpGet("GetAspNetRoleClaimById/{Id}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<AspNetRoleClaimResDetailDto>>> GetAspNetRoleClaimById(int  Id,  bool withDetails = false)
        {
           var result = await _servicesCollection.AspNetRoleClaimServices.GetById(Id, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new AspNetRoleClaim.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a AspNetRoleClaim.</param>
        /// <returns>Added AspNetRoleClaim details.</returns>
        [HttpPost("AddAspNetRoleClaim")]
        public async Task<ActionResult<SingleResponse<AspNetRoleClaimResDto>>> AddAspNetRoleClaim(AddAspNetRoleClaimReqDto requestDto)
        {
            var result = await _servicesCollection.AspNetRoleClaimServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing AspNetRoleClaim.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a AspNetRoleClaim.</param>
        /// <returns>Updated AspNetRoleClaim details.</returns>
        [HttpPost("UpdateAspNetRoleClaim")]
        public async Task<ActionResult<SingleResponse<AspNetRoleClaimResDto>>> UpdateAspNetRoleClaim(UpdateAspNetRoleClaimReqDto requestDto)
        {
            var result = await _servicesCollection.AspNetRoleClaimServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a AspNetRoleClaim.
        /// </summary>
        /// <param name="Id">AspNetRoleClaim Id.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteAspNetRoleClaim/{Id}")]
        public async Task<IActionResult> DeleteAspNetRoleClaim(int  Id)
        {
           var result = await _servicesCollection.AspNetRoleClaimServices.Delete(Id);
           return HandleResult(result);
        }

    }
}





namespace thinkbridge.Grp2BackendAN.Api.Controllers
{
    public class AspNetUserClaimController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  AspNetUserClaimController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all AspNetUserClaims, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of AspNetUserClaims with pagination info.</returns>    
        [HttpPost("GetAllAspNetUserClaim")]
        public async Task<ActionResult<ListResponse<AspNetUserClaimResDto>>> GetAllAspNetUserClaim(GetAllAspNetUserClaimReqDto? requestDto)
        { 
            var result = await _servicesCollection.AspNetUserClaimServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a AspNetUserClaim by Id, with optional detailed response.
        /// </summary>
        /// <param name="Id"> AspNetUserClaim Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> AspNetUserClaim details.</returns>
        [HttpGet("GetAspNetUserClaimById/{Id}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<AspNetUserClaimResDetailDto>>> GetAspNetUserClaimById(int  Id,  bool withDetails = false)
        {
           var result = await _servicesCollection.AspNetUserClaimServices.GetById(Id, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new AspNetUserClaim.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a AspNetUserClaim.</param>
        /// <returns>Added AspNetUserClaim details.</returns>
        [HttpPost("AddAspNetUserClaim")]
        public async Task<ActionResult<SingleResponse<AspNetUserClaimResDto>>> AddAspNetUserClaim(AddAspNetUserClaimReqDto requestDto)
        {
            var result = await _servicesCollection.AspNetUserClaimServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing AspNetUserClaim.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a AspNetUserClaim.</param>
        /// <returns>Updated AspNetUserClaim details.</returns>
        [HttpPost("UpdateAspNetUserClaim")]
        public async Task<ActionResult<SingleResponse<AspNetUserClaimResDto>>> UpdateAspNetUserClaim(UpdateAspNetUserClaimReqDto requestDto)
        {
            var result = await _servicesCollection.AspNetUserClaimServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a AspNetUserClaim.
        /// </summary>
        /// <param name="Id">AspNetUserClaim Id.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteAspNetUserClaim/{Id}")]
        public async Task<IActionResult> DeleteAspNetUserClaim(int  Id)
        {
           var result = await _servicesCollection.AspNetUserClaimServices.Delete(Id);
           return HandleResult(result);
        }

    }
}




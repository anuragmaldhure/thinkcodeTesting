
namespace thinkbridge.Grp2BackendAN.Api.Controllers
{
    public class AspNetRoleController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  AspNetRoleController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all AspNetRoles, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of AspNetRoles with pagination info.</returns>    
        [HttpPost("GetAllAspNetRole")]
        public async Task<ActionResult<ListResponse<AspNetRoleResDto>>> GetAllAspNetRole(GetAllAspNetRoleReqDto? requestDto)
        { 
            var result = await _servicesCollection.AspNetRoleServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a AspNetRole by Id, with optional detailed response.
        /// </summary>
        /// <param name="Id"> AspNetRole Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> AspNetRole details.</returns>
        [HttpGet("GetAspNetRoleById/{Id}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<AspNetRoleResDetailDto>>> GetAspNetRoleById(string  Id,  bool withDetails = false)
        {
           var result = await _servicesCollection.AspNetRoleServices.GetById(Id, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new AspNetRole.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a AspNetRole.</param>
        /// <returns>Added AspNetRole details.</returns>
        [HttpPost("AddAspNetRole")]
        public async Task<ActionResult<SingleResponse<AspNetRoleResDto>>> AddAspNetRole(AddAspNetRoleReqDto requestDto)
        {
            var result = await _servicesCollection.AspNetRoleServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing AspNetRole.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a AspNetRole.</param>
        /// <returns>Updated AspNetRole details.</returns>
        [HttpPost("UpdateAspNetRole")]
        public async Task<ActionResult<SingleResponse<AspNetRoleResDto>>> UpdateAspNetRole(UpdateAspNetRoleReqDto requestDto)
        {
            var result = await _servicesCollection.AspNetRoleServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a AspNetRole.
        /// </summary>
        /// <param name="Id">AspNetRole Id.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteAspNetRole/{Id}")]
        public async Task<IActionResult> DeleteAspNetRole(string  Id)
        {
           var result = await _servicesCollection.AspNetRoleServices.Delete(Id);
           return HandleResult(result);
        }

    }
}




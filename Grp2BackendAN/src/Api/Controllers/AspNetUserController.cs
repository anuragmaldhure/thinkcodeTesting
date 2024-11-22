
namespace thinkbridge.Grp2BackendAN.Api.Controllers
{
    public class AspNetUserController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  AspNetUserController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all AspNetUsers, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of AspNetUsers with pagination info.</returns>    
        [HttpPost("GetAllAspNetUser")]
        public async Task<ActionResult<ListResponse<AspNetUserResDto>>> GetAllAspNetUser(GetAllAspNetUserReqDto? requestDto)
        { 
            var result = await _servicesCollection.AspNetUserServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a AspNetUser by Id, with optional detailed response.
        /// </summary>
        /// <param name="Id"> AspNetUser Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> AspNetUser details.</returns>
        [HttpGet("GetAspNetUserById/{Id}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<AspNetUserResDetailDto>>> GetAspNetUserById(string  Id,  bool withDetails = false)
        {
           var result = await _servicesCollection.AspNetUserServices.GetById(Id, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new AspNetUser.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a AspNetUser.</param>
        /// <returns>Added AspNetUser details.</returns>
        [HttpPost("AddAspNetUser")]
        public async Task<ActionResult<SingleResponse<AspNetUserResDto>>> AddAspNetUser(AddAspNetUserReqDto requestDto)
        {
            var result = await _servicesCollection.AspNetUserServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing AspNetUser.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a AspNetUser.</param>
        /// <returns>Updated AspNetUser details.</returns>
        [HttpPost("UpdateAspNetUser")]
        public async Task<ActionResult<SingleResponse<AspNetUserResDto>>> UpdateAspNetUser(UpdateAspNetUserReqDto requestDto)
        {
            var result = await _servicesCollection.AspNetUserServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a AspNetUser.
        /// </summary>
        /// <param name="Id">AspNetUser Id.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteAspNetUser/{Id}")]
        public async Task<IActionResult> DeleteAspNetUser(string  Id)
        {
           var result = await _servicesCollection.AspNetUserServices.Delete(Id);
           return HandleResult(result);
        }

    }
}





namespace thinkbridge.Grp2BackendAN.Api.Controllers
{
    public class ToDoReminderController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  ToDoReminderController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all ToDoReminders, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of ToDoReminders with pagination info.</returns>    
        [HttpPost("GetAllToDoReminder")]
        public async Task<ActionResult<ListResponse<ToDoReminderResDto>>> GetAllToDoReminder(GetAllToDoReminderReqDto? requestDto)
        { 
            var result = await _servicesCollection.ToDoReminderServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a ToDoReminder by ReminderId, with optional detailed response.
        /// </summary>
        /// <param name="Id"> ToDoReminder Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> ToDoReminder details.</returns>
        [HttpGet("GetToDoReminderById/{ReminderId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<ToDoReminderResDetailDto>>> GetToDoReminderById(int  ReminderId,  bool withDetails = false)
        {
           var result = await _servicesCollection.ToDoReminderServices.GetById(ReminderId, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new ToDoReminder.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a ToDoReminder.</param>
        /// <returns>Added ToDoReminder details.</returns>
        [HttpPost("AddToDoReminder")]
        public async Task<ActionResult<SingleResponse<ToDoReminderResDto>>> AddToDoReminder(AddToDoReminderReqDto requestDto)
        {
            var result = await _servicesCollection.ToDoReminderServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing ToDoReminder.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a ToDoReminder.</param>
        /// <returns>Updated ToDoReminder details.</returns>
        [HttpPost("UpdateToDoReminder")]
        public async Task<ActionResult<SingleResponse<ToDoReminderResDto>>> UpdateToDoReminder(UpdateToDoReminderReqDto requestDto)
        {
            var result = await _servicesCollection.ToDoReminderServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a ToDoReminder.
        /// </summary>
        /// <param name="Id">ToDoReminder ReminderId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteToDoReminder/{ReminderId}")]
        public async Task<IActionResult> DeleteToDoReminder(int  ReminderId)
        {
           var result = await _servicesCollection.ToDoReminderServices.Delete(ReminderId);
           return HandleResult(result);
        }

    }
}




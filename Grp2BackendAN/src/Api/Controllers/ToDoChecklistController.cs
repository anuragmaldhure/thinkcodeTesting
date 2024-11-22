
namespace thinkbridge.Grp2BackendAN.Api.Controllers
{
    public class ToDoChecklistController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  ToDoChecklistController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all ToDoChecklists, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of ToDoChecklists with pagination info.</returns>    
        [HttpPost("GetAllToDoChecklist")]
        public async Task<ActionResult<ListResponse<ToDoChecklistResDto>>> GetAllToDoChecklist(GetAllToDoChecklistReqDto? requestDto)
        { 
            var result = await _servicesCollection.ToDoChecklistServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a ToDoChecklist by ToDoChecklistId, with optional detailed response.
        /// </summary>
        /// <param name="Id"> ToDoChecklist Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> ToDoChecklist details.</returns>
        [HttpGet("GetToDoChecklistById/{ToDoChecklistId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<ToDoChecklistResDetailDto>>> GetToDoChecklistById(int  ToDoChecklistId,  bool withDetails = false)
        {
           var result = await _servicesCollection.ToDoChecklistServices.GetById(ToDoChecklistId, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new ToDoChecklist.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a ToDoChecklist.</param>
        /// <returns>Added ToDoChecklist details.</returns>
        [HttpPost("AddToDoChecklist")]
        public async Task<ActionResult<SingleResponse<ToDoChecklistResDto>>> AddToDoChecklist(AddToDoChecklistReqDto requestDto)
        {
            var result = await _servicesCollection.ToDoChecklistServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing ToDoChecklist.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a ToDoChecklist.</param>
        /// <returns>Updated ToDoChecklist details.</returns>
        [HttpPost("UpdateToDoChecklist")]
        public async Task<ActionResult<SingleResponse<ToDoChecklistResDto>>> UpdateToDoChecklist(UpdateToDoChecklistReqDto requestDto)
        {
            var result = await _servicesCollection.ToDoChecklistServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a ToDoChecklist.
        /// </summary>
        /// <param name="Id">ToDoChecklist ToDoChecklistId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteToDoChecklist/{ToDoChecklistId}")]
        public async Task<IActionResult> DeleteToDoChecklist(int  ToDoChecklistId)
        {
           var result = await _servicesCollection.ToDoChecklistServices.Delete(ToDoChecklistId);
           return HandleResult(result);
        }


        //Custom API to mark a single subtask as completed

        /// <summary>
        /// API endpoint to mark a single checklist item as completed.
        /// </summary>
        /// <param name="toDoChecklistId">ID of the ToDo checklist to mark as completed.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>Action result containing the response with status and result.</returns>
        [HttpPut("MarkSingleChecklistCompleted")]
        public async Task<ActionResult<SingleResponse<MarkSingleChecklistCompletedDTO>>> MarkSingleChecklistCompleted([FromBody] int toDoChecklistId, CancellationToken cancellationToken)
        {
            if (toDoChecklistId <= 0)
            {
                return BadRequest(new ResponseMessage { Message = "Invalid checklist Id." });
            }

            try
            {
                var result = await _servicesCollection.ToDoChecklistServices.MarkSingleChecklistCompleted(toDoChecklistId, cancellationToken);
                return HandleResult(result);
            }
            catch (ApplicationException ex)
            {
                // Log exception
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseMessage { Message = ex.Message });
            }

        }
    }
}




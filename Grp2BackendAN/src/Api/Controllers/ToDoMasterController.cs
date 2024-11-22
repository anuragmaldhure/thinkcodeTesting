
namespace thinkbridge.Grp2BackendAN.Api.Controllers
{
    public class ToDoMasterController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;

        public ToDoMasterController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;

        }

        /// <summary>
        /// Endpoint to get all ToDoMasters, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of ToDoMasters with pagination info.</returns>    
        [HttpPost("GetAllToDoMaster")]
        public async Task<ActionResult<ListResponse<ToDoMasterResDto>>> GetAllToDoMaster(GetAllToDoMasterReqDto? requestDto)
        {
            var result = await _servicesCollection.ToDoMasterServices.GetAll(requestDto);
            return HandleResult(result);
        }

        /// <summary>
        /// Endpoint to get a ToDoMaster by ToDoTaskId, with optional detailed response.
        /// </summary>
        /// <param name="Id"> ToDoMaster Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> ToDoMaster details.</returns>
        [HttpGet("GetToDoMasterById/{ToDoTaskId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<ToDoMasterResDetailDto>>> GetToDoMasterById(int ToDoTaskId, bool withDetails = false)
        {
            var result = await _servicesCollection.ToDoMasterServices.GetById(ToDoTaskId, withDetails);
            return HandleResult(result);
        }


        /// <summary>
        /// Endpoint to add a new ToDoMaster.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a ToDoMaster.</param>
        /// <returns>Added ToDoMaster details.</returns>
        [HttpPost("AddToDoMaster")]
        public async Task<ActionResult<SingleResponse<ToDoMasterResDto>>> AddToDoMaster(AddToDoMasterReqDto requestDto)
        {
            var result = await _servicesCollection.ToDoMasterServices.Save(requestDto);
            return HandleResult(result);
        }


        /// <summary>
        /// Endpoint to update an existing ToDoMaster.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a ToDoMaster.</param>
        /// <returns>Updated ToDoMaster details.</returns>
        [HttpPost("UpdateToDoMaster")]
        public async Task<ActionResult<SingleResponse<ToDoMasterResDto>>> UpdateToDoMaster(UpdateToDoMasterReqDto requestDto)
        {
            var result = await _servicesCollection.ToDoMasterServices.Update(requestDto);
            return HandleResult(result);
        }



        /// <summary>
        /// Endpoint to delete a ToDoMaster.
        /// </summary>
        /// <param name="Id">ToDoMaster ToDoTaskId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteToDoMaster/{ToDoTaskId}")]
        public async Task<IActionResult> DeleteToDoMaster(int ToDoTaskId)
        {
            var result = await _servicesCollection.ToDoMasterServices.Delete(ToDoTaskId);
            return HandleResult(result);
        }


        //Custom API for soft deleting the main task

        /// <summary>
        /// Marks a ToDo task as deleted by task ID.
        /// </summary>
        /// <param name="toDoTaskId">The ID of the ToDo task to mark as deleted.</param>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <returns>The status of the operation wrapped in an ActionResult.</returns>
        [HttpPut("MarkAsDeleted/{toDoTaskId}")]
        public async Task<ActionResult<SingleResponse<string>>> MarkAsDeleted(int toDoTaskId, CancellationToken cancellationToken)
        {
            if (toDoTaskId <= 0)
            {
                return BadRequest(new SingleResponse<string> { Messages = new List<ResponseMessage> { new ResponseMessage { Message = "Invalid task ID." } } });
            }

            var result = await _servicesCollection.ToDoMasterServices.MarkAsDeleted(toDoTaskId, cancellationToken);
            return HandleResult(result);
        }


        //Custom API for marking main Task as completed

        /// <summary>
        /// Endpoint to mark a todo task as completed.
        /// </summary>
        /// <param name="toDoTaskId">The ID of the task to be marked complete.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response containing the result of the operation.</returns>
        [HttpPut("MarkTaskCompleted")]
        public async Task<ActionResult<SingleResponse<MarkTaskCompletedDTO>>> MarkTaskCompleted([FromBody] int toDoTaskId, CancellationToken cancellationToken)
        {
            if (toDoTaskId <= 0)
            {
                return BadRequest("Task ID must be greater than zero.");
            }

            var result = await _servicesCollection.ToDoMasterServices.MarkTaskCompleted(toDoTaskId, cancellationToken);
            return HandleResult(result);
        }


        //Custom API

        /// <summary>
        /// Updates the ToDo master task with the given ID.
        /// </summary>
        /// <param name="toDoTaskId">ID of the task to update.</param>
        /// <param name="updateToDoMasterDTO">DTO with updated task information.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>Updated task wrapped in a response.</returns>
        [HttpPut("UpdateToDoMaster/{toDoTaskId}")]
        public async Task<ActionResult<SingleResponse<UpdateToDoMasterDTO>>> UpdateToDoMaster(int toDoTaskId, [FromBody] UpdateToDoMasterDTO updateToDoMasterDTO, CancellationToken cancellationToken)
        {
            if (updateToDoMasterDTO == null)
            {
                return BadRequest("Invalid input data.");
            }

            var result = await _servicesCollection.ToDoMasterServices.UpdateToDoMaster(toDoTaskId, updateToDoMasterDTO, cancellationToken);
            return HandleResult(result);
        }



        //Report 1 endpoint - For overdue tasks within a date range



        /// <summary>
        /// Handles the HTTP GET request to retrieve overdue tasks.
        /// </summary>
        /// <param name="userId">Optional user ID to filter tasks by.</param>
        /// <param name="dateRangeDto">Date range for filtering tasks.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>A list of overdue tasks as an <see cref="ActionResult{ListResponse{ToDoMasterDto}}"/>.</returns>
        [HttpPost("GetOverdueTasks")]
        public async Task<ActionResult<ListResponse<ToDoMasterDto>>> GetOverdueTasks([FromQuery] string? userId, [FromBody] DateRangeDto dateRangeDto, CancellationToken cancellationToken)
        {
            if (dateRangeDto == null)
                return BadRequest("Date range cannot be null.");

            var result = await _servicesCollection.ToDoMasterServices.GetOverdueTasks(userId, dateRangeDto.StartDate, dateRangeDto.EndDate, cancellationToken);
            return HandleResult(result);
        }


        //Report -3 

        /// <summary>
        /// API endpoint to retrieve tasks completed by each user in the last 7 days.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A Task result containing ActionResult with list of TaskCompletionPerUserDto.</returns>
        [HttpGet("GetTasksCompletedLast7DaysPerUser")]
        public async Task<ActionResult<ListResponse<TaskCompletionPerUserDto>>> GetTasksCompletedLast7DaysPerUser(CancellationToken cancellationToken)
        {
            var result = await _servicesCollection.ToDoMasterServices.GetTasksCompletedLast7DaysPerUser(cancellationToken);
            return HandleResult(result);
        }
        
    }
}




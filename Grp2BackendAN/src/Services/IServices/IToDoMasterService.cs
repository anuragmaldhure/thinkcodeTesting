
namespace thinkbridge.Grp2BackendAN.Services.IServices;
public interface IToDoMasterService
{

    Task<ListResponse<ToDoMasterResDto>> GetAll(GetAllToDoMasterReqDto? requestDto);
    Task<SingleResponse<ToDoMasterResDto>> Save(AddToDoMasterReqDto requestDto);
    Task<SingleResponse<ToDoMasterResDto>> Update(UpdateToDoMasterReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int ToDoTaskId, bool withDetails = false);
    Task<BaseResponse> Delete(int ToDoTaskId);

    /// <summary>
    /// Marks a ToDo task as deleted.
    /// </summary>
    /// <param name="toDoTaskId">The ID of the ToDo task to mark as deleted.</param>
    /// <param name="cancellationToken">Token for cancelling the operation.</param>
    /// <returns>The status of the operation wrapped in a SingleResponse object.</returns>
    Task<SingleResponse<string>> MarkAsDeleted(int toDoTaskId, CancellationToken cancellationToken);

    Task<SingleResponse<MarkTaskCompletedDTO>> MarkTaskCompleted(int toDoTaskId, CancellationToken cancellationToken);




    Task<SingleResponse<UpdateToDoMasterDTO>> UpdateToDoMaster(int toDoTaskId, UpdateToDoMasterDTO updateToDoMasterDTO, CancellationToken cancellationToken);


    //Report 1 

    /// <summary>
    /// Retrieves a list of overdue tasks based on the given criteria.
    /// </summary>
    /// <param name="userId">ID of the user to filter tasks by. Null or empty to ignore filter.</param>
    /// <param name="startDate">Start date for filtering tasks. Null to ignore filter.</param>
    /// <param name="endDate">End date for filtering tasks. Null to ignore filter.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A list of overdue tasks as a <see cref="ListResponse{ToDoMasterDto}"/>.</returns>
    Task<ListResponse<ToDoMasterDto>> GetOverdueTasks(string? userId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken);



    //Report 4 

    /// <summary>
    /// Retrieves a list of the number of tasks completed by each user in the last 7 days.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A Task containing a ListResponse of TaskCompletionPerUserDto.</returns>
    Task<ListResponse<TaskCompletionPerUserDto>> GetTasksCompletedLast7DaysPerUser(CancellationToken cancellationToken);
}




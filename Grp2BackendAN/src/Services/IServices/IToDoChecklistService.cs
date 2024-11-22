
namespace thinkbridge.Grp2BackendAN.Services.IServices;
public interface IToDoChecklistService
{

    Task<ListResponse<ToDoChecklistResDto>> GetAll(GetAllToDoChecklistReqDto? requestDto);
    Task<SingleResponse<ToDoChecklistResDto>> Save(AddToDoChecklistReqDto requestDto);
    Task<SingleResponse<ToDoChecklistResDto>> Update(UpdateToDoChecklistReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int ToDoChecklistId, bool withDetails = false);
    Task<BaseResponse> Delete(int ToDoChecklistId);

    /// <summary>
    /// Marks a single checklist item as completed.
    /// </summary>
    /// <param name="toDoChecklistId">ID of the ToDo checklist to mark as completed.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Response indicating the success status and result of the operation.</returns>
    Task<SingleResponse<MarkSingleChecklistCompletedDTO>> MarkSingleChecklistCompleted(int toDoChecklistId, CancellationToken cancellationToken);

}




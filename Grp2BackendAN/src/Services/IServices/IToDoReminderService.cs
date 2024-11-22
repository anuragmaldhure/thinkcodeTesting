
namespace thinkbridge.Grp2BackendAN.Services.IServices;
public interface IToDoReminderService
{
 
    Task<ListResponse<ToDoReminderResDto>> GetAll(GetAllToDoReminderReqDto? requestDto);
    Task<SingleResponse<ToDoReminderResDto>> Save(AddToDoReminderReqDto requestDto);
    Task<SingleResponse<ToDoReminderResDto>> Update(UpdateToDoReminderReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  ReminderId, bool withDetails = false);
    Task<BaseResponse> Delete(int  ReminderId);
  
}




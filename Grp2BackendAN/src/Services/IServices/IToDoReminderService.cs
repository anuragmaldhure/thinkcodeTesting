
namespace thinkbridge.Grp2BackendAN.Services.IServices;
public interface IToDoReminderService
{

    Task<ListResponse<ToDoReminderResDto>> GetAll(GetAllToDoReminderReqDto? requestDto);
    Task<SingleResponse<ToDoReminderResDto>> Save(AddToDoReminderReqDto requestDto);
    Task<SingleResponse<ToDoReminderResDto>> Update(UpdateToDoReminderReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int ReminderId, bool withDetails = false);
    Task<BaseResponse> Delete(int ReminderId);


    //Added

    /// <summary>
    /// Retrieves reminders for a given date.
    /// </summary>
    /// <param name="date">The date for which to fetch reminders.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation with a list of reminders.</returns>
    Task<ListResponse<ReminderDto>> GetRemindersForDate(DateTime date, CancellationToken cancellationToken);

}





namespace thinkbridge.Grp2BackendAN.Services.IServices;
public interface ITeamService
{
 
    Task<ListResponse<TeamResDto>> GetAll(GetAllTeamReqDto? requestDto);
    Task<SingleResponse<TeamResDto>> Save(AddTeamReqDto requestDto);
    Task<SingleResponse<TeamResDto>> Update(UpdateTeamReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  TeamId, bool withDetails = false);
    Task<BaseResponse> Delete(int  TeamId);
  
}




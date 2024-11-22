
namespace thinkbridge.Grp2BackendAN.Services.IServices;
public interface IAspNetRoleClaimService
{
 
    Task<ListResponse<AspNetRoleClaimResDto>> GetAll(GetAllAspNetRoleClaimReqDto? requestDto);
    Task<SingleResponse<AspNetRoleClaimResDto>> Save(AddAspNetRoleClaimReqDto requestDto);
    Task<SingleResponse<AspNetRoleClaimResDto>> Update(UpdateAspNetRoleClaimReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  Id, bool withDetails = false);
    Task<BaseResponse> Delete(int  Id);
  
}




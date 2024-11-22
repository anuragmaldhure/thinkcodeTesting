
namespace thinkbridge.Grp2BackendAN.Services.IServices;
public interface IAspNetRoleService
{
 
    Task<ListResponse<AspNetRoleResDto>> GetAll(GetAllAspNetRoleReqDto? requestDto);
    Task<SingleResponse<AspNetRoleResDto>> Save(AddAspNetRoleReqDto requestDto);
    Task<SingleResponse<AspNetRoleResDto>> Update(UpdateAspNetRoleReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(string  Id, bool withDetails = false);
    Task<BaseResponse> Delete(string  Id);
  
}




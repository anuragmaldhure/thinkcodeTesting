
namespace thinkbridge.Grp2BackendAN.Services.IServices;
public interface IAspNetUserService
{
 
    Task<ListResponse<AspNetUserResDto>> GetAll(GetAllAspNetUserReqDto? requestDto);
    Task<SingleResponse<AspNetUserResDto>> Save(AddAspNetUserReqDto requestDto);
    Task<SingleResponse<AspNetUserResDto>> Update(UpdateAspNetUserReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(string  Id, bool withDetails = false);
    Task<BaseResponse> Delete(string  Id);
  
}




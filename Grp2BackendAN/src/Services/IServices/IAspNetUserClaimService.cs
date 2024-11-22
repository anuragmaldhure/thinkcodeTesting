
namespace thinkbridge.Grp2BackendAN.Services.IServices;
public interface IAspNetUserClaimService
{
 
    Task<ListResponse<AspNetUserClaimResDto>> GetAll(GetAllAspNetUserClaimReqDto? requestDto);
    Task<SingleResponse<AspNetUserClaimResDto>> Save(AddAspNetUserClaimReqDto requestDto);
    Task<SingleResponse<AspNetUserClaimResDto>> Update(UpdateAspNetUserClaimReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  Id, bool withDetails = false);
    Task<BaseResponse> Delete(int  Id);
  
}




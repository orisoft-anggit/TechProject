using TectProject.API.DTO;
using TectProject.API.DTO.Auth.Request;
using TectProject.API.DTO.Auth.Response;

namespace TectProject.API.Interface
{
	public interface IAuthService
	{
		Task<BaseResponse<string>>Login(CreateLoginRequest request);
		
		Task<BaseResponse<CreateRegisterResponse>>Register(CreateRegisterRequest request);
	}
}
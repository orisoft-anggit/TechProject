using AutoMapper;
using TectProject.API.DTO.Auth.Request;
using TectProject.API.DTO.Auth.Response;
using TectProject.API.Models;

namespace TectProject.API.Mapper.Auth
{
    public class AuthConfigurationProfile : Profile
    {
        public AuthConfigurationProfile()
		{
			CreateMap<CreateRegisterRequest, MsUser>();
		
			CreateMap<MsUser, CreateRegisterResponse>();
		}
    }
}
using AutoMapper;
using TectProject.API.DTO.Bpkb.Request;
using TectProject.API.Models;

namespace TectProject.API.Mapper.Bpkb
{
    public class BpkbConfigurationProfile : Profile
    {
        public BpkbConfigurationProfile()
        {
            CreateMap<CreateBpkbRequest, TrBpkb>();
		
        }
    }
}
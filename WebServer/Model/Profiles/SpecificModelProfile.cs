using AutoMapper;
using DataLayer.DataTransferModel;

namespace WebServer.Model.Profiles
{
    public class SpecificModelProfile : Profile
    {
        public SpecificModelProfile()
        {
            CreateMap<PlayerDTO, PlayerModel>();
            CreateMap<SpecificSalt, SpecificSaltModel>();
        }
    }
}
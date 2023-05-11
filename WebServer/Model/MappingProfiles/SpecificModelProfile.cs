using AutoMapper;
using DataLayer.DataTransferModel;
using DataLayer.DatabaseModel.CasinoModel;

namespace WebServer.Model.MappingProfiles
{
    public class SpecificModelProfile : Profile
    {
        public SpecificModelProfile()
        {
            CreateMap<PlayerDTO, PlayerModel>();
            CreateMap<BetDTO, BetModel>();
        }
    }
}
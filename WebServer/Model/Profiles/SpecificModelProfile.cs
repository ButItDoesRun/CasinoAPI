using AutoMapper;
using DataLayer.DataTransferModel;
using DataLayer.DatabaseModel.CasinoModel;

namespace WebServer.Model.Profiles
{
    public class SpecificModelProfile : Profile
    {
        public SpecificModelProfile()
        {
            CreateMap<SpecificPlayer, SpecificPlayerModel>();
            CreateMap<SpecificSalt, SpecificSaltModel>();
            CreateMap<Bet, SpecificBetModel>();
        }
    }
}
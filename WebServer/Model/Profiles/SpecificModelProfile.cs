using AutoMapper;
using DataLayer.DataTransferModel;
using DataLayer.DatabaseModel.CasinoModel;

namespace WebServer.Model.Profiles
{
    public class SpecificModelProfile : Profile
    {
        public SpecificModelProfile()
        {
            CreateMap<SpecificGame, SpecificGameModel>();
            CreateMap<SpecificPlayer, SpecificPlayerModel>();
            CreateMap<SpecificMoneyPot, SpecificMoneyPotModel>();
            CreateMap<SpecificSalt, SpecificSaltModel>();
            CreateMap<Bet, SpecificBetModel>();
        }
    }
}
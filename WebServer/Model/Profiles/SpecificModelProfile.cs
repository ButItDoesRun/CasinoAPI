using AutoMapper;
using DataLayer.DataTransferModel;

namespace WebServer.Model.Profiles
{
    public class SpecificModelProfile : Profile
    {
        public SpecificModelProfile()
        {
            CreateMap<SpecificGame, SpecificGameModel>();
            CreateMap<SpecificPlayer, SpecificPlayerModel>();
            CreateMap<SpecificMoneyPot, SpecificMoneyPotModel>();
        }
    }
}
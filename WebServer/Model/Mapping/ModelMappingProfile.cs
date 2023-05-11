using AutoMapper;
using DataLayer.DataTransferModel;

namespace WebServer.Model.Mapping
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            //Game mapping
            CreateMap<GameDTO, GameModel>();

            //MoneyPot mapping

            //Player mapping
            CreateMap<PlayersDTO, PlayersModel>();

            //Bet mapping
            CreateMap<BetsDTO, BetsModel>();

        }

    }
}
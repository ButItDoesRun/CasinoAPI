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
            CreateMap<GameDTO, GameUpdateModel>();

            //MoneyPot mapping
            CreateMap<MoneyPotDTO, PotModel>()
                .ForMember(model => model.PotAmount, config => config.MapFrom(dto => dto.Amount));
            CreateMap<MoneyPotDTO, PotCreateModel>();
            CreateMap<MoneyPotDTO, PotUpdateModel>();

            //Player mapping
            CreateMap<PlayerDTO, PlayerModel>();
            CreateMap<PlayerDTO, PlayerModel>()
               .ForMember(model => model.Balance, config => config.MapFrom(dto => dto.Balance));
            CreateMap<PlayerDTO, PlayerBalanceUpdateModel>();
            CreateMap<PlayerDTO, PlayerUpdateModel>();
            CreateMap<PlayersDTO, PlayersModel>();
            CreateMap<GamesDTO, PlayerGamesModel>();

            //Bet mapping
            CreateMap<BetsDTO, BetsModel>();
            CreateMap<BetDTO, BetModel>();
            CreateMap<BetDTO, BetCreateModel>();
            CreateMap<BetDTO, BetUpdateModel>();
            CreateMap<BetDTO, BetDeleteModel>();
        }

    }
}
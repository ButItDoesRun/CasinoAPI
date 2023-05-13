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
            CreateMap<PlayersDTO, PlayersModel>();

            //Bet mapping
            CreateMap<BetsDTO, BetsModel>();

        }

    }
}
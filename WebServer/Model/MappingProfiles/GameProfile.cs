using AutoMapper;
using DataLayer.DataTransferModel;
using WebServer.Model;

namespace WebServer.Model.MappingProfiles
{
    public class GameProfile : Profile
    {
        public GameProfile() {
            CreateMap<GameDTO, GameModel>();

        }

    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Model;

namespace DataLayer.DataTransferModel.Profiles
{
    public class ListModelProfile : Profile
    {
        public ListModelProfile()
        {
            CreateMap<GameListElement,GamesModel>();
        }
    }
}

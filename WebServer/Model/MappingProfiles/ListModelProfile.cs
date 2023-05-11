using AutoMapper;
using DataLayer.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Model;

namespace WebServer.Model.MappingProfiles
{
    public class ListModelProfile : Profile
    {
        public ListModelProfile()
        {
            CreateMap<PlayersDTO, PlayersModel>();
            CreateMap<BetsDTO, BetsModel>();
        }
    }
}

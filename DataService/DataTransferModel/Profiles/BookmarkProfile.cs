using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DatabaseModel;

namespace DataLayer.DataTransferModel.Profiles
{
    public class BookmarkProfile : Profile
    {
        public BookmarkProfile()
        {
            CreateMap<BookmarkName, BookmarkElement>();
            CreateMap<BookmarkTitle, BookmarkElement>();
            CreateMap<BookmarkName, BookmarkListElement>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.NConst));
            CreateMap<BookmarkTitle, BookmarkListElement>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.TConst));
        }
    }
}

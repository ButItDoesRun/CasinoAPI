using AutoMapper;
using DataLayer;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;

namespace WebServer.Model 
{ 
    public class GameListModel
    {
        public string? Name { get; set; }
        public string? Url { get; set; }

    }
}

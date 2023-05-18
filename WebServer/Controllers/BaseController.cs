using AutoMapper;
using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebServer.Model;

namespace WebServer.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly LinkGenerator _generator;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _configuration;
        protected const int MaxPageSize = 30;

        protected BaseController(LinkGenerator generator, IMapper mapper, IConfiguration configuration)
        {
            _generator = generator;
            _mapper = mapper;
            _configuration = configuration;
        }

        //Link functions
        [NonAction]
        public string? GenerateLink(string endpointName, object input)
        {
            var link = _generator.GetUriByName(HttpContext, endpointName, input);
            return link;
        }

        [NonAction]
        public UrlModel GenerateUrlModel(string endpointName, object input, object? jsonBody)
        {
            var link = _generator.GetUriByName(HttpContext, endpointName, input);
            UrlModel model = new UrlModel()
            {
                Url = link,
                JsonBody = jsonBody
            };
            return model;
        }



        //MODEL FUNCIONS
        public object DefaultPagingModel<T>(int page, int pageSize, int total, IEnumerable<T> items, string endpointName)
        {
            pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;

            var pages = (int)Math.Ceiling((double)total / (double)pageSize);

            var first = total > 0
                ? GenerateLink(endpointName, new { page = 0, pageSize })
                : null;

            var prev = page > 0
                ? GenerateLink(endpointName, new { page = page - 1, pageSize })
                : null;

            var current = GenerateLink(endpointName, new { page, pageSize });

            var next = page < pages - 1
                ? GenerateLink(endpointName, new { page = page + 1, pageSize })
                : null;

            var result = new
            {
                first,
                prev,
                next,
                current,
                total,
                pages,
                items
            };
            return result;
        }


        //TOKEN FUNCTIONS
        [NonAction]
        public string? GetUsername()
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value;
        }

        [NonAction]
        public string? GenerateJwtToken(string username)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Auth:secret").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }


        [NonAction]
        protected GameModel ConstructGameModel(GameDTO game)
        {
            var gameModel = _mapper.Map<GameModel>(game);
            var gameUpdateModel = _mapper.Map<GameUpdateModel>(game);
            //insert UrlModel for update game
            gameModel.UpdateGameUrl = GenerateUrlModel(nameof(GameController.UpdateGame), new { gid = game.Gid }, gameUpdateModel);
            //insert UrlModel for delete game
            gameModel.DeleteGameUrl = GenerateUrlModel(nameof(GameController.DeleteGame), new { gid = game.Gid }, null);

            return gameModel;
        }

        [NonAction]
        protected PotModel ConstructPotModel(MoneyPotDTO pot)
        {
            var potModel = _mapper.Map<PotModel>(pot);
            var potCreateModel = _mapper.Map<PotCreateModel>(pot);
            var potUpdateModel = _mapper.Map<PotUpdateModel>(pot);
            //insert UrlModel for create pot
            potModel.CreatePotUrl = GenerateUrlModel(nameof(PotController.CreatePot), new { gid = pot.Gid }, potCreateModel);
            //insert UrlModel for update pot
            potModel.UpdatePotUrl = GenerateUrlModel(nameof(PotController.UpdatePot), new { gid = pot.Gid }, potUpdateModel);
            //insert UrlModel for delete pot
            potModel.DeletePotUrl = GenerateUrlModel(nameof(PotController.DeletePot), new { gid = pot.Gid }, null);

            return potModel;
        }

        [NonAction]
        protected BetModel ConstructBetModel(BetDTO bet)
        {
            var betModel = _mapper.Map<BetModel>(bet);
            var betCreateModel = _mapper.Map<BetCreateModel>(bet);
            var betUpdateModel = _mapper.Map<BetUpdateModel>(bet);
            var betDeleteModel = _mapper.Map<BetDeleteModel>(bet);

            betModel.CreateBetUrl = GenerateUrlModel(nameof(BetController.CreateBet), new { gid = bet.Gid }, betCreateModel);

            betModel.UpdateBetUrl = GenerateUrlModel(nameof(BetController.UpdateBet), new { bid = bet.Bid }, betUpdateModel);

            betModel.DeleteBetUrl = GenerateUrlModel(nameof(BetController.DeleteBet), new { bid = bet.Bid }, betDeleteModel);

            return betModel;
        }




        [NonAction]
        protected PlayerModel ConstructPlayerModel(PlayerDTO player)
        {
            var playerModel = _mapper.Map<PlayerModel>(player);
            var playerUpdateModel = _mapper.Map<PlayerUpdateModel>(player);
            var playerBalanceUpdateModel = _mapper.Map<PlayerBalanceUpdateModel>(player);
            //insert UrlModel for update balance
            playerModel.UpdatePlayerUrl = GenerateUrlModel(nameof(PlayerController.UpdatePlayer), new { playername = player.PlayerName }, playerUpdateModel);
            //insert UrlModel for update balance
            playerModel.UpdatePlayerBalanceUrl = GenerateUrlModel(nameof(PlayerController.UpdatePlayerBalance), new { playername = player.PlayerName }, playerBalanceUpdateModel);
            //insert UrlModel for delete player
            playerModel.DeletePlayerUrl = GenerateUrlModel(nameof(PlayerController.DeletePlayer), new { playername = player.PlayerName }, null);
            return playerModel;
        }


    }
}

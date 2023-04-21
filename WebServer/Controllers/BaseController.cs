using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

    }
}

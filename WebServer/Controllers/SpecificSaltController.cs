using AutoMapper;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using WebServer.Model;

namespace WebServer.Controllers
{
        [Route("casino/salt")]
        [ApiController]
        public class SpecificSaltController : BaseController
        {
            private readonly IDataserviceSalt _dataServiceSalt;

            public SpecificSaltController(IDataserviceSalt dataServiceSalt, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
            {
                _dataServiceSalt = dataServiceSalt;
            }

            [HttpGet("{name}", Name = nameof(GetSaltByName))]
            public IActionResult GetSaltByName(string name)
            {
                var specificSalt = _dataServiceSalt.GetSaltByName(name);
                if (specificSalt == null)
                {
                    return NotFound();
                }
                var specificSaltModel = CreateSpecificTitleModel(specificSalt);
                return Ok(specificSaltModel);
            }



            public SpecificSaltModel CreateSpecificTitleModel(SpecificSalt salt)
            {
                var model = _mapper.Map<SpecificSaltModel>(salt);

                model.Url = GenerateLink(nameof(GetSaltByName), new { name = salt.PlayerName });

                return model;
            }


        }
    }


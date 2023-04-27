using AutoMapper;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using WebServer.Model;

namespace WebServer.Controllers
{
    [Route("casino/moneypot")]
    [ApiController]
    public class SpecificMoneyPotController : BaseController
    {
        private readonly IDataserviceMoneyPot _dataServiceMoneyPot;

        public SpecificMoneyPotController(IDataserviceMoneyPot dataServiceMoneyPot, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataServiceMoneyPot = dataServiceMoneyPot;
        }

        [HttpGet("{pid}", Name = nameof(GetMoneyPotByPid))]
        public IActionResult GetMoneyPotByPid(int pid)
        {
            var moneyPotModel = _dataServiceMoneyPot.GetMoneyPotByPid(pid);
            if (moneyPotModel == null)
            {
                return NotFound();
            }
            var specificMoneyPotModel = CreateSpecificMoneyPotModel(moneyPotModel);
            return Ok(specificMoneyPotModel);
        }

        public SpecificMoneyPotModel CreateSpecificMoneyPotModel(SpecificMoneyPot moneyPot)
        {
            var model = _mapper.Map<SpecificMoneyPotModel>(moneyPot);

            model.Url = GenerateLink(nameof(GetMoneyPotByPid), new { pid = moneyPot.Pid });

            return model;
        }
    }
}

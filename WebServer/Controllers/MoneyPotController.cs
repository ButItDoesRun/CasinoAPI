using AutoMapper;
using DataLayer;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using WebServer.Model;

namespace WebServer.Controllers
{
    [Route("casino/moneypots")]
    [ApiController]
    public class MoneyPotsController : BaseController
    {
        private readonly IDataserviceMoneyPots _dataServiceMoneyPots;

        public MoneyPotsController(IDataserviceMoneyPots dataServiceMoneyPots, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataServiceMoneyPots = dataServiceMoneyPots;
        }


        [HttpGet("all", Name = nameof(GetMoneyPots))]
        public IActionResult GetMoneyPots(int page = 0, int pageSize = 20)
        {
            var moneyPots = _dataServiceMoneyPots.GetMoneyPots(page, pageSize);
            if (moneyPots == null)
            {
                return NotFound();
            }
            var moneyPotsModel = CreateMoneyPotsModel(moneyPots);

            var total = _dataServiceMoneyPots.GetNumberOfMoneyPots();

            return Ok(DefaultPagingModel(page, pageSize, total, moneyPotsModel, nameof(GetMoneyPots)));

        }


        public IList<MoneyPotListModel> CreateMoneyPotsModel(IList<MoneyPotListElement> moneyPots)
        {
            var moneyPotsModel = new List<MoneyPotListModel>();

            foreach (var moneyPot in moneyPots)
            {
                var moneyPotModelElement = _mapper.Map<MoneyPotListModel>(moneyPot);
                moneyPotModelElement.Url = _generator.GetUriByName(HttpContext,
                        nameof(BetsController.GetBets),
                        new { id = moneyPot.Pid });

                moneyPotsModel.Add(moneyPotModelElement);
            }

            return moneyPotsModel;
        }
    }
}

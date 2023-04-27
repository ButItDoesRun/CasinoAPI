using AutoMapper;
using DataLayer;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Mvc;
using WebServer.Model;

namespace WebServer.Controllers
{

    [Route("casino/bets")]
    [ApiController]
    public class BetsController : BaseController
    {

        private readonly IDataserviceBets _dataServiceBets;

        public BetsController(IDataserviceBets dataServiceBets, LinkGenerator generator, IMapper mapper, IConfiguration configuration) : base(generator, mapper, configuration)
        {
            _dataServiceBets = dataServiceBets;
        }


        [HttpGet("all", Name = nameof(GetBets))]
        public IActionResult GetBets(int page = 0, int pageSize = 20)
        {
            var bets = _dataServiceBets.GetBets(page, pageSize);
            if (bets == null)
            {
                return NotFound();
            }
            var betsModel = CreateBetsModel(bets);

            var total = _dataServiceBets.GetNumberOfBets();

            return Ok(DefaultPagingModel(page, pageSize, total, betsModel, nameof(GetBets)));

        }


        public IList<BetListModel> CreateBetsModel(IList<BetListElement> bets)
        {
            var betsModel = new List<BetListModel>();

            foreach (var bet in bets)
            {
                var betsModelElement = _mapper.Map<BetListModel>(bet);
                betsModelElement.Url = _generator.GetUriByName(HttpContext,
                        nameof(BetsController.GetBets),
                        new { id = bet.PlayerName });

                betsModel.Add(betsModelElement);
            }

            return betsModel;
        }
    }
}

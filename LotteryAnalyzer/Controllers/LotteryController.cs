using LotteryAnalyzer.Models;
using LotteryAnalyzer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LotteryAnalyzer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LotteryController : ControllerBase
    {
        #region Instance variables

        private LotteryService _service;

        #endregion
        #region Constructor
        public LotteryController(LotteryService service)
        {
            _service = service;

        }

        #endregion
        #region Public Methods

        // GET: api/v1/LotteryAnalysis?<query filter>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<Lottery> GetLottery([FromQuery] LotteryFilter filter)
        {
            Lottery lottery = null;
            ActionResult retval = BadRequest(filter);

            try
            {
                if (!ModelState.IsValid)
                {
                    retval = BadRequest(ModelState);
                }
                else
                {
                    lottery = _service.getLottery(filter, true);

                    if (lottery != null)
                    {
                        retval = new OkObjectResult(lottery);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retval;
        }

        #endregion
    }
}

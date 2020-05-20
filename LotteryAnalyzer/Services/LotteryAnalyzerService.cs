using LotteryAnalyzer.Models;
using LotteryAnalyzer.Services.Service_Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using static LotteryAnalyzer.Classes.Enumerations;

namespace LotteryAnalyzer.Services
{
    public class LotteryAnalyzerService
    {
        #region Private Variables

        private readonly IConfiguration _config;
        private readonly IHostEnvironment _env;

        #endregion
        #region Constructor

        public LotteryAnalyzerService(IConfiguration config, IHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        #endregion
        #region Public Methods

        public void AnalyzeLottery(Lottery lottery)
        {
            try
            {
                if (lottery != null)
                {
                    // Process the lottery
                    switch (lottery.LotteryDomain)
                    {
                        case LotteryDomain.Wclc:
                            WclcAnalyzer.ProcessWCLC(ref lottery);
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        #endregion
    }
}

using LotteryAnalyzer.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LotteryAnalyzer.Services
{
    public class DailyAnalysisService : IHostedService, IDisposable
    {
        // Constants
        #region Constants
        private const string STARTING_DAILY_ANALYSIS = "Starting Daily Analysis";
        private const string FINISHED_DAILY_ANALYSIS = "Finished Daily Analysis";
        #endregion
        #region Private Variables
        // Private Varibales
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly LotteryService _lotteryService;
        private readonly LotteryAnalyzerService _analyzerService;
        #endregion
        #region Public Methods
        public DailyAnalysisService(ILogger<DailyAnalysisService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;

            // Use this to get the scope of the services you need injected into the thread
            IServiceScope scope = serviceScopeFactory.CreateScope();

            IServiceProvider scopedServices = scope.ServiceProvider;

            _lotteryService = scopedServices.GetRequiredService<LotteryService>();

            _analyzerService = scopedServices.GetRequiredService<LotteryAnalyzerService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            // Timer has infinite timespan as it is supposed to run with application lifetime
            // This could be configure to run as a 24-hour scheduled task rather than infinite loop if required in the future
            _timer = new Timer(DoWork, null, TimeSpan.Zero, Timeout.InfiniteTimeSpan);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
        #endregion
        #region Private Methods
        private void DoWork(object state)
        {
            List<Lottery> lotteries = new List<Lottery>();

            LotteryFilter filter = new LotteryFilter();

            DateTime currentDay = DateTime.Now.AddDays(-1);

            try
            {
                // infinite loop
                while (true)
                {
                    // Its a new day. Restart the process
                    if (currentDay.Date < DateTime.Now.Date)
                    {
                        Console.WriteLine(STARTING_DAILY_ANALYSIS);

                        // Get the lotteries
                        lotteries = _lotteryService.getLotteries(filter);

                        if (lotteries != null && lotteries.Count > 0)
                        {
                            // Proccess the Lotteries
                            foreach (Lottery lottery in lotteries)
                            {
                                // Analyze the lottery
                                _analyzerService.AnalyzeLottery(lottery);

                                // Update the lottery
                                _lotteryService.updateLottery(lottery);
                            }
                        }
                        // update the date
                        currentDay = DateTime.UtcNow;

                        Console.WriteLine(FINISHED_DAILY_ANALYSIS);
                    }
                    // Once automatic processing is complete for the day, check every hour for new day 
                    Thread.Sleep(3600000);
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

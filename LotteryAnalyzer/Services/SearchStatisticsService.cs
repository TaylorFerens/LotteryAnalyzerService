using LotteryAnalyzer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace LotteryAnalyzer.Services
{
    public class SearchStatisticsService
    {

        #region Constants

        private const string UNKNOWN_DOMAIN_QUERIED = "Unkown domain has be searched, unable to query";

        #endregion
        #region Private Variables

        private readonly IConfiguration _config;
        private readonly IHostEnvironment _env;
        private readonly DatabaseContext _context;

        #endregion
        #region Constructor

        public SearchStatisticsService(IConfiguration config, IHostEnvironment env, DatabaseContext context)
        {
            _config = config;
            _env = env;
            _context = context;
        }

        #endregion
        #region Public Methods

        /// <summary>
        /// Update the stats for the searched domain
        /// </summary>
        /// <param name="domain"></param>
        public async void UpdateSearchStats(string LotteryId)
        {
            SearchStatistics statsToUpdate = null;
            IQueryable<SearchStatistics> query = null;

            try
            {
                if (LotteryId != null)
                {
                    //Define the query to get the stats
                    query = makeQuery(LotteryId);

                    //Get the statistic for the domain
                    statsToUpdate = query.FirstOrDefault();

                    //Increment the total times searched if there is history
                    if (statsToUpdate != null)
                    {
                        statsToUpdate.TotalTimesSearched++;

                        _context.SearchStatistics.Update(statsToUpdate);
                    }
                    else //This is a new domain
                    {
                        statsToUpdate = new SearchStatistics();
                        statsToUpdate.SearchStatisticId = Guid.NewGuid();
                        statsToUpdate.LotteryId = LotteryId;
                        //Start the total searches at 1 since it was currently being searched
                        statsToUpdate.TotalTimesSearched = 1;

                        _context.SearchStatistics.Add(statsToUpdate);
                    }

                    //Save changes
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion
        #region Private Methods

        /// <summary>
        /// Make the query to retrieve the statistic
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        private IQueryable<SearchStatistics> makeQuery(string LotteryId)
        {
            IQueryable<SearchStatistics> retval = null;

            try
            {
                retval = _context.SearchStatistics.Select(ss => ss);

                //Only query it out if we have a supported domain
                if (LotteryId != null)
                {
                    retval.Where(ss => ss.LotteryId == LotteryId);
                }
                else
                {
                    throw new Exception(UNKNOWN_DOMAIN_QUERIED);
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

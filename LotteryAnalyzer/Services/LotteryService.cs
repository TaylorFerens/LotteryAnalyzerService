using LotteryAnalyzer.Models;
using LotteryAnalyzer.Services.Service_Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LotteryAnalyzer.Services
{
    public class LotteryService
    {
        #region Constants
        #endregion
        #region Private Variables

        private readonly IConfiguration _config;
        private readonly IHostEnvironment _env;
        private readonly DatabaseContext _context;
        private readonly SearchStatisticsService _statsService;

        #endregion
        #region Constructor

        public LotteryService(IConfiguration config,
                              IHostEnvironment env,
                              DatabaseContext context,
                              SearchStatisticsService statsService)
        {
            _config = config;
            _env = env;
            _context = context;
            _statsService = statsService;
        }

        #endregion
        #region Public Methods

        /// <summary>
        /// Get One Lottery defined by id in filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Lottery getLottery(LotteryFilter filter, bool updateStatistics)
        {
            Lottery retval = null;
            IQueryable<Lottery> query = null;
            try
            {
                if (updateStatistics)
                {
                    _statsService.UpdateSearchStats(filter.LotteryId.ToString());
                }
                if (filter != null)
                {
                    query = getLotteryQuery(filter);

                    retval = query.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retval;
        }

        /// <summary>
        /// Get multiple lotteries determined by either domain, or just get all
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Lottery> getLotteries(LotteryFilter filter)
        {
            List<Lottery> retval = null;
            IQueryable<Lottery> query = null;
            try
            {
                if (filter != null)
                {
                    query = getLotteryQuery(filter);

                    retval = query.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retval;
        }

        /// <summary>
        /// Update the existing lottery
        /// </summary>
        /// <param name="lottery"></param>
        /// <returns></returns>
        public Lottery updateLottery(Lottery lottery)
        {
            Lottery retval = null;

            try
            {
                if (lottery != null)
                {
                    _context.Lottery.Update(lottery);

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retval;
        }

        /// <summary>
        /// Add the new lottery
        /// </summary>
        /// <param name="lottery"></param>
        /// <returns></returns>
        public Lottery addLottery(Lottery lottery)
        {
            try
            {
                if (lottery != null)
                {
                    // Make sure it has an id
                    if (lottery.LotteryId == null)
                    {
                        lottery.LotteryId = Guid.NewGuid();
                        lottery.LotteryNumbers = SharedMethods.InitializeLotteryNumbers(lottery.LotteryId);
                    }

                    _context.Lottery.Add(lottery);

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return lottery;
        }

        #endregion
        #region Private Methods

        private IQueryable<Lottery> getLotteryQuery(LotteryFilter filter)
        {
            IQueryable<Lottery> query = null;

            try
            {
                query = _context.Lottery.Select(l => l);

                query.Include(l => l.LotteryNumbers);

                if (filter.LotteryId != null)
                {
                    query.Where(l => l.LotteryId == filter.LotteryId);
                }

                if (filter.Domain != Classes.Enumerations.LotteryDomain.Unknown)
                {
                   // query.Where(l => l.LotteryDomain == filter.Domain);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return query;
        }

        #endregion
    }
}

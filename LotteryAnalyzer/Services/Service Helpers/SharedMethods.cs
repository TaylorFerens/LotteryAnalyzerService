using LotteryAnalyzer.Models;
using System;
using System.Collections.Generic;

namespace LotteryAnalyzer.Services.Service_Helpers
{
    public static class SharedMethods
    {
        #region Private Variables

        private const int MAX_LOTTERY_NUMBER = 49;

        #endregion
        #region Public Methods
        public static List<LotteryNumber> InitializeLotteryNumbers(Guid? lotteryId)
        {
            List<LotteryNumber> retval = new List<LotteryNumber>();
            int currentNumber = 1;

            try
            {
                retval = new List<LotteryNumber>();

                //Create a new entry from 1 - 50 for the collection
                while (currentNumber <= MAX_LOTTERY_NUMBER)
                {
                    retval.Add(new LotteryNumber
                    {
                        LotteryNumberId = Guid.NewGuid(),
                        Number = currentNumber.ToString(),
                        TimesPicked = 0,
                        LotteryId = lotteryId
                    }); ;
                    currentNumber++;
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

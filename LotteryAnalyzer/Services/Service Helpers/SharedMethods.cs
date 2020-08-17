using LotteryAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static LotteryAnalyzer.Classes.Constants;

namespace LotteryAnalyzer.Services.Service_Helpers
{
    public static class SharedMethods
    {
        #region Private Variables

        private const int MAX_LOTTERY_NUMBER = 49;

        #endregion
        #region Public Methods
        public static List<LotteryNumber> InitializeLotteryNumbers(Lottery lottery)
        {
            List<LotteryNumber> retval = new List<LotteryNumber>();
            int currentNumber = 1;

            try
            {
                retval = new List<LotteryNumber>();

                //Create a new entry from 1 - 50 for the collection
                while (currentNumber <= lottery.MaxlotteryNumber)
                {
                    retval.Add(new LotteryNumber
                    {
                        LotteryNumberId = Guid.NewGuid(),
                        Number = currentNumber.ToString(),
                        TimesPicked = 0,
                        LotteryId = lottery.LotteryId
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

        // We just want to see if a line as a date, this is tricky to do, since we have so many different possible formats
        // Since this method is only called once we have determined have a hit the designated draw tag tag, we can assume the following:
        // we are itterating line by line look for a date. Since thats the case there is one sure fire way to determine if a line of 
        // Html contains a date, and that is check for a 4 digit number (a year), we cant check for months or days (1 or 2 digits) as those could be lottery numbers
        public static bool HasDate(string html)
        {
            bool retval = false;

            try
            {
                if (!String.IsNullOrEmpty(html))
                {
                    retval = !String.IsNullOrEmpty(Regex.Match(html, @"\d{4}").Value);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return retval;
        }

        public static DateTime ParseDateFromHtml(string html)
        {
            // TO DO: write logic that can extract a date from a line of html. Must handle all possible date formats
            DateTime retval = new DateTime(1899, 1, 1);

            try
            {
                // First attempt to get out a medium or long formatted date
                DateTime.TryParse(Regex.Match(MEDIUM_OR_LONG_DATE_REGEX, html).Value, out retval);

                // If the year is still 1899, it didnt work, and we should try a short date format
                if(retval.Year == 1899)
                {
                    DateTime.TryParse(Regex.Match(SHORT_DATE_REGEX, html).Value, out retval);
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

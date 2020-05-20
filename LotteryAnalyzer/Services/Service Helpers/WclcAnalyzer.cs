using LotteryAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LotteryAnalyzer.Services.Service_Helpers
{
    public static class WclcAnalyzer
    {
        #region Constants

        public const string LOTTERY_NUMBERS = "Lottery Numbers";
        public const string PAST_WIN_NUMBER = "pastWinNumber";
        public const string PAST_WIN_NUMBER_MM = "pastWinNumMM";
        public const string PAST_WIN_DATE = "pastWinNumDate";

        #endregion
        #region "Public Methods"
        public static void ProcessWCLC(ref Lottery lottery)
        {
            try
            {
                getWinningNumbersFromURL(ref lottery);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion
        #region private methods
        private static void getWinningNumbersFromURL(ref Lottery lottery)
        {
            Stream stream = null;
            StreamReader reader = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            DateTime drawDate = new DateTime(1899, 1, 1);

            bool endLoop = true;
            try
            {
                //check if URL is null or empty
                if (!String.IsNullOrEmpty(lottery.LotteryUrl))
                {
                    //validate the URL
                    request = (HttpWebRequest)WebRequest.Create(lottery.LotteryUrl);
                    response = (HttpWebResponse)request.GetResponse();

                    //check URL status
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        stream = response.GetResponseStream();
                        reader = null;

                        if (response.CharacterSet == null)
                        {
                            reader = new StreamReader(stream);
                        }
                        else
                        {
                            reader = new StreamReader(stream, Encoding.GetEncoding(response.CharacterSet));
                        }

                        string currentLine = reader.ReadLine();

                        List<int> currentWinningNumbers = new List<int>();
                        do
                        {
                            if (currentLine != null)
                            {
                                // Get the draw date
                                if (currentLine.Contains(PAST_WIN_DATE))
                                {
                                    drawDate = processDrawDate(ref reader);
                                }
                                // If the draw dates don't match, there is a new draw
                                if (drawDate != new DateTime(1899, 1, 1) &&
                                    drawDate.Date != lottery.LastDrawDate.Date)
                                {
                                    processWinningNumbers(ref lottery, ref reader);
                                    lottery.LastDrawDate = drawDate;
                                    // We should now have the latest and should stop reading the page
                                    endLoop = false;
                                }
                            }

                            currentLine = reader.ReadLine();

                        } while (currentLine != null && endLoop);

                        response.Close();
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static DateTime processDrawDate(ref StreamReader reader)
        {
            DateTime retval = new DateTime(1899, 1, 1);
            string currentLine = null;
            try
            {
                // Skip h4 line
                currentLine = reader.ReadLine();
                currentLine = reader.ReadLine();

                // Now on the line with the date
                retval = Convert.ToDateTime(currentLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return retval;
        }

        private static void processWinningNumbers(ref Lottery lottery, ref StreamReader reader)
        {
            string winningNumber = null;
            LotteryNumber currNumber = null;
            string currentLine = null;

            try
            {
                // Skip lines to lottery numbers
                currentLine = reader.ReadLine();
                currentLine = reader.ReadLine();
                currentLine = reader.ReadLine();
                currentLine = reader.ReadLine();
                // Get the winning Numbers
                while ((currentLine.Contains(PAST_WIN_NUMBER) || currentLine.Contains(PAST_WIN_NUMBER_MM)))
                {
                    winningNumber = Regex.Match(currentLine, @"\d+").Value;

                    // If winning numbers were found, add it to the collection
                    if (!String.IsNullOrEmpty(winningNumber))
                    {
                        // Find the winning number in the collection, and increment its frequency
                        if (lottery.LotteryNumbers != null)
                        {
                            currNumber = lottery.LotteryNumbers.Find(n => n.Number.Equals(winningNumber));
                        }

                        if (currNumber != null)
                        {
                            currNumber.TimesPicked++;
                        }
                        else // Number doesnt exist and must be added
                        {
                            lottery.LotteryNumbers.Add(new LotteryNumber
                            {
                                Number = winningNumber,
                                LotteryId = lottery.LotteryId,
                                TimesPicked = 1
                            });
                        }
                    }
                    currentLine = reader.ReadLine();
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

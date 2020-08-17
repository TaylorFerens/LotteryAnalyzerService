using LotteryAnalyzer.Models;
using LotteryAnalyzer.Services.Service_Helpers;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using LotteryAnalyzer.Classes;
using System.Text.RegularExpressions;

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
                    getWinningNumbersFromURL(ref lottery);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        #endregion
        #region Private Methods

        private void getWinningNumbersFromURL(ref Lottery lottery)
        {
            Stream stream = null;
            StreamReader reader = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            DateTime drawDate = new DateTime(1899, 1, 1);
            string drawDateTag = null;
            ICollection<LotteryHtmlTag> winningNumberTags = null;

            bool endLoop = true;
            try
            {
                // Make sure we have all the necessary tags to perform this all
                drawDateTag = lottery.DrawDateTagBroker.LotteryTags.FirstOrDefault().HtmlTag;
                winningNumberTags = lottery.LotteryNumberHtmlTagBroker.LotteryTags;

                if (!String.IsNullOrEmpty(drawDateTag) && (winningNumberTags != null && winningNumberTags.Count > 0))
                {
                    // check if URL is null or empty
                    if (!String.IsNullOrEmpty(lottery.LotteryUrl))
                    {
                        // validate the URL
                        request = (HttpWebRequest)WebRequest.Create(lottery.LotteryUrl);
                        response = (HttpWebResponse)request.GetResponse();

                        // check URL status
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
                                    if (currentLine.Contains(drawDateTag))
                                    {
                                        drawDate = processDrawDate(ref reader, currentLine);
                                    }
                                    // If the draw dates don't match, there is a new draw
                                    if (drawDate != new DateTime(1899, 1, 1) &&
                                        drawDate.Date != lottery.LastDrawDate.Date)
                                    {
                                        processWinningNumbers(ref lottery, currentLine, ref reader, winningNumberTags);
                                        lottery.LastDrawDate = drawDate;
                                        // We should now have the latest and should stop reading the page
                                        endLoop = false;
                                    }
                                }

                                currentLine = reader.ReadLine();

                            } while (currentLine != null && endLoop);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                response.Dispose();
                reader.Dispose();
            }
        }

        private DateTime processDrawDate(ref StreamReader reader, string currentLine)
        {
            DateTime retval = new DateTime(1899, 1, 1);

            try
            {
                // try to extract the date, we need this to determine if we already have the latest numbers
                do
                {
                    if (SharedMethods.HasDate(currentLine))
                    {
                        // Now on the line with the date
                        retval = Convert.ToDateTime(SharedMethods.ParseDateFromHtml(currentLine));
                    }
                    else
                    {
                        currentLine = reader.ReadLine();
                    }
                } while (retval.Year == 1899 || currentLine != null);

                // Now on the line with the date
                retval = Convert.ToDateTime(currentLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return retval;
        }

        private void processWinningNumbers(ref Lottery lottery,string currentLine, ref StreamReader reader, ICollection<LotteryHtmlTag> winningNumberTags)
        {
            string winningNumber = null;
            LotteryNumber currNumber = null;
            int totalDrawNumbers;
            int numbersFound = 0;
            try
            {
                totalDrawNumbers = lottery.TotalNumberDraws;

                // If we have a bonus, add one more draw
                if (lottery.HasBonus)
                {
                    totalDrawNumbers++;
                }

                // Get the winning Numbers
                while (numbersFound < totalDrawNumbers || currentLine != null)
                {
                    winningNumber = Regex.Match(currentLine, @"\d+").Value;

                    // If winning numbers were found, add it to the collection
                    if (!String.IsNullOrEmpty(winningNumber))
                    {
                        // Find the winning number in the collection, and increment its frequency
                        if (lottery.LotteryNumbers != null)
                        {
                            currNumber = lottery.LotteryNumbers.FirstOrDefault(n => n.Number.Equals(winningNumber));
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

                        numbersFound++;
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

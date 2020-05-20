using System;

namespace LotteryAnalyzer.Classes
{
    //Shared constants for entire program are found in this class
    public class Constants
    {

        public const string LOTTERY_NUMBERS = "Lottery Numbers";
        public const string TIMES_PICKED = "Times Picked";
        public const string WCLC_PAST_WIN_NUMBER = "pastWinNumber";
        public const string WCLCPAST_WIN_NUMBER_MM = "pastWinNumMM";

        public DateTime DEFAULT_DATE = new DateTime(1899, 1, 1);


        #region Supported Uris

        public const string WCLC_HOST = "www.wclc.com";

        #endregion
    }
}

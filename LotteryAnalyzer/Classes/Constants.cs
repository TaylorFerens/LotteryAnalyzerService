using System;
using System.Collections.Generic;

namespace LotteryAnalyzer.Classes
{
    //Shared constants for entire program are found in this class
    public class Constants
    {
        public DateTime DEFAULT_DATE = new DateTime(1899, 1, 1);

        public const List<string> DAYS_OF_WEEK = null;
        
        #region Supported Uris

        public const string WCLC_HOST = "www.wclc.com";

        #endregion

        #region Regular Expressions

        public const string SHORT_DATE_REGEX = @"^\d{1,2}\/\d{1,2}\/\d{4}";

        public const string MEDIUM_OR_LONG_DATE_REGEX = @"^(?:(((Jan(uary)?|Ma(r(ch)?|y)|Jul(y)?|Aug(ust)?|Oct(ober)?|Dec(ember)?)\ 31)|((Jan(uary)?|Ma(r(ch)?|y)|Apr(il)?|Ju((ly?)|(ne?))|Aug(ust)?|Oct(ober)?|(Sept|Nov|Dec)(ember)?)\ (0?[1-9]|([12]\d)|30))|(Feb(ruary)?\ (0?[1-9]|1\d|2[0-8]|(29(?=,\ ((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))))\,\ ((1[6-9]|[2-9]\d)\d{2})";

        #endregion
    }
}

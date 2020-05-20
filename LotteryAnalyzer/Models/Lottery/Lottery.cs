using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LotteryAnalyzer.Classes.Enumerations;

namespace LotteryAnalyzer.Models
{
    public class Lottery
    {
        #region Public Variables
        [Key]
        public Guid? LotteryId { get; set; }
        public string LotteryName { get; set; }
        public string LotteryUrl { get; set; }
        public LotteryDomain LotteryDomain { get; set; }
        public DateTime LastDrawDate { get; set; }

        // One to many relationship between lottery and lottery numbers
        [NotMapped]
        public List<LotteryNumber> LotteryNumbers { get; set; } = new List<LotteryNumber>();
        #endregion
    }
}

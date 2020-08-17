
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
        public int MaxlotteryNumber { get; set; }
        public int TotalNumberDraws { get; set; }
        public bool HasBonus { get; set; }
        public DateTime LastDrawDate { get; set; }

        // Foreign Keys
        public Guid? LotteryDateTagBrokerId { get; set; }
        public Guid? LotteryHtmlTagBrokerId { get; set; }


        // Foreign Entities
        public ICollection<LotteryNumber> LotteryNumbers { get; set; }

        [ForeignKey("LotteryHtmlTagBrokerId")]
        public LotteryHtmlTagBroker LotteryNumberHtmlTagBroker { get; set; }

        [ForeignKey("LotteryDateTagId")]
        public LotteryHtmlTagBroker DrawDateTagBroker { get; set; }
        #endregion
    }
}

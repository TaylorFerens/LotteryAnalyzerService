using System;
using System.ComponentModel.DataAnnotations;

namespace LotteryAnalyzer.Models
{
    public class SearchStatistics
    {
        [Key]
        public Guid? SearchStatisticId { get; set; }
        public string LotteryId { get; set; }
        public int TotalTimesSearched { get; set; }

    }
}

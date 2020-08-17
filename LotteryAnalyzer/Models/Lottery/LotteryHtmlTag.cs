using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LotteryAnalyzer.Models
{
    public class LotteryHtmlTag
    {

        public Guid? LotteryHtmlTagId { get; set; }
        public Guid? LotteryHtmlTagBrokerId { get; set; }
        public string HtmlTag { get; set; }

        // Foreign Entities
        [ForeignKey("LotteryHtmlTagBrokerId")]
        public LotteryHtmlTagBroker HtmlTagBroker { get; set; }
    }
}

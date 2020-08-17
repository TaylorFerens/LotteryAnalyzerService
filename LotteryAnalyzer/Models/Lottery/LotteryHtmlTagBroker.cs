using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotteryAnalyzer.Models
{
    public class LotteryHtmlTagBroker
    {
       public  Guid? LotteryHtmlTagBrokerId { get; set; }

       public ICollection<LotteryHtmlTag> LotteryTags { get; set; }
        public ICollection<Lottery> Lotteries { get; set; }

    }
}

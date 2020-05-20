﻿using System;
using static LotteryAnalyzer.Classes.Enumerations;

namespace LotteryAnalyzer.Models
{
    public class LotteryFilter
    {
        public Guid? LotteryId { get; set; }
        public LotteryDomain Domain { get; set; } = LotteryDomain.Unknown;

    }
}

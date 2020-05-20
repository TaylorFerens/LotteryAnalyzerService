using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LotteryAnalyzer.Models
{
    public class LotteryNumber
    {
        #region Public Variables
        [Key]
        public Guid? LotteryNumberId { get; set; }
        [Required]
        public Guid? LotteryId { get; set; }
        public string Number { get; set; }
        public int TimesPicked { get; set; }

        // Many to one relationship with lottery
        [NotMapped]
        public Lottery Lottery { get; set; }
        #endregion
    }
}

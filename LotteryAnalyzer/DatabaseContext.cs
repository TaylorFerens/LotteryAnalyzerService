using LotteryAnalyzer.Models;
using LotteryAnalyzer.Services.Service_Helpers;
using Microsoft.EntityFrameworkCore;
using System;

namespace LotteryAnalyzer
{
    public class DatabaseContext : DbContext
    {

        #region DataSets

        public DbSet<SearchStatistics> SearchStatistics { get; set; }
        public DbSet<Lottery> Lottery { get; set; }
        public DbSet<LotteryNumber> LotteryNumbers { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion
        #region Constructor

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        #endregion
        #region Override Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetupLotterySystem(ref modelBuilder);

            SetupDatabaseSeedData(ref modelBuilder);
        }

        #endregion

        #region Setup seeded data
        private void SetupDatabaseSeedData(ref ModelBuilder modelBuilder)
        {
            SeedWclcLotteries(ref modelBuilder);
        }

        #endregion
        #region Private Methods

        private void SetupLotterySystem(ref ModelBuilder modelBuilder)
        {
            //Set up one to many relationship between lotteries and numbers
            modelBuilder.Entity<Lottery>()
                .HasMany(l => l.LotteryNumbers)
                .WithOne(ln => ln.Lottery);
        }

        private void SeedWclcLotteries(ref ModelBuilder modelBuilder)
        {
            // Some starting lotteries for supported analyzer
            modelBuilder.Entity<Lottery>().HasData(new Models.Lottery
            {
                LotteryId = Guid.NewGuid(),
                LotteryName = "Lotto Max",
                LotteryDomain = Classes.Enumerations.LotteryDomain.Wclc,
                LotteryUrl = "https://www.wclc.com/winning-numbers/lotto-max-extra.htm"
            });

            modelBuilder.Entity<Lottery>().HasData(new Models.Lottery
            {
                LotteryId = Guid.NewGuid(),
                LotteryName = "Lotto 649",
                LotteryDomain = Classes.Enumerations.LotteryDomain.Wclc,
                LotteryUrl = "https://www.wclc.com/winning-numbers/lotto-649-extra.htm"
            });

            modelBuilder.Entity<Lottery>().HasData(new Models.Lottery
            {
                LotteryId = Guid.NewGuid(),
                LotteryName = "Western 649",
                LotteryDomain = Classes.Enumerations.LotteryDomain.Wclc,
                LotteryUrl = "https://www.wclc.com/winning-numbers/Western-649-extra.htm"
            });


        }

        #endregion
    }
}

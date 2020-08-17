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
        public DbSet<LotteryHtmlTag> LotteryHtmlTags { get; set; }
        public DbSet<LotteryHtmlTagBroker> LotteryHtmlTagBrokers { get; set; }

        #endregion
        #region Constructor

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        #endregion
        #region Override Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetupLotterySystem(ref modelBuilder);
            SetHtmlTagRelationship(ref modelBuilder);
            SetTagAndLotteryRelationship(ref modelBuilder);

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

            modelBuilder.Entity<LotteryNumber>()
                .HasOne(ln => ln.Lottery)
                .WithMany(l => l.LotteryNumbers)
                .HasForeignKey(ln => ln.LotteryId);
        }


        private void SetHtmlTagRelationship(ref ModelBuilder modelBuilder)
        {
            // Set up relationship between LotteryHtmlTags and their brokers
            modelBuilder.Entity<LotteryHtmlTagBroker>()
                 .HasMany(ltb => ltb.LotteryTags)
                 .WithOne(lt => lt.HtmlTagBroker)
                 .HasForeignKey(lt => lt.LotteryHtmlTagBrokerId);

            modelBuilder.Entity<LotteryHtmlTag>()
                .HasOne(lt => lt.HtmlTagBroker)
                .WithMany(p => p.LotteryTags);
        }

        private void SetTagAndLotteryRelationship(ref ModelBuilder modelBuilder)
        {
            // Set up relationship between LotteryHtmlTags and lotteries
            modelBuilder.Entity<Lottery>()
                 .HasOne(l => l.LotteryNumberHtmlTagBroker)
                 .WithMany(ltb => ltb.Lotteries)
                 .HasForeignKey(l => l.LotteryHtmlTagBrokerId);

            modelBuilder.Entity<LotteryHtmlTagBroker>()
                .HasMany(ltb => ltb.Lotteries)
                .WithOne(l => l.LotteryNumberHtmlTagBroker);
        }

        private void SeedWclcLotteries(ref ModelBuilder modelBuilder)
        {
            // Some starting lotteries for supported analyzer
            modelBuilder.Entity<Lottery>().HasData(new Models.Lottery
            {
                LotteryId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e86"),
                LotteryName = "Lotto Max",
                MaxlotteryNumber = 49,
                HasBonus = true,
                TotalNumberDraws = 7,
                LotteryDateTagBrokerId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84"),
                LotteryHtmlTagBrokerId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"),
                LotteryUrl = "https://www.wclc.com/winning-numbers/lotto-max-extra.htm"
            });

            modelBuilder.Entity<Lottery>().HasData(new Models.Lottery
            {
                LotteryId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e87"),
                LotteryName = "Lotto 649",
                MaxlotteryNumber = 49,
                HasBonus = true,
                TotalNumberDraws = 6,
                LotteryDateTagBrokerId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84"),
                LotteryHtmlTagBrokerId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"),
                LotteryUrl = "https://www.wclc.com/winning-numbers/lotto-649-extra.htm"
            });

            modelBuilder.Entity<Lottery>().HasData(new Models.Lottery
            {
                LotteryId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e88"),
                LotteryName = "Western 649",
                MaxlotteryNumber = 49,
                HasBonus = true,
                TotalNumberDraws = 6,
                LotteryDateTagBrokerId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84"),
                LotteryHtmlTagBrokerId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83"),
                LotteryUrl = "https://www.wclc.com/winning-numbers/Western-649-extra.htm"
            });

            modelBuilder.Entity<LotteryNumber>().HasData(new Models.LotteryNumber
            {
                LotteryNumberId = Guid.NewGuid(),
                LotteryId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e86"),
                Number = "1"

            });

            modelBuilder.Entity<LotteryNumber>().HasData(new Models.LotteryNumber
            {
                LotteryNumberId = Guid.NewGuid(),
                LotteryId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e87"),
                Number = "1"

            });

            modelBuilder.Entity<LotteryNumber>().HasData(new Models.LotteryNumber
            {
                LotteryNumberId = Guid.NewGuid(),
                LotteryId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e88"),
                Number = "1"

            });


            modelBuilder.Entity<LotteryHtmlTagBroker>().HasData(new LotteryHtmlTagBroker
            {
                LotteryHtmlTagBrokerId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83")
            });

            modelBuilder.Entity<LotteryHtmlTagBroker>().HasData(new LotteryHtmlTagBroker
            {
                LotteryHtmlTagBrokerId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84")
            });


            modelBuilder.Entity<LotteryHtmlTag>().HasData(new LotteryHtmlTag { 
                LotteryHtmlTagId = new Guid("6fa14576-2c64-4fd6-892f-eb000bc6cae9"),
                HtmlTag = "pastWinNumber",
                LotteryHtmlTagBrokerId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e83")
            });

            modelBuilder.Entity<LotteryHtmlTag>().HasData(new LotteryHtmlTag
            {
                LotteryHtmlTagId = new Guid("6fa14576-2c64-4fd6-892f-eb000bc6cae0"),
                HtmlTag = "pastWinNumDate",
                LotteryHtmlTagBrokerId = new Guid("80f2cc4d-8477-49c3-9dc9-91ded3d84e84")
            });


        }

        #endregion
    }
}

﻿// <auto-generated />
using System;
using LotteryAnalyzer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LotteryAnalyzer.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200516191607_ADD_LOTTERY_AND_LOTTERY_NUMBER")]
    partial class ADD_LOTTERY_AND_LOTTERY_NUMBER
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("LotteryAnalyzer.Models.Lottery", b =>
                {
                    b.Property<Guid?>("LotteryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastDrawDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("LotteryDomain")
                        .HasColumnType("integer");

                    b.Property<string>("LotteryName")
                        .HasColumnType("text");

                    b.Property<string>("LotteryUrl")
                        .HasColumnType("text");

                    b.HasKey("LotteryId");

                    b.ToTable("Lottery");
                });

            modelBuilder.Entity("LotteryAnalyzer.Models.LotteryNumber", b =>
                {
                    b.Property<Guid?>("LotteryNumberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("LotteryId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<int>("TimesPicked")
                        .HasColumnType("integer");

                    b.HasKey("LotteryNumberId");

                    b.HasIndex("LotteryId");

                    b.ToTable("LotteryNumbers");
                });

            modelBuilder.Entity("LotteryAnalyzer.Models.SearchStatistics", b =>
                {
                    b.Property<Guid?>("SearchStatisticId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("LotteryDomain")
                        .HasColumnType("integer");

                    b.Property<int>("TotalTimesSearched")
                        .HasColumnType("integer");

                    b.HasKey("SearchStatisticId");

                    b.ToTable("SearchStatistics");
                });

            modelBuilder.Entity("LotteryAnalyzer.Models.LotteryNumber", b =>
                {
                    b.HasOne("LotteryAnalyzer.Models.Lottery", "Lottery")
                        .WithMany("LotteryNumbers")
                        .HasForeignKey("LotteryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

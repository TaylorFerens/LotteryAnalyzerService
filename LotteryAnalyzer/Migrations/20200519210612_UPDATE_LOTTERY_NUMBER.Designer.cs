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
    [Migration("20200519210612_UPDATE_LOTTERY_NUMBER")]
    partial class UPDATE_LOTTERY_NUMBER
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

                    b.HasData(
                        new
                        {
                            LotteryId = new Guid("53338e29-399e-4ffd-a336-0986baff0d34"),
                            LastDrawDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LotteryDomain = 1,
                            LotteryName = "Lotto Max",
                            LotteryUrl = "https://www.wclc.com/winning-numbers/lotto-max-extra.htm"
                        },
                        new
                        {
                            LotteryId = new Guid("4329ea67-0832-4248-9ba6-7881fb4e566c"),
                            LastDrawDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LotteryDomain = 1,
                            LotteryName = "Lotto 649",
                            LotteryUrl = "https://www.wclc.com/winning-numbers/lotto-649-extra.htm"
                        },
                        new
                        {
                            LotteryId = new Guid("c26a608f-b486-4a14-9b1f-684bdcb06d84"),
                            LastDrawDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LotteryDomain = 1,
                            LotteryName = "Western 649",
                            LotteryUrl = "https://www.wclc.com/winning-numbers/Western-649-extra.htm"
                        });
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

                    b.Property<string>("LotteryId")
                        .HasColumnType("text");

                    b.Property<int>("TotalTimesSearched")
                        .HasColumnType("integer");

                    b.HasKey("SearchStatisticId");

                    b.ToTable("SearchStatistics");
                });

            modelBuilder.Entity("LotteryAnalyzer.Models.User", b =>
                {
                    b.Property<Guid?>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("UserId");

                    b.ToTable("Users");
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

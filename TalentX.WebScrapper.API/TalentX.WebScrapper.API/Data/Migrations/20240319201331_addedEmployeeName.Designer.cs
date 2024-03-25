﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalentX.WebScrapper.API.Data;

#nullable disable

namespace TalentX.WebScrapper.API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240319201331_addedEmployeeName")]
    partial class addedEmployeeName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.28");

            modelBuilder.Entity("TalentX.WebScrapper.API.Entities.DetailedScrapOutputData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AllabolagUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CEO")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OrgNo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Revenue")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("YearOfEstablishment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("employeeNames")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DetailedScrapOutputData");
                });

            modelBuilder.Entity("TalentX.WebScrapper.API.Entities.InitialScrapOutputData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("InitialScrapOutputData");
                });
#pragma warning restore 612, 618
        }
    }
}

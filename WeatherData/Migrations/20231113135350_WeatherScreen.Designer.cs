﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherData;

#nullable disable

namespace WeatherData.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231113135350_WeatherScreen")]
    partial class WeatherScreen
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WeatherData.Entities.CountryCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CountryCodes");
                });

            modelBuilder.Entity("WeatherData.Entities.WeatherScreen", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Humidity")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ScreenTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("Temperature")
                        .HasColumnType("float");

                    b.Property<double>("TemperatureFeelsLike")
                        .HasColumnType("float");

                    b.Property<double>("TemperatureMax")
                        .HasColumnType("float");

                    b.Property<double>("TemperatureMin")
                        .HasColumnType("float");

                    b.Property<string>("WeatherDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WindSpeed")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("WeatherScreens");
                });
#pragma warning restore 612, 618
        }
    }
}
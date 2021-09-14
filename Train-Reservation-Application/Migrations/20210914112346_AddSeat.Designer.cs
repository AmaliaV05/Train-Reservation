﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Train_Reservation_Application.Data;

namespace Train_Reservation_Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210914112346_AddSeat")]
    partial class AddSeat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Train_Reservation_Application.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarNumber")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<int?>("TrainId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrainId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Seat");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Train", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trains");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Car", b =>
                {
                    b.HasOne("Train_Reservation_Application.Models.Train", "Train")
                        .WithMany("Cars")
                        .HasForeignKey("TrainId");

                    b.Navigation("Train");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Seat", b =>
                {
                    b.HasOne("Train_Reservation_Application.Models.Car", "Car")
                        .WithMany("Seats")
                        .HasForeignKey("CarId");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Car", b =>
                {
                    b.Navigation("Seats");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Train", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}

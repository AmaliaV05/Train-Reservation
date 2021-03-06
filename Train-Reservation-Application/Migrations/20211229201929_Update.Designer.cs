// <auto-generated />
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
    [Migration("20211229201929_Update")]
    partial class Update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReservationSeat", b =>
                {
                    b.Property<int>("ReservationsId")
                        .HasColumnType("int");

                    b.Property<int>("SeatsId")
                        .HasColumnType("int");

                    b.HasKey("ReservationsId", "SeatsId");

                    b.HasIndex("SeatsId");

                    b.ToTable("ReservationSeat");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Calendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CalendarDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Calendars");
                });

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

            modelBuilder.Entity("Train_Reservation_Application.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialSecurityNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Reservations");
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

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.SeatCalendar", b =>
                {
                    b.Property<int>("CalendarId")
                        .HasColumnType("int");

                    b.Property<int>("SeatId")
                        .HasColumnType("int");

                    b.Property<bool>("SeatAvailability")
                        .HasColumnType("bit");

                    b.HasKey("CalendarId", "SeatId");

                    b.HasIndex("SeatId");

                    b.ToTable("SeatCalendar");
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

            modelBuilder.Entity("ReservationSeat", b =>
                {
                    b.HasOne("Train_Reservation_Application.Models.Reservation", null)
                        .WithMany()
                        .HasForeignKey("ReservationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Train_Reservation_Application.Models.Seat", null)
                        .WithMany()
                        .HasForeignKey("SeatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Car", b =>
                {
                    b.HasOne("Train_Reservation_Application.Models.Train", "Train")
                        .WithMany("Cars")
                        .HasForeignKey("TrainId");

                    b.Navigation("Train");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Reservation", b =>
                {
                    b.HasOne("Train_Reservation_Application.Models.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Seat", b =>
                {
                    b.HasOne("Train_Reservation_Application.Models.Car", "Car")
                        .WithMany("Seats")
                        .HasForeignKey("CarId");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.SeatCalendar", b =>
                {
                    b.HasOne("Train_Reservation_Application.Models.Calendar", "Calendar")
                        .WithMany("SeatCalendars")
                        .HasForeignKey("CalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Train_Reservation_Application.Models.Seat", "Seat")
                        .WithMany("SeatCalendars")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calendar");

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Calendar", b =>
                {
                    b.Navigation("SeatCalendars");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Car", b =>
                {
                    b.Navigation("Seats");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Seat", b =>
                {
                    b.Navigation("SeatCalendars");
                });

            modelBuilder.Entity("Train_Reservation_Application.Models.Train", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}

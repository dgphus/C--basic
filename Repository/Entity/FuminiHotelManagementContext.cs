using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Entity;

public class FuminiHotelManagementContext : DbContext
{
    public FuminiHotelManagementContext()
    {
    }
    public FuminiHotelManagementContext(DbContextOptions<FuminiHotelManagementContext> options) : base(options) { }

    public DbSet<BookingDetail> BookingDetails { get; set; }
    public DbSet<BookingReservation> BookingReservations { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<RoomInformation> RoomInformations { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDB"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookingDetail>()
                    .HasKey(bd => new { bd.BookingReservationId, bd.RoomId }); 

        modelBuilder.Entity<BookingDetail>()
                    .HasOne(bd => bd.BookingReservation)
                    .WithMany(br => br.BookingDetails)
                    .HasForeignKey(bd => bd.BookingReservationId)
                    .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<BookingDetail>()
                    .HasOne(bd => bd.Room)
                    .WithMany(r => r.BookingDetails)
                    .HasForeignKey(bd => bd.RoomId)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomInformation>()
                    .Property(r => r.RoomPricePerDay)
                    .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<BookingDetail>()
                    .Property(r => r.ActualPrice)
                    .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<BookingReservation>()
                    .Property(r => r.TotalPrice)
                    .HasColumnType("decimal(18,2)");


        modelBuilder.Entity<RoomType>().HasData(
            new RoomType { RoomTypeId = 1, RoomTypeName = "Standard", TypeDescription = "Basic room", TypeNote = "No note" },
            new RoomType { RoomTypeId = 2, RoomTypeName = "Deluxe", TypeDescription = "Luxurious room", TypeNote = "Includes breakfast" }
        );

        modelBuilder.Entity<RoomInformation>().HasData(
            new RoomInformation { RoomId = 1, RoomNumber = "101", RoomDetailDescription = "A standard room", RoomMaxCapacity = 2, RoomTypeId = 1, RoomStatus = 1, RoomPricePerDay = 100 },
            new RoomInformation { RoomId = 2, RoomNumber = "102", RoomDetailDescription = "A deluxe room", RoomMaxCapacity = 3, RoomTypeId = 2, RoomStatus = 1, RoomPricePerDay = 150 }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, CustomerFullName = "John Doe", Telephone = "123456789", EmailAddress = "johndoe@example.com", CustomerBirthday = new DateOnly(1990, 1, 1), CustomerStatus = 1, Password = "password123" },
            new Customer { CustomerId = 2, CustomerFullName = "Jane Smith", Telephone = "987654321", EmailAddress = "janesmith@example.com", CustomerBirthday = new DateOnly(1985, 5, 15), CustomerStatus = 1, Password = "password456" }
        );

        modelBuilder.Entity<BookingReservation>().HasData(
            new BookingReservation { BookingReservationId = 1, BookingDate = new DateOnly(2023, 9, 1), TotalPrice = 300, CustomerId = 1, BookingStatus = 1 },
            new BookingReservation { BookingReservationId = 2, BookingDate = new DateOnly(2023, 9, 5), TotalPrice = 450, CustomerId = 2, BookingStatus = 2 }
        );

        modelBuilder.Entity<BookingDetail>().HasData(
            new BookingDetail { BookingReservationId = 1, RoomId = 1, StartDate = new DateOnly(2023, 9, 1), EndDate = new DateOnly(2023, 9, 3), ActualPrice = 200 },
            new BookingDetail { BookingReservationId = 2, RoomId = 2, StartDate = new DateOnly(2023, 9, 5), EndDate = new DateOnly(2023, 9, 8), ActualPrice = 400 }
        );
    }
}

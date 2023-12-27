using Microsoft.EntityFrameworkCore;

namespace Lab3
{
    public class ModelDB:DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Renter>? Renters { get; set; }
        public DbSet<PriceList>? PriceLists { get; set; }
        public DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceList>().HasData(
                new PriceList { Id = 1,ApartType="One-room",PricePerMeter=2000, Utilities=12500},
                new PriceList { Id = 2,ApartType = "Two-room", PricePerMeter = 1400, Utilities = 10000 },
                new PriceList { Id = 3,ApartType = "Three-room", PricePerMeter = 1900, Utilities = 21000 },
                new PriceList { Id = 4,ApartType = "Four-room", PricePerMeter = 2500, Utilities = 34000 },
                new PriceList { Id = 5,ApartType = "Five-room", PricePerMeter = 5000, Utilities = 40000 },
                new PriceList { Id = 6,ApartType = "Six-room", PricePerMeter = 1100, Utilities = 9900 },
                new PriceList { Id = 7, ApartType = "Studio", PricePerMeter = 900, Utilities = 6000 }
                );
            modelBuilder.Entity<Renter>().HasData(
                new Renter 
                {   
                    Id=1, 
                    Name="Гадик", 
                    FirstName="Петров",
                    LastName="Петрович",
                    NumOfResidents = 1, 
                    LivingSpace = 31.8, 
                    ApartType = "One-room", 
                    CostOfLiving = 10000, 
                    RentAmount = 9000, 
                    PriceListId=1
                },
                new Renter
                {
                    Id=2,
                    Name = "Мразик",
                    FirstName = "Ифанов",
                    LastName = "Иванович",
                    NumOfResidents = 2,
                    LivingSpace = 50.6,
                    ApartType = "Two-room",
                    CostOfLiving = 16000,
                    RentAmount = 9000,
                    PriceListId = 2
                },
                new Renter
                {
                    Id=3,
                    Name = "Андрей",
                    FirstName = "Прокофьев",
                    LastName = "Семенович",
                    NumOfResidents = 4,
                    LivingSpace = 66.7,
                    ApartType = "Three-room",
                    CostOfLiving = 45000,
                    RentAmount = 9000,
                    PriceListId = 3
                },
                new Renter
                {
                    Id=4,
                    Name = "Артем",
                    FirstName = "Коваленко",
                    LastName = "Игоревич",
                    NumOfResidents = 1,
                    LivingSpace = 50.6,
                    ApartType = "Studio",
                    CostOfLiving = 10000,
                    RentAmount = 5000,
                    PriceListId = 7
                },
                new Renter
                {
                    Id=5,
                    Name = "Максим",
                    FirstName = "Зиновьев",
                    LastName = "Александрович",
                    NumOfResidents = 1,
                    LivingSpace = 50.6,
                    ApartType = "Two-room",
                    CostOfLiving = 16000,
                    RentAmount = 9000,
                    PriceListId = 2
                },
                new Renter
                {
                    Id=6,
                    Name = "Павел",
                    FirstName = "Кипров",
                    LastName = "Гослинглович",
                    NumOfResidents = 7,
                    LivingSpace = 307.6,
                    ApartType = "Six-room",
                    CostOfLiving = 70000,
                    RentAmount = 23000,
                    PriceListId = 6
                },
                new Renter
                {
                    Id=7,
                    Name = "Ярослав",
                    FirstName = "Рекун",
                    LastName = "Гадович",
                    NumOfResidents = 2,
                    LivingSpace = 47.6,
                    ApartType = "Two-room",
                    CostOfLiving = 14000,
                    RentAmount = 7000,
                    PriceListId = 2
                },
                new Renter
                {
                    Id = 8,
                    Name = "Евгений",
                    FirstName = "Жироежка",
                    LastName = "Анаконодвич",
                    NumOfResidents = 3,
                    LivingSpace = 191.8,
                    ApartType = "Four-room",
                    CostOfLiving = 96000,
                    RentAmount = 14000,
                    PriceListId = 4
                },
                new Renter
                {
                    Id = 9,
                    Name = "Геннадий",
                    FirstName = "Иванов",
                    LastName = "Иванович",
                    NumOfResidents = 6,
                    LivingSpace = 215.9,
                    ApartType = "Five-room",
                    CostOfLiving = 78600,
                    RentAmount = 20000,
                    PriceListId = 5
                },
                new Renter
                {
                    Id = 10,
                    Name = "Иван",
                    FirstName = "Жулин",
                    LastName = "Петрович",
                    NumOfResidents = 4,
                    LivingSpace = 66.7,
                    ApartType = "Three-room",
                    CostOfLiving = 45000,
                    RentAmount = 9000,
                    PriceListId = 1
                },
                new Renter
                {
                    Id = 11,
                    Name = "Сергей",
                    FirstName = "Дудка",
                    LastName = "Иванович",
                    NumOfResidents = 2,
                    LivingSpace = 300,
                    ApartType = "Six-room",
                    CostOfLiving = 112000,
                    RentAmount = 28000,
                    PriceListId = 6
                },
                new Renter
                {
                    Id = 12,
                    Name = "Анатолий",
                    FirstName = "Твойборода",
                    LastName = "Константинович",
                    NumOfResidents = 1,
                    LivingSpace = 28,
                    ApartType = "Studio",
                    CostOfLiving = 16000,
                    RentAmount = 9000,
                    PriceListId = 7
                },
                new Renter
                {
                    Id = 13,
                    Name = "Константин",
                    FirstName = "Жорин",
                    LastName = "Иванович",
                    NumOfResidents = 1,
                    LivingSpace = 41.2,
                    ApartType = "Two-room",
                    CostOfLiving = 24000,
                    RentAmount = 12000,
                    PriceListId = 2
                },
                new Renter
                {
                    Id = 14,
                    Name = "Данила",
                    FirstName = "Голубихин",
                    LastName = "Валерьевич",
                    NumOfResidents = 1,
                    LivingSpace = 49.1,
                    ApartType = "One-room",
                    CostOfLiving = 16200,
                    RentAmount = 9100,
                    PriceListId = 2
                },
                new Renter
                {
                    Id = 15,
                    Name = "Ольга",
                    FirstName = "Прокофьева",
                    LastName = "Анатольевна",
                    NumOfResidents = 3,
                    LivingSpace = 50.6,
                    ApartType = "Two-room",
                    CostOfLiving = 16000,
                    RentAmount = 9000,
                    PriceListId = 2
                }
                );
        }
    }
}

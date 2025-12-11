using CarRental.Domain.DataModels;
using CarRental.Domain.InternalData.ComponentClasses;
using CarRental.Domain.InternalData.ComponentEnums;

namespace CarRental.Domain.DataSeed;

/// <summary>
/// Provides a fixed set of pre-initialized domain entities for testing and demonstration purposes
/// </summary>
public class DataSeed 
{
    /// <summary>
    /// List of physical vehicles available for rental
    /// </summary>
    public List<Car> Cars { get; }

    /// <summary>
    /// List of registered clients
    /// </summary>
    public List<Client> Clients { get; }

    /// <summary>
    /// List of rental agreements linking clients to specific cars
    /// </summary>
    public List<Rent> Rents { get; }

    /// <summary>
    /// List of car models representing vehicle
    /// </summary>
    public List <CarModel> Models { get; }

    /// <summary>
    /// List of car model generations
    /// </summary>
    public List<CarModelGeneration> Generation { get; }

    /// <summary>
    /// Constructor implementation
    /// </summary>
    public DataSeed()
    {
        Models = new List<CarModel>
        {
            new() { Id = 1, Name = "Fiat 500", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 4, BodyType = BodyType.CityCar, ClassType = ClassType.A },
            new() { Id = 2, Name = "Subaru Outback", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.StationWagon, ClassType = ClassType.D },
            new() { Id = 3, Name = "Volkswagen Golf", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Hatchback, ClassType = ClassType.C },
            new() { Id = 4, Name = "Mazda CX-5", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.SportUtilityVehicle, ClassType = ClassType.C },
            new() { Id = 5, Name = "Nissan Qashqai", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.Crossover, ClassType = ClassType.C },
            new() { Id = 6, Name = "Volvo XC90", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 7, BodyType = BodyType.SportUtilityVehicle, ClassType = ClassType.E },
            new() { Id = 7, Name = "Audi A4", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.D },
            new() { Id = 8, Name = "Honda CR-V", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.SportUtilityVehicle, ClassType = ClassType.D },
            new() { Id = 9, Name = "Hyundai Tucson", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.SportUtilityVehicle, ClassType = ClassType.C },
            new() { Id = 10, Name = "Volkswagen Transporter", DriveType = InternalData.ComponentEnums.DriveType.RearWheel, SeatsNumber = 9, BodyType = BodyType.Van, ClassType = ClassType.F },
            new() { Id = 11, Name = "Mercedes E-Class", DriveType = InternalData.ComponentEnums.DriveType.RearWheel,SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.E },
            new() { Id = 12, Name = "Ford Focus", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Hatchback, ClassType = ClassType.C },
            new() { Id = 13, Name = "Jaguar F-Type", DriveType = InternalData.ComponentEnums.DriveType.RearWheel, SeatsNumber = 2, BodyType = BodyType.Coupe, ClassType = ClassType.E },
            new() { Id = 14, Name = "Tesla Model 3", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.D },
            new() { Id = 15, Name = "Toyota Camry", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.D },
            new() { Id = 16, Name = "Lexus LS", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.F },
            new() { Id = 17, Name = "Porsche 911", DriveType = InternalData.ComponentEnums.DriveType.RearWheel, SeatsNumber = 2, BodyType = BodyType.SportsCar, ClassType = ClassType.E },
            new() { Id = 18, Name = "Renault Megane", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Hatchback, ClassType = ClassType.C },
            new() { Id = 19, Name = "BMW X5", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.SportUtilityVehicle, ClassType = ClassType.E },
            new() { Id = 20, Name = "Kia Rio", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.B }
        };

        Generation = new List<CarModelGeneration>
        {
            new() { Id = 1, Year = 2019, TransmissionType = TransmissionType.Manual, Model = Models[16], HourCost = 160.00m }, // Porsche 911
            new() { Id = 2, Year = 2022, TransmissionType = TransmissionType.Automatic, Model = Models[0], HourCost = 35.00m },  // Fiat 500
            new() { Id = 3, Year = 2021, TransmissionType = TransmissionType.Manual, Model = Models[11], HourCost = 55.00m }, // Ford Focus
            new() { Id = 4, Year = 2020, TransmissionType = TransmissionType.Variable, Model = Models[4], HourCost = 70.00m }, // Nissan Qashqai
            new() { Id = 5, Year = 2023, TransmissionType = TransmissionType.Automatic, Model = Models[18], HourCost = 120.00m }, // BMW X5
            new() { Id = 6, Year = 2022, TransmissionType = TransmissionType.Automatic, Model = Models[15], HourCost = 140.00m }, // Lexus LS
            new() { Id = 7, Year = 2018, TransmissionType = TransmissionType.Manual, Model = Models[19], HourCost = 40.00m }, // Kia Rio
            new() { Id = 8, Year = 2021, TransmissionType = TransmissionType.Automatic, Model = Models[7], HourCost = 85.00m }, // Honda CR-V
            new() { Id = 9, Year = 2023, TransmissionType = TransmissionType.Automatic, Model = Models[12], HourCost = 150.00m }, // Jaguar F-Type
            new() { Id = 10, Year = 2020, TransmissionType = TransmissionType.Manual, Model = Models[9], HourCost = 60.00m }, // VW Transporter
            new() { Id = 11, Year = 2022, TransmissionType = TransmissionType.Automatic, Model = Models[1], HourCost = 95.00m }, // Subaru Outback
            new() { Id = 12, Year = 2021, TransmissionType = TransmissionType.Automatic, Model = Models[8], HourCost = 75.00m }, // Hyundai Tucson
            new() { Id = 13, Year = 2019, TransmissionType = TransmissionType.Manual, Model = Models[2], HourCost = 50.00m }, // VW Golf
            new() { Id = 14, Year = 2023, TransmissionType = TransmissionType.Automatic, Model = Models[13], HourCost = 100.00m }, // Tesla Model 3
            new() { Id = 15, Year = 2022, TransmissionType = TransmissionType.Automatic, Model = Models[14], HourCost = 80.00m }, // Toyota Camry
            new() { Id = 16, Year = 2020, TransmissionType = TransmissionType.Automatic, Model = Models[6], HourCost = 90.00m }, // Audi A4
            new() { Id = 17, Year = 2022, TransmissionType = TransmissionType.Automatic, Model = Models[5], HourCost = 105.00m }, // Volvo XC90
            new() { Id = 18, Year = 2021, TransmissionType = TransmissionType.Manual, Model = Models[17], HourCost = 55.00m }, // Renault Megane
            new() { Id = 19, Year = 2023, TransmissionType = TransmissionType.Automatic, Model = Models[10], HourCost = 110.00m }, // Mercedes E-Class
            new() { Id = 20, Year = 2021, TransmissionType = TransmissionType.Automatic, Model = Models[3], HourCost = 80.00m } // Mazda CX-5
        };

        Cars = new List<Car>
        {
            new() { Id = 1, ModelGeneration = Generation[5], NumberPlate = "T890NO96", Colour = "Gray" },
            new() { Id = 2, ModelGeneration = Generation[14], NumberPlate = "A123BC77", Colour = "Black" },
            new() { Id = 3, ModelGeneration = Generation[0], NumberPlate = "M789ZA89", Colour = "Yellow" },
            new() { Id = 4, ModelGeneration = Generation[19], NumberPlate = "D012HI80", Colour = "Blue" },
            new() { Id = 5, ModelGeneration = Generation[6], NumberPlate = "E345JK81", Colour = "Red" },
            new() { Id = 6, ModelGeneration = Generation[16], NumberPlate = "F678LM82", Colour = "Gray" },
            new() { Id = 7, ModelGeneration = Generation[7], NumberPlate = "G901NO83", Colour = "Green" },
            new() { Id = 8, ModelGeneration = Generation[13], NumberPlate = "H234PQ84", Colour = "Black" },
            new() { Id = 9, ModelGeneration = Generation[3], NumberPlate = "I567RS85", Colour = "White" },
            new() { Id = 10, ModelGeneration = Generation[18], NumberPlate = "J890TU86", Colour = "Silver" },
            new() { Id = 11, ModelGeneration = Generation[10], NumberPlate = "K123VW87", Colour = "Blue" },
            new() { Id = 12, ModelGeneration = Generation[11], NumberPlate = "L456XY88", Colour = "Red" },
            new() { Id = 13, ModelGeneration = Generation[8], NumberPlate = "R234JK94", Colour = "Blue" },
            new() { Id = 14, ModelGeneration = Generation[9], NumberPlate = "N012BC90", Colour = "White" },
            new() { Id = 15, ModelGeneration = Generation[1], NumberPlate = "Q901HI93", Colour = "Red" },
            new() { Id = 16, ModelGeneration = Generation[15], NumberPlate = "P678FG92", Colour = "Silver" },
            new() { Id = 17, ModelGeneration = Generation[2], NumberPlate = "O345DE91", Colour = "Black" },
            new() { Id = 18, ModelGeneration = Generation[17], NumberPlate = "S567LM95", Colour = "Green" },
            new() { Id = 19, ModelGeneration = Generation[4], NumberPlate = "C789FG79", Colour = "Silver" },
            new() { Id = 20, ModelGeneration = Generation[12], NumberPlate = "B456DE78", Colour = "White" }
        };

        Clients = new List<Client>
        {
            new() { Id = 1, DriverLicenseId = "DL990011223", LastName = "Belov", FirstName = "Roman", Patronymic = "Evgenievich", BirthDate = new DateOnly(1984, 9, 13) },
            new() { Id = 2, DriverLicenseId = "DL112233445", LastName = "Lebedev", FirstName = "Artem", Patronymic = "Olegovich", BirthDate = new DateOnly(1994, 10, 21) },
            new() { Id = 3, DriverLicenseId = "DL001122334", LastName = "Efimova", FirstName = "Daria", Patronymic = "Mikhailovna", BirthDate = new DateOnly(1999, 6, 22) },
            new() { Id = 4, DriverLicenseId = "DL445566778", LastName = "Vinogradova", FirstName = "Polina", Patronymic = "Sergeevna", BirthDate = new DateOnly(1996, 12, 19) },
            new() { Id = 5, DriverLicenseId = "DL567890123", LastName = "Smirnov", FirstName = "Dmitry", Patronymic = "Alexandrovich", BirthDate = new DateOnly(1985, 7, 12) },
            new() { Id = 6, DriverLicenseId = "DL234567890", LastName = "Petrova", FirstName = "Maria", Patronymic = "Dmitrievna", BirthDate = new DateOnly(1988, 11, 3) },
            new() { Id = 7, DriverLicenseId = "DL789012345", LastName = "Vasiliev", FirstName = "Sergey", Patronymic = "Nikolaevich", BirthDate = new DateOnly(1980, 12, 5) },
            new() { Id = 8, DriverLicenseId = "DL890123456", LastName = "Fedorov", FirstName = "Andrey", Patronymic = null, BirthDate = new DateOnly(1993, 9, 27) },
            new() { Id = 9, DriverLicenseId = "DL334455667", LastName = "Orlov", FirstName = "Maxim", Patronymic = "Igorevich", BirthDate = new DateOnly(1986, 8, 3) },
            new() { Id = 10, DriverLicenseId = "DL012345678", LastName = "Nikolaev", FirstName = "Nikolay", Patronymic = "Pavlovich", BirthDate = new DateOnly(1987, 6, 9) },
            new() { Id = 11, DriverLicenseId = "DL678901234", LastName = "Popova", FirstName = "Anna", Patronymic = "Ivanovna", BirthDate = new DateOnly(1997, 4, 18) },
            new() { Id = 12, DriverLicenseId = "DL223344556", LastName = "Sokolova", FirstName = "Tatiana", Patronymic = null, BirthDate = new DateOnly(1989, 2, 11) },
            new() { Id = 13, DriverLicenseId = "DL901234567", LastName = "Morozova", FirstName = "Olga", Patronymic = "Viktorovna", BirthDate = new DateOnly(1991, 3, 14) },
            new() { Id = 14, DriverLicenseId = "DL123456789", LastName = "Ivanov", FirstName = "Alexey", Patronymic = "Sergeevich", BirthDate = new DateOnly(1990, 5, 15) },
            new() { Id = 15, DriverLicenseId = "DL556677889", LastName = "Mikhailov", FirstName = "Kirill", Patronymic = null, BirthDate = new DateOnly(1990, 7, 25) },
            new() { Id = 16, DriverLicenseId = "DL667788990", LastName = "Romanova", FirstName = "Victoria", Patronymic = "Andreevna", BirthDate = new DateOnly(1983, 11, 8) },
            new() { Id = 17, DriverLicenseId = "DL778899001", LastName = "Karpov", FirstName = "Igor", Patronymic = "Valentinovich", BirthDate = new DateOnly(1982, 4, 17) },
            new() { Id = 18, DriverLicenseId = "DL889900112", LastName = "Timofeeva", FirstName = "Natalia", Patronymic = null, BirthDate = new DateOnly(1998, 1, 29) },
            new() { Id = 19, DriverLicenseId = "DL345678901", LastName = "Sidorov", FirstName = "Ivan", Patronymic = "Petrovich", BirthDate = new DateOnly(1995, 8, 22) },
            new() { Id = 20, DriverLicenseId = "DL456789012", LastName = "Kuznetsova", FirstName = "Elena", Patronymic = null, BirthDate = new DateOnly(1992, 1, 30) }
        };

        var baseTime = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        Rents = new List<Rent>
        {
            new() { Id = 1, StartDateTime = baseTime.AddDays(-2), Duration = 6, Car = Cars[13], Client = Clients[14] },
            new() { Id = 2, StartDateTime = baseTime.AddDays(12), Duration = 6, Car = Cars[19], Client = Clients[19] },
            new() { Id = 3, StartDateTime = baseTime.AddDays(-25), Duration = 48, Car = Cars[2], Client = Clients[2] },
            new() { Id = 4, StartDateTime = baseTime.AddDays(8), Duration = 24, Car = Cars[17], Client = Clients[17] },
            new() { Id = 5, StartDateTime = baseTime.AddDays(-20), Duration = 72, Car = Cars[4], Client = Clients[4] },
            new() { Id = 6, StartDateTime = baseTime.AddDays(4), Duration = 72, Car = Cars[15], Client = Clients[15] },
            new() { Id = 7, StartDateTime = baseTime.AddDays(-15), Duration = 168, Car = Cars[6], Client = Clients[6] },
            new() { Id = 8, StartDateTime = baseTime.AddDays(-4), Duration = 48, Car = Cars[11], Client = Clients[11] },
            new() { Id = 9, StartDateTime = baseTime.AddDays(-10), Duration = 36, Car = Cars[8], Client = Clients[8] },
            new() { Id = 10, StartDateTime = baseTime, Duration = 24, Car = Cars[1], Client = Clients[0] },
            new() { Id = 11, StartDateTime = baseTime.AddDays(2), Duration = 8, Car = Cars[14], Client = Clients[13] },
            new() { Id = 12, StartDateTime = baseTime.AddDays(-8), Duration = 24, Car = Cars[9], Client = Clients[9] },
            new() { Id = 13, StartDateTime = baseTime.AddDays(6), Duration = 12, Car = Cars[16], Client = Clients[16] },
            new() { Id = 14, StartDateTime = baseTime.AddDays(-6), Duration = 12, Car = Cars[10], Client = Clients[10] },
            new() { Id = 15, StartDateTime = baseTime.AddDays(10), Duration = 48, Car = Cars[18], Client = Clients[18] },
            new() { Id = 16, StartDateTime = baseTime.AddDays(-28), Duration = 12, Car = Cars[12], Client = Clients[12] },
            new() { Id = 17, StartDateTime = baseTime.AddDays(-22), Duration = 6, Car = Cars[3], Client = Clients[3] },
            new() { Id = 18, StartDateTime = baseTime.AddDays(-18), Duration = 8, Car = Cars[5], Client = Clients[5] },
            new() { Id = 19, StartDateTime = baseTime.AddDays(-12), Duration = 4, Car = Cars[7], Client = Clients[7] },
            new() { Id = 20, StartDateTime = baseTime.AddDays(-30), Duration = 24, Car = Cars[0], Client = Clients[1] },
            new() { Id = 21, StartDateTime = baseTime.AddDays(-25), Duration = 12, Car = Cars[5], Client = Clients[0] },
            new() { Id = 22, StartDateTime = baseTime.AddDays(-10), Duration = 24, Car = Cars[0], Client = Clients[1] },
            new() { Id = 23, StartDateTime = baseTime.AddDays(-5), Duration = 8, Car = Cars[10], Client = Clients[1] },
            new() { Id = 24, StartDateTime = baseTime.AddDays(-20), Duration = 48, Car = Cars[3], Client = Clients[2] },
            new() { Id = 25, StartDateTime = baseTime.AddDays(-15), Duration = 6, Car = Cars[7], Client = Clients[2] },
            new() { Id = 26, StartDateTime = baseTime.AddDays(-8), Duration = 12, Car = Cars[15], Client = Clients[2] },
            new() { Id = 27, StartDateTime = baseTime.AddDays(-22), Duration = 24, Car = Cars[4], Client = Clients[3] },
            new() { Id = 28, StartDateTime = baseTime.AddDays(-18), Duration = 36, Car = Cars[8], Client = Clients[3] },
            new() { Id = 29, StartDateTime = baseTime.AddDays(-12), Duration = 12, Car = Cars[12], Client = Clients[3] },
            new() { Id = 30, StartDateTime = baseTime.AddDays(-6), Duration = 6, Car = Cars[17], Client = Clients[3] },
            new() { Id = 31, StartDateTime = baseTime.AddDays(-28), Duration = 72, Car = Cars[1], Client = Clients[4] },
            new() { Id = 32, StartDateTime = baseTime.AddDays(-24), Duration = 24, Car = Cars[6], Client = Clients[4] },
            new() { Id = 33, StartDateTime = baseTime.AddDays(-20), Duration = 48, Car = Cars[9], Client = Clients[4] },
            new() { Id = 34, StartDateTime = baseTime.AddDays(-16), Duration = 12, Car = Cars[13], Client = Clients[4] },
            new() { Id = 35, StartDateTime = baseTime.AddDays(-10), Duration = 8, Car = Cars[18], Client = Clients[4] },
            new() { Id = 36, StartDateTime = baseTime.AddDays(-30), Duration = 168, Car = Cars[2], Client = Clients[5] },
            new() { Id = 37, StartDateTime = baseTime.AddDays(-26), Duration = 24, Car = Cars[7], Client = Clients[5] },
            new() { Id = 38, StartDateTime = baseTime.AddDays(-22), Duration = 48, Car = Cars[11], Client = Clients[5] },
            new() { Id = 39, StartDateTime = baseTime.AddDays(-18), Duration = 6, Car = Cars[14], Client = Clients[5] },
            new() { Id = 40, StartDateTime = baseTime.AddDays(-14), Duration = 12, Car = Cars[16], Client = Clients[5] },
            new() { Id = 41, StartDateTime = baseTime.AddDays(-10), Duration = 24, Car = Cars[19], Client = Clients[5] },
            new() { Id = 42, StartDateTime = baseTime.AddDays(-3), Duration = 10, Car = Cars[0], Client = Clients[6] },
            new() { Id = 43, StartDateTime = baseTime.AddDays(-1), Duration = 5, Car = Cars[2], Client = Clients[7] },
            new() { Id = 44, StartDateTime = baseTime.AddDays(1), Duration = 7, Car = Cars[5], Client = Clients[8] },
            new() { Id = 45, StartDateTime = baseTime.AddDays(3), Duration = 9, Car = Cars[10], Client = Clients[9] }
        };
    }
}

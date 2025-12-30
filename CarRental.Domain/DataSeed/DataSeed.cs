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
    public List<CarModelGeneration> Generations { get; }

    /// <summary>
    /// Constructor implementation
    /// </summary>
    public DataSeed()
    {
        Models = new List<CarModel>
        {
            new() { Id = Guid.NewGuid(), Name = "Fiat 500", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 4, BodyType = BodyType.CityCar, ClassType = ClassType.A },
            new() { Id = Guid.NewGuid(), Name = "Subaru Outback", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.StationWagon, ClassType = ClassType.D },
            new() { Id = Guid.NewGuid(), Name = "Volkswagen Golf", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Hatchback, ClassType = ClassType.C },
            new() { Id = Guid.NewGuid(), Name = "Mazda CX-5", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.SportUtilityVehicle, ClassType = ClassType.C },
            new() { Id = Guid.NewGuid(), Name = "Nissan Qashqai", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.Crossover, ClassType = ClassType.C },
            new() { Id = Guid.NewGuid(), Name = "Volvo XC90", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 7, BodyType = BodyType.SportUtilityVehicle, ClassType = ClassType.E },
            new() { Id = Guid.NewGuid(), Name = "Audi A4", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.D },
            new() { Id = Guid.NewGuid(), Name = "Honda CR-V", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.SportUtilityVehicle, ClassType = ClassType.D },
            new() { Id = Guid.NewGuid(), Name = "Hyundai Tucson", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.SportUtilityVehicle, ClassType = ClassType.C },
            new() { Id = Guid.NewGuid(), Name = "Volkswagen Transporter", DriveType = InternalData.ComponentEnums.DriveType.RearWheel, SeatsNumber = 9, BodyType = BodyType.Van, ClassType = ClassType.F },
            new() { Id = Guid.NewGuid(), Name = "Mercedes E-Class", DriveType = InternalData.ComponentEnums.DriveType.RearWheel,SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.E },
            new() { Id = Guid.NewGuid(), Name = "Ford Focus", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Hatchback, ClassType = ClassType.C },
            new() { Id = Guid.NewGuid(), Name = "Jaguar F-Type", DriveType = InternalData.ComponentEnums.DriveType.RearWheel, SeatsNumber = 2, BodyType = BodyType.Coupe, ClassType = ClassType.E },
            new() { Id = Guid.NewGuid(), Name = "Tesla Model 3", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.D },
            new() { Id = Guid.NewGuid(), Name = "Toyota Camry", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.D },
            new() { Id = Guid.NewGuid(), Name = "Lexus LS", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.F },
            new() { Id = Guid.NewGuid(), Name = "Porsche 911", DriveType = InternalData.ComponentEnums.DriveType.RearWheel, SeatsNumber = 2, BodyType = BodyType.SportsCar, ClassType = ClassType.E },
            new() { Id = Guid.NewGuid(), Name = "Renault Megane", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Hatchback, ClassType = ClassType.C },
            new() { Id = Guid.NewGuid(), Name = "BMW X5", DriveType = InternalData.ComponentEnums.DriveType.AllWheel, SeatsNumber = 5, BodyType = BodyType.SportUtilityVehicle, ClassType = ClassType.E },
            new() { Id = Guid.NewGuid(), Name = "Kia Rio", DriveType = InternalData.ComponentEnums.DriveType.FrontWheel, SeatsNumber = 5, BodyType = BodyType.Sedan, ClassType = ClassType.B }
        };

        Generations = new List<CarModelGeneration>
        {
            new() { Id = Guid.NewGuid(), Year = 2019, TransmissionType = TransmissionType.Manual, ModelId = Models[16].Id, Model = Models[16], HourCost = 160.00m }, // Porsche 911
            new() { Id = Guid.NewGuid(), Year = 2022, TransmissionType = TransmissionType.Automatic, ModelId = Models[0].Id, Model = Models[0], HourCost = 35.00m },  // Fiat 500
            new() { Id = Guid.NewGuid(), Year = 2021, TransmissionType = TransmissionType.Manual, ModelId = Models[11].Id, Model = Models[11], HourCost = 55.00m }, // Ford Focus
            new() { Id = Guid.NewGuid(), Year = 2020, TransmissionType = TransmissionType.Variable, ModelId = Models[4].Id, Model = Models[4], HourCost = 70.00m }, // Nissan Qashqai
            new() { Id = Guid.NewGuid(), Year = 2023, TransmissionType = TransmissionType.Automatic, ModelId = Models[18].Id, Model = Models[18], HourCost = 120.00m }, // BMW X5
            new() { Id = Guid.NewGuid(), Year = 2022, TransmissionType = TransmissionType.Automatic, ModelId = Models[15].Id, Model = Models[15], HourCost = 140.00m }, // Lexus LS
            new() { Id = Guid.NewGuid(), Year = 2018, TransmissionType = TransmissionType.Manual, ModelId = Models[19].Id, Model = Models[19], HourCost = 40.00m }, // Kia Rio
            new() { Id = Guid.NewGuid(), Year = 2021, TransmissionType = TransmissionType.Automatic, ModelId = Models[7].Id, Model = Models[7], HourCost = 85.00m }, // Honda CR-V
            new() { Id = Guid.NewGuid(), Year = 2023, TransmissionType = TransmissionType.Automatic, ModelId = Models[12].Id, Model = Models[12], HourCost = 150.00m }, // Jaguar F-Type
            new() { Id = Guid.NewGuid(), Year = 2020, TransmissionType = TransmissionType.Manual, ModelId = Models[9].Id, Model = Models[9], HourCost = 60.00m }, // VW Transporter
            new() { Id = Guid.NewGuid(), Year = 2022, TransmissionType = TransmissionType.Automatic, ModelId = Models[1].Id, Model = Models[1], HourCost = 95.00m }, // Subaru Outback
            new() { Id = Guid.NewGuid(), Year = 2021, TransmissionType = TransmissionType.Automatic, ModelId = Models[8].Id, Model = Models[8], HourCost = 75.00m }, // Hyundai Tucson
            new() { Id = Guid.NewGuid(), Year = 2019, TransmissionType = TransmissionType.Manual, ModelId = Models[2].Id, Model = Models[2], HourCost = 50.00m }, // VW Golf
            new() { Id = Guid.NewGuid(), Year = 2023, TransmissionType = TransmissionType.Automatic, ModelId = Models[13].Id, Model = Models[13], HourCost = 100.00m }, // Tesla Model 3
            new() { Id = Guid.NewGuid(), Year = 2022, TransmissionType = TransmissionType.Automatic, ModelId = Models[14].Id, Model = Models[14], HourCost = 80.00m }, // Toyota Camry
            new() { Id = Guid.NewGuid(), Year = 2020, TransmissionType = TransmissionType.Automatic, ModelId = Models[6].Id, Model = Models[6], HourCost = 90.00m }, // Audi A4
            new() { Id = Guid.NewGuid(), Year = 2022, TransmissionType = TransmissionType.Automatic, ModelId = Models[5].Id, Model = Models[5], HourCost = 105.00m }, // Volvo XC90
            new() { Id = Guid.NewGuid(), Year = 2021, TransmissionType = TransmissionType.Manual, ModelId = Models[17].Id, Model = Models[17], HourCost = 55.00m }, // Renault Megane
            new() { Id = Guid.NewGuid(), Year = 2023, TransmissionType = TransmissionType.Automatic, ModelId = Models[10].Id, Model = Models[10], HourCost = 110.00m }, // Mercedes E-Class
            new() { Id = Guid.NewGuid(), Year = 2021, TransmissionType = TransmissionType.Automatic, ModelId = Models[3].Id, Model = Models[3], HourCost = 80.00m } // Mazda CX-5
        };

        Cars = new List<Car>
        {
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[5].Id, ModelGeneration = Generations[5], NumberPlate = "T890NO96", Colour = "Gray" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[14].Id, ModelGeneration = Generations[14], NumberPlate = "A123BC77", Colour = "Black" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[0].Id, ModelGeneration = Generations[0], NumberPlate = "M789ZA89", Colour = "Yellow" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[19].Id, ModelGeneration = Generations[19], NumberPlate = "D012HI80", Colour = "Blue" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[6].Id, ModelGeneration = Generations[6], NumberPlate = "E345JK81", Colour = "Red" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[16].Id, ModelGeneration = Generations[16], NumberPlate = "F678LM82", Colour = "Gray" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[7].Id, ModelGeneration = Generations[7], NumberPlate = "G901NO83", Colour = "Green" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[13].Id, ModelGeneration = Generations[13], NumberPlate = "H234PQ84", Colour = "Black" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[3].Id, ModelGeneration = Generations[3], NumberPlate = "I567RS85", Colour = "White" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[18].Id, ModelGeneration = Generations[18], NumberPlate = "J890TU86", Colour = "Silver" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[10].Id, ModelGeneration = Generations[10], NumberPlate = "K123VW87", Colour = "Blue" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[11].Id, ModelGeneration = Generations[11], NumberPlate = "L456XY88", Colour = "Red" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[8].Id, ModelGeneration = Generations[8], NumberPlate = "R234JK94", Colour = "Blue" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[9].Id, ModelGeneration = Generations[9], NumberPlate = "N012BC90", Colour = "White" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[1].Id, ModelGeneration = Generations[1], NumberPlate = "Q901HI93", Colour = "Red" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[15].Id, ModelGeneration = Generations[15], NumberPlate = "P678FG92", Colour = "Silver" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[2].Id, ModelGeneration = Generations[2], NumberPlate = "O345DE91", Colour = "Black" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[17].Id, ModelGeneration = Generations[17], NumberPlate = "S567LM95", Colour = "Green" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[4].Id, ModelGeneration = Generations[4], NumberPlate = "C789FG79", Colour = "Silver" },
            new() { Id = Guid.NewGuid(), ModelGenerationId = Generations[12].Id, ModelGeneration = Generations[12], NumberPlate = "B456DE78", Colour = "White" }
        };

        Clients = new List<Client>
        {
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL990011223", LastName = "Belov", FirstName = "Roman", Patronymic = "Evgenievich", BirthDate = new DateOnly(1984, 9, 13) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL112233445", LastName = "Lebedev", FirstName = "Artem", Patronymic = "Olegovich", BirthDate = new DateOnly(1994, 10, 21) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL001122334", LastName = "Efimova", FirstName = "Daria", Patronymic = "Mikhailovna", BirthDate = new DateOnly(1999, 6, 22) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL445566778", LastName = "Vinogradova", FirstName = "Polina", Patronymic = "Sergeevna", BirthDate = new DateOnly(1996, 12, 19) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL567890123", LastName = "Smirnov", FirstName = "Dmitry", Patronymic = "Alexandrovich", BirthDate = new DateOnly(1985, 7, 12) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL234567890", LastName = "Petrova", FirstName = "Maria", Patronymic = "Dmitrievna", BirthDate = new DateOnly(1988, 11, 3) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL789012345", LastName = "Vasiliev", FirstName = "Sergey", Patronymic = "Nikolaevich", BirthDate = new DateOnly(1980, 12, 5) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL890123456", LastName = "Fedorov", FirstName = "Andrey", Patronymic = null, BirthDate = new DateOnly(1993, 9, 27) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL334455667", LastName = "Orlov", FirstName = "Maxim", Patronymic = "Igorevich", BirthDate = new DateOnly(1986, 8, 3) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL012345678", LastName = "Nikolaev", FirstName = "Nikolay", Patronymic = "Pavlovich", BirthDate = new DateOnly(1987, 6, 9) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL678901234", LastName = "Popova", FirstName = "Anna", Patronymic = "Ivanovna", BirthDate = new DateOnly(1997, 4, 18) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL223344556", LastName = "Sokolova", FirstName = "Tatiana", Patronymic = null, BirthDate = new DateOnly(1989, 2, 11) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL901234567", LastName = "Morozova", FirstName = "Olga", Patronymic = "Viktorovna", BirthDate = new DateOnly(1991, 3, 14) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL123456789", LastName = "Ivanov", FirstName = "Alexey", Patronymic = "Sergeevich", BirthDate = new DateOnly(1990, 5, 15) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL556677889", LastName = "Mikhailov", FirstName = "Kirill", Patronymic = null, BirthDate = new DateOnly(1990, 7, 25) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL667788990", LastName = "Romanova", FirstName = "Victoria", Patronymic = "Andreevna", BirthDate = new DateOnly(1983, 11, 8) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL778899001", LastName = "Karpov", FirstName = "Igor", Patronymic = "Valentinovich", BirthDate = new DateOnly(1982, 4, 17) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL889900112", LastName = "Timofeeva", FirstName = "Natalia", Patronymic = null, BirthDate = new DateOnly(1998, 1, 29) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL345678901", LastName = "Sidorov", FirstName = "Ivan", Patronymic = "Petrovich", BirthDate = new DateOnly(1995, 8, 22) },
            new() { Id = Guid.NewGuid(), DriverLicenseId = "DL456789012", LastName = "Kuznetsova", FirstName = "Elena", Patronymic = null, BirthDate = new DateOnly(1992, 1, 30) }
        };

        var baseTime = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        Rents = new List<Rent>
        {
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-2), Duration = 6, CarId = Cars[13].Id, Car = Cars[13], ClientId = Clients[14].Id, Client = Clients[14] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(12), Duration = 6, CarId = Cars[19].Id, Car = Cars[19], ClientId = Clients[19].Id, Client = Clients[19] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-25), Duration = 48, CarId = Cars[2].Id, Car = Cars[2], ClientId = Clients[2].Id, Client = Clients[2] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(8), Duration = 24, CarId = Cars[17].Id, Car = Cars[17], ClientId = Clients[17].Id, Client = Clients[17] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-20), Duration = 72, CarId = Cars[4].Id, Car = Cars[4], ClientId = Clients[4].Id, Client = Clients[4] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(4), Duration = 72, CarId = Cars[15].Id, Car = Cars[15], ClientId = Clients[15].Id, Client = Clients[15] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-15), Duration = 168, CarId = Cars[6].Id, Car = Cars[6], ClientId = Clients[6].Id, Client = Clients[6] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-4), Duration = 48, CarId = Cars[11].Id, Car = Cars[11], ClientId = Clients[11].Id, Client = Clients[11] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-10), Duration = 36, CarId = Cars[8].Id, Car = Cars[8], ClientId = Clients[8].Id, Client = Clients[8] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime, Duration = 24, CarId = Cars[1].Id, Car = Cars[1], ClientId = Clients[0].Id, Client = Clients[0] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(2), Duration = 8, CarId = Cars[14].Id, Car = Cars[14], ClientId = Clients[13].Id, Client = Clients[13] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-8), Duration = 24, CarId = Cars[9].Id, Car = Cars[9], ClientId = Clients[9].Id, Client = Clients[9] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(6), Duration = 12, CarId = Cars[16].Id, Car = Cars[16], ClientId = Clients[16].Id, Client = Clients[16] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-6), Duration = 12, CarId = Cars[10].Id, Car = Cars[10], ClientId = Clients[10].Id, Client = Clients[10] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(10), Duration = 48, CarId = Cars[18].Id, Car = Cars[18], ClientId = Clients[18].Id, Client = Clients[18] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-28), Duration = 12, CarId = Cars[12].Id, Car = Cars[12], ClientId = Clients[12].Id, Client = Clients[12] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-22), Duration = 6, CarId = Cars[3].Id, Car = Cars[3], ClientId = Clients[3].Id, Client = Clients[3] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-18), Duration = 8, CarId = Cars[5].Id, Car = Cars[5], ClientId = Clients[5].Id, Client = Clients[5] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-12), Duration = 4, CarId = Cars[7].Id, Car = Cars[7], ClientId = Clients[7].Id, Client = Clients[7] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-30), Duration = 24, CarId = Cars[0].Id, Car = Cars[0], ClientId = Clients[1].Id, Client = Clients[1] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-25), Duration = 12, CarId = Cars[5].Id, Car = Cars[5], ClientId = Clients[0].Id, Client = Clients[0] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-10), Duration = 24, CarId = Cars[0].Id, Car = Cars[0], ClientId = Clients[1].Id, Client = Clients[1] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-5), Duration = 8, CarId = Cars[10].Id, Car = Cars[10], ClientId = Clients[1].Id, Client = Clients[1] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-20), Duration = 48, CarId = Cars[3].Id, Car = Cars[3], ClientId = Clients[2].Id, Client = Clients[2] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-15), Duration = 6, CarId = Cars[7].Id, Car = Cars[7], ClientId = Clients[2].Id, Client = Clients[2] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-8), Duration = 12, CarId = Cars[15].Id, Car = Cars[15], ClientId = Clients[2].Id, Client = Clients[2] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-22), Duration = 24, CarId = Cars[4].Id, Car = Cars[4], ClientId = Clients[3].Id, Client = Clients[3] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-18), Duration = 36, CarId = Cars[8].Id, Car = Cars[8], ClientId = Clients[3].Id, Client = Clients[3] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-12), Duration = 12, CarId = Cars[12].Id, Car = Cars[12], ClientId = Clients[3].Id, Client = Clients[3] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-6), Duration = 6, CarId = Cars[17].Id, Car = Cars[17], ClientId = Clients[3].Id, Client = Clients[3] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-28), Duration = 72, CarId = Cars[1].Id, Car = Cars[1], ClientId = Clients[4].Id, Client = Clients[4] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-24), Duration = 24, CarId = Cars[6].Id, Car = Cars[6], ClientId = Clients[4].Id, Client = Clients[4] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-20), Duration = 48, CarId = Cars[9].Id, Car = Cars[9], ClientId = Clients[4].Id, Client = Clients[4] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-16), Duration = 12, CarId = Cars[13].Id, Car = Cars[13], ClientId = Clients[4].Id, Client = Clients[4] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-10), Duration = 8, CarId = Cars[18].Id, Car = Cars[18], ClientId = Clients[4].Id, Client = Clients[4] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-30), Duration = 168, CarId = Cars[2].Id, Car = Cars[2], ClientId = Clients[5].Id, Client = Clients[5] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-26), Duration = 24, CarId = Cars[7].Id, Car = Cars[7], ClientId = Clients[5].Id, Client = Clients[5] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-22), Duration = 48, CarId = Cars[11].Id, Car = Cars[11], ClientId = Clients[5].Id, Client = Clients[5] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-18), Duration = 6, CarId = Cars[14].Id, Car = Cars[14], ClientId = Clients[5].Id, Client = Clients[5] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-14), Duration = 12, CarId = Cars[16].Id, Car = Cars[16], ClientId = Clients[5].Id, Client = Clients[5] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-10), Duration = 24, CarId = Cars[19].Id, Car = Cars[19], ClientId = Clients[5].Id, Client = Clients[5] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-3), Duration = 10, CarId = Cars[0].Id, Car = Cars[0], ClientId = Clients[6].Id, Client = Clients[6] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(-1), Duration = 5, CarId = Cars[2].Id, Car = Cars[2], ClientId = Clients[7].Id, Client = Clients[7] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(1), Duration = 7, CarId = Cars[5].Id, Car = Cars[5], ClientId = Clients[8].Id, Client = Clients[8] },
            new() { Id = Guid.NewGuid(), StartDateTime = baseTime.AddDays(3), Duration = 9, CarId = Cars[10].Id, Car = Cars[10], ClientId = Clients[9].Id, Client = Clients[9] }
        };
    }
}

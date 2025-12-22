var builder = DistributedApplication.CreateBuilder(args);

var mongodb = builder.AddMongoDB("mongodb");

var carDb = mongodb.AddDatabase("CarRentalDb");

builder.AddProject<Projects.CarRental_Api>("carrental-api")
    .WithReference(carDb);

builder.Build().Run();

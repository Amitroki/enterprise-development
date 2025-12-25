var builder = DistributedApplication.CreateBuilder(args);

var mongodb = builder.AddMongoDB("mongodb");
mongodb.AddDatabase("car-rental");

builder.AddProject<Projects.CarRental_Api>("carrental-api")
       .WithReference(mongodb, "CarRentalDb")
       .WaitFor(mongodb);

builder.Build().Run();

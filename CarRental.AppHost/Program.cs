var builder = DistributedApplication.CreateBuilder(args);

var mongodb = builder.AddMongoDB("mongodb");
mongodb.AddDatabase("car-rental");

var kafka = builder.AddKafka("car-rental-kafka")
    .WithKafkaUI()
    .WithEnvironment("KAFKA_AUTO_CREATE_TOPICS_ENABLE", "true");

builder.AddProject<Projects.CarRental_Api>("carrental-api")
       .WithReference(mongodb, "CarRentalDb")
       .WithReference(kafka)
       .WaitFor(mongodb)
       .WaitFor(kafka);

builder.AddProject<Projects.CarRental_Generator>("carrental-generator")
    .WithReference(kafka)
    .WaitFor(kafka);

builder.Build().Run();
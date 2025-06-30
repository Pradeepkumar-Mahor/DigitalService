var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DigitalService_UI>("digitalservice-ui");

builder.Build().Run();
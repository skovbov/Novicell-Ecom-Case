using ProductService.Api.Endpoints;
using ProductService.Application;
using ProductService.Infrastructure;
using ProductService.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddHostedService<ErpSyncWorker>();

builder.Services.AddOpenApi();
var app = builder.Build();

app.MapOpenApi();

app.MapProductEndpoints();

app.Run();
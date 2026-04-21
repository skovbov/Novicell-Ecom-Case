using SyncWorker.Application.ExternalServices;
using SyncWorker.Infrastructure.ExternalServices;
using SyncWorker.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddHttpClient<IErpClient, ErpClient>(client =>
{
    client.BaseAddress = new Uri("https://ncrecruitmentpubweu.blob.core.windows.net/cases/ecom/");
});

var host = builder.Build();
host.Run();
using NativeBackgroundTasks.Cronjobs;
using NativeBackgroundTasks.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddHttpClient<RandomStringService>();
builder.Services.AddSingleton<RandomStringService>();
builder.Services.AddHostedService<RandomStringCron>();
builder.Services.AddSingleton<IQueue, QueueService>();
builder.Services.AddHostedService<QueueCron>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

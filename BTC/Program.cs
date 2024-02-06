using BTC;
using BTC.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.Configure<SourceConfigs>(builder.Configuration.GetSection("AvailableSources"));

builder.Services.AddSingleton<IBitCoinDataSource, BitStampBitCoinDataSource>();
builder.Services.AddSingleton<IBitCoinDataSource, BitFinexBitCoinDataSource>();
builder.Services.AddSingleton<IDataService, InMemoryDataService>();
builder.Services.AddSingleton<BitCoinPriceManager, BitCoinPriceManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using MongoDB.Driver;
using MongoStoreApi.Models;
using MongoStoreApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoStoreDatabaseSettings>(
    builder.Configuration.GetSection("MongoStoreDatabase"));


builder.Services.AddSingleton<SongService>();
builder.Services.AddSingleton<HistorialService>();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = "mongodb://localhost:27017";
var mongoClient = new MongoClient(connectionString);
Console.WriteLine(mongoClient);
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

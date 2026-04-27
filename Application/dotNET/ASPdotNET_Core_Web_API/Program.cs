using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

// Connecting to Postgres Database! Check appsettings.json for the credencials 
var connectionString = builder.Configuration.GetConnectionString("aisDatabase");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Failed to establish Database Connection! Check Appsettings.json or your PostgressSQL.");
}
else Console.WriteLine("Database Connected: "+connectionString);

builder.Services.AddDbContext<AisDB_Context>(options => options.UseNpgsql(connectionString));





// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// This is the allowed IP adresses for Response.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClientFrontend", policy =>
    {

        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
       
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});






builder.WebHost.UseUrls("http://0.0.0.0:5000");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  //  app.UseSwagger();
//    app.UseSwaggerUI();
}
// SWAGGER foreveryone!
 app.UseSwagger();
 app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowClientFrontend");

app.MapControllers();

app.Run();

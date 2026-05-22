using DataLayer;
using WebLayer;
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
builder.Services.AddScoped<IDataService, DataService>();





// Add services to the container.


// (this commend came with the template :) Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// This is the allowed IP adresses for Response.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClientFrontend", policy =>
    {

        policy.WithOrigins("http://localhost:5173", "https://localhost:5173", "https://aismap.dk", "http://aismap.dk")
       
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
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c => { 
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "api.aismap.dk");
    c.InjectStylesheet("/swagger.css");
});

app.UseHttpsRedirection();

app.UseCors("AllowClientFrontend");

app.UseAuthorization();



app.MapControllers();

app.Run();

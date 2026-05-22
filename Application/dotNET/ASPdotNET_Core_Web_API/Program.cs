using DataLayer;
using WebLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Asp.Versioning;

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





builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    //(?api-version=2.0)So basiclly we combines reading from a quiery string to a request header.
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version")
   );
}).AddApiExplorer(options => {
    options.GroupNameFormat = "'v'vvv"; // This line is related to version string format
    options.SubstituteApiVersionInUrl = true;
});


builder.Services.AddSwaggerGen(options =>
{
    // v1
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "api.aismap.dk",
        Version = "v1"
    });

    // v2
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "api.aismap.dk",
        Version = "v2"
    });
});



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
app.UseSwaggerUI(options =>
{


    // Build swagger UI endpoints for both versions so they appear in the top-right dropdown menu
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2.0");
    options.InjectStylesheet("/swagger.css");
    options.DocumentTitle = "api.aismap.dk - API Documentation";
});

app.UseHttpsRedirection();

app.UseCors("AllowClientFrontend");

app.UseAuthorization();



app.MapControllers();

app.Run();

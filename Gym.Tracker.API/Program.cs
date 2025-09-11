using Asp.Versioning.ApiExplorer;
using Auth.Learn.Common.Extensions;
using Gym.Tracker.Core.Extensions;
using Gym.Tracker.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);


IWebHostEnvironment environment = builder.Environment;
var configuration = new ConfigurationBuilder()
                                      .AddJsonFile(environment.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json")
                                      .Build();

//Enable CORS
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

// Add services to the container.
builder.Services.AddControllers();

// Business service collection extension
builder.Services.AddServiceConnector();

//Register Versioning
builder.Services.RegisterApiVersioningServices();

//Register Swagger
builder.Services.RegisterSwaggerAuthorization("Gym.Tracker.Api.xml");

// Register DbContext with connection string
builder.Services.AddDataConnector(configuration);

builder.Services.AddRouting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(c =>
    {
        foreach (var desc in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",$"Gym Tracker {desc.GroupName}");
        }
    });}

app.UseCors(x => x.AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials());

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

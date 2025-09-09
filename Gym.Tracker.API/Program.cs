using Auth.Learn.Common.Extensions;
using Scalar.AspNetCore;
using System;

var builder = WebApplication.CreateBuilder(args);


//Enable CORS
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader();
}));
// Add services to the container.

builder.Services.AddControllers();

builder.Services.RegisterApiVersioningServices();

//Register Swagger
builder.Services.RegisterSwaggerAuthorization("Gym.Tracker.Api.xml");

//Add OpenAPI
//builder.Services.AddOpenApi();

builder.Services.AddRouting();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapOpenApi();

    app.UseSwaggerUI(swagger =>
    {
        swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym Tracker API V1");
        swagger.SwaggerEndpoint("/swagger/v2/swagger.json", "Gym Tracker API V2");
    });

    //string[] versions = ["v1", "v2"];
    //app.MapScalarApiReference(options =>
    //{
    //    options.AddDocuments(versions.Select(version => new ScalarDocument(version, $"Gym Tracker API {version}")));
    //    options.WithTitle("Scalar API");
    //    options.WithTheme(ScalarTheme.Solarized);
    //    options.WithDownloadButton(true);
    //});

}

app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials());

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

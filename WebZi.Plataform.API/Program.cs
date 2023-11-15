using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WebZi.Plataform.CrossCutting.Configuration;
using WebZi.Plataform.Data.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

ConfigureJson(builder);

ConfigureWebApplication(builder.Build());

static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services
        .AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();

    builder.Services
        .RegisterDependencies(builder.Configuration);

    builder.Services
        .AddMvc(options =>
    {
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    });
}

static void ConfigureJson(WebApplicationBuilder builder)
{
    // Newtonsoft.Json
    JsonConvert.DefaultSettings = () => new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    // System.Json
    builder.Services
        .Configure<JsonOptions>(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
}

static void ConfigureWebApplication(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(options =>
    {
        options.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin();
    });

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
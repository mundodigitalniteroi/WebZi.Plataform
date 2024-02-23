using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Text.Json.Serialization;
using WebZi.Plataform.Data.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

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

    // https://www.c-sharpcorner.com/article/response-compression-in-asp-net-core/
    // https://balta.io/blog/aspnet-compressao
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes;
    });

    builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.Optimal;
    });

    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.Optimal;
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

    // TODO:
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
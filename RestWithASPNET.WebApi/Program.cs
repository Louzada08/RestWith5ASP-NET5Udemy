using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestWithASPNET.FrameWrkDrivers.Services;
using RestWithASPNET.WebApi.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorizationRestUdemyConfiguration(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("MySQLConnectionString");

builder.Services.AddDbContextInjector(connection);
builder.Services.AddServicesInjector();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddMediatorInjector();
builder.Services.AddAutoMapperInjector();

#region Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Development",
        b =>
            b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});
#endregion

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
    options.FormatterMappings.SetMediaTypeMappingForFormat("pdf", "application/pdf");
})
    .AddXmlSerializerFormatters();

// Versioning API
builder.Services.AddApiVersioning();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Rest API´s de 0 a Azure com ASP.NET Core 8 e Docker",
        Version = "v1",
        Description = "API RESTful desenvolvida no curso de ASP.NET Core 8",
        Contact = new()
        {
            Name = "Leandro Costa",
            Url = new Uri("https://github.com/leandrocgsi")
        }
    });


    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira apenas o token, sem a palavra-chave 'Bearer'",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer",
    });

    c.OperationFilter<AuthenticationRequirementsOperationFilter>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() ||
    app.Environment.IsEnvironment("Homolog") ||
    app.Environment.IsEnvironment("Api - Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//void MigrateDatabase(String connection)
//{
//    try
//    {
//        var envolveConnection = new MySqlConnection(connection);
//        var envolve = new Evolve(envolveConnection, Log.Information)
//        {
//            Locations = new List<string> { "db/migrations", "db/dataset" },
//            IsEraseDisabled = true,
//        };
//        envolve.Migrate();
//    }
//    catch (Exception ex)
//    {
//        Log.Error("Database migration failed.", ex);
//        throw;
//    }
//}
using EvolveDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using RestWithASPNETUdemy.Configurations;
using RestWithASPNETUdemy.InfraIOC;
using RestWithASPNETUdemy.Model.Context;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var appVersion = "v1";

builder.Services.AddAuthorizationRestUdemyConfiguration(builder.Configuration);

#region Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Development",
        b =>
            b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});
#endregion

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(appVersion, new()
    {
        Title = "Rest API´s de 0 a Azure com ASP.NET Core 8 e Docker",
        Version = appVersion,
        Description = "API RESTful desenvolvida no curso de ASP.NET Core 8",
        Contact = new()
        {
            Name = "Leandro Costa",
            Url = new Uri("https://github.com/leandrocgsi")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira apenas o token, sem a palavra-chave 'Bearer'",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer",
    });

    c.OperationFilter<AuthenticationRequirementsOperationFilter>();

});


var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection,
    new MySqlServerVersion(new Version(8, 0, 29)))
);

//if(builder.Environment.IsDevelopment())
//{
//    MigrateDatabase(connection);
//}

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
})
    .AddXmlSerializerFormatters();

// Versioning API
builder.Services.AddApiVersioning();

builder.Services.AddServicesInjector();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void MigrateDatabase(String connection)
{
    try
    {
        var envolveConnection = new MySqlConnection(connection);
        var envolve = new Evolve(envolveConnection, Log.Information) 
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        envolve.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed.", ex);
        throw;
    }
}

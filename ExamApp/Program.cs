using Exam.Business.Services;
using Exam.DataAccess.Data;
using Exam.Business.Mapping;
using Microsoft.EntityFrameworkCore;
using Exam.DataAccess.Repository.IRepository;
using Exam.DataAccess.Repository;
using Microsoft.OpenApi.Models;
using Exam.DataAccess.DbInitializer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;

//Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
//    .WriteTo.File("log/logs.xml", rollingInterval: RollingInterval.Day).CreateLogger();

//builder.Host.UseSerilog();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
).AddXmlDataContractSerializerFormatters(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"));
});
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IExamCategoryRepository, ExamCategoryRepository>();
builder.Services.AddAutoMapper(option =>
{
    option.AddProfile(new MapperProfile());
});

builder.Services.AddIdentityServices();

builder.Services.AddAuthenticationServices(configuration);

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer' [space] and then your token in the text input below \r\n\r\n" +
                      "Example: Bearer 12345abcdef",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer"
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

SeedDatabase();

app.MapControllers();

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope()){
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}

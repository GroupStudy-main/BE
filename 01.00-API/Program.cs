using API;
using APIExtension.Auth;
using DataLayer.DBContext;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region dbContext
builder.Services.AddDbContext<GroupStudyContext>(options =>
{
    options.EnableSensitiveDataLogging();
    //options.UseSqlServer(configuration.GetConnectionString("Default"));
    options.UseInMemoryDatabase("GroupStudy");
});
//Use for scaffolding api controller. remove later
//builder.Services.AddDbContext<TempContext>(options =>
//{
//    //options.UseSqlServer(configuration.GetConnectionString("Default"));
//    options.UseInMemoryDatabase("GroupStudy");
//});
#endregion
#region auth
builder.Services.AddJwtAuthService(configuration);
builder.Services.AddSwaggerGen(options =>
{
    options.AddJwtAuthUi();
    //options.AddGoogleAuthUi
    options.AddGoogleAuthUi(configuration);
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
#region setGoogle loginPage
    options.SetGoogleAuthUi(configuration);
    #endregion
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

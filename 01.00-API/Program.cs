using API;
using API.SignalRHub;
using API.SignalRHub.Tracker;
using APIExtension.Auth;
using DataLayer.DBContext;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.ClassImplement;
using RepositoryLayer.Interface;
using ServiceLayer.ClassImplement;
using ServiceLayer.Interface;

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
    options.UseSqlServer(configuration.GetConnectionString("Default"));
    //options.UseInMemoryDatabase("GroupStudy");
});
//Use for scaffolding api controller. remove later
builder.Services.AddDbContext<TempContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("Default"));
    //options.UseInMemoryDatabase("GroupStudy");
});
#endregion

#region SignalR
builder.Services.AddSignalR();
#endregion
#region AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region service and repo
builder.Services.AddSingleton<PresenceTracker>();
builder.Services.AddSingleton<ShareScreenTracker>();
builder.Services.AddScoped<IRepoWrapper, RepoWrapper>();
builder.Services.AddScoped<IServiceWrapper, ServiceWrapper>();
#endregion

builder.Services.AddJwtAuthService(configuration);
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
#region auth
    options.AddJwtAuthUi();
    //options.AddGoogleAuthUi
    options.AddGoogleAuthUi(configuration);
#endregion
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
#region setGoogle loginPage
    options.SetGoogleAuthUi(configuration);
    #endregion
});
app.UseStaticFiles();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<ChatHub>("hubs/chathub");

app.Run();

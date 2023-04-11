using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Notes.API.Notes.Domain.Models;
using Notes.API.Notes.Domain.Repositories;
using Notes.API.Notes.Domain.Services;
using Notes.API.Notes.Repositories;
using Notes.API.Notes.Services;
using Notes.API.Security.Authorization.Handlers.Implementations;
using Notes.API.Security.Authorization.Middleware;
using Notes.API.Security.Authorization.Middleware.Handlers.Interfaces;
using Notes.API.Security.Authorization.Settings;
using Notes.API.Security.Domain.Repositories;
using Notes.API.Security.Domain.Services;
using Notes.API.Security.Models;
using Notes.API.Security.Repositories;
using Notes.API.Security.Services;
using Notes.API.Shared.Domain.Repositories;
using Notes.API.Shared.Persistence.Context;
using Notes.API.Shared.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//OpenApi Configuration
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Notes App",
            TermsOfService = new Uri("https://www.google.com/"),
            Contact = new OpenApiContact
            {
                Name = "NastyPad",
                Url = new Uri("https://www.google.com/")
            },
            License = new OpenApiLicense
            {
                Name = "NastyPad JoBit License",
                Url = new Uri("https://www.google.com/")
            }
        });
        options.EnableAnnotations();
    });

//Connection String
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<AppDbContext>(
    optionsBuilder =>
    {
        optionsBuilder.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    });

//Lowercase Url's configuration
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Security Services
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Notes Service
//---Notes---
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

//Shared Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBaseRepository<User, long>, BaseRepository<User, long>>();
builder.Services.AddScoped<IBaseRepository<Note, long>, BaseRepository<Note, long>>();


//Services
builder.Services.AddAutoMapper(
    typeof(Notes.API.Notes.Mapping.ModelToResourceProfile),
    typeof(Notes.API.Notes.Mapping.ResourceToModelProfile),
    typeof(Notes.API.Security.Mapping.ModelToResourceProfile),
    typeof(Notes.API.Security.Mapping.ResourceToModelProfile));


var app = builder.Build();

//Validation for ensuring database tables are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context?.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

//CORS
app.UseCors(corsPolicyBuilder => 
    corsPolicyBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());


app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Configure JWT Handling 
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.Run();
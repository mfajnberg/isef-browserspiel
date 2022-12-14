 using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using web_api;
using web_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
options.AddPolicy(name: "sexy",
    policy => {
        policy.WithOrigins("http://127.0.0.1:5173") // Frontend Dev Server
        .AllowCredentials()
        .WithHeaders("Content-Type");
    });
});

builder.Services.AddControllers();

// Add DbContext for EntityFramework
builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString));

});

// when methods or controller sercured with Autorization, then a authentication-service must be injected
// also the app must UseAuthentication() before UseAuthorization()  !!!
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// add Notification Service
builder.Services.AddScoped<INotification, MailNotification>();


// set Logger 
// Error: System.PlatformNotSupportedException: EventLog access is not supported on this platform.

//builder.Host.ConfigureLogging(logging =>
//{
//    logging.ClearProviders();
//    logging.AddConsole();
//});
//builder.Services.AddScoped<ILogger, ConsoleLogger>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// complete options => necassary for Swagger authorization
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Default Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

    // jwt-token must be entered in authorize dialog as "bearer **key**"

    // using System.Reflection;
    /*var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));*/
});

builder.Services.AddHostedService<InteractionClock>();


var app = builder.Build();

// run db migrations automaticly on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("sexy");
}

app.UseHttpsRedirection();

app.UseDefaultFiles();

var typeProvider = new FileExtensionContentTypeProvider();
typeProvider.Mappings[".glb"] = "application/octet-stream";
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
       Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
        RequestPath = "",
        ContentTypeProvider = typeProvider,
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Assets")),
        RequestPath = "/assets",
        ContentTypeProvider = typeProvider,
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

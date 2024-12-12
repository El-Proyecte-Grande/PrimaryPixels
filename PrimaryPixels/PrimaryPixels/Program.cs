using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrimaryPixels.Data;
using PrimaryPixels.Models.Users;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Repositories;
using PrimaryPixels.Models;
using PrimaryPixels.Models.ShoppingCartItem;
using PrimaryPixels.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");
var validIssuer = builder.Configuration["TokenValidation:ValidIssuer"];
var validAudience = builder.Configuration["TokenValidation:ValidAudience"];
var issuerSigningKey = builder.Configuration["TokenValidation:IssuerSigningKey"];
// Add services to the container.

AddServices();
ConfigureSwagger();
AddDbContexts();
AddAuthentication();
AddIdentity();
AddCors();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var authenticationSeeder = scope.ServiceProvider.GetRequiredService<AuthenticationSeeder>();
authenticationSeeder.AddRoles();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowFrontend");
app.Run();


void AddServices()
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IRepository<User>, UserRepository>();
    builder.Services.AddScoped<IRepository<Headphone>, ProductRepository<Headphone>>();
    builder.Services.AddScoped<IRepository<Phone>, ProductRepository<Phone>>();
    builder.Services.AddScoped<IRepository<Computer>, ProductRepository<Computer>>();
    builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
    builder.Services.AddScoped<IRepository<OrderDetails>, OrderDetailsRepository>();
    builder.Services.AddScoped<IRepository<ShoppingCartItem>, ShoppingCartItemRepository>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<AuthenticationSeeder>();
    builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
  
}


void ConfigureSwagger()
{
    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    });
}

void AddDbContexts()
{
    builder.Services.AddDbContext<PrimaryPixelsContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });

    builder.Services.AddDbContext<UsersContext>(options =>
    {
        options.UseSqlServer(connectionString);

    });
}

void AddAuthentication()
{
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = validIssuer,
                ValidAudience = validAudience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(issuerSigningKey)
                ),
            };
        });
}

void AddIdentity()
{
    builder.Services
        .AddIdentityCore<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<UsersContext>();
}

void AddCors()
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend",
            builder => builder
                .WithOrigins("http://localhost:5000")
                .AllowAnyHeader()
                .AllowAnyMethod());
    });

}
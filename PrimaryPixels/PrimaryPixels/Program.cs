using System.Text;
using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrimaryPixels.Data;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Repositories;
using PrimaryPixels.Models;
using PrimaryPixels.Models.ShoppingCartItem;
using PrimaryPixels.Services;
using PrimaryPixels.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);
DotEnv.Load();
builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration["ConnectionString"];
var validIssuer = builder.Configuration["ValidIssuer"];
var validAudience = builder.Configuration["ValidAudience"];
var issuerSigningKey = builder.Configuration["JwtSecretKey"];
var frontendUrl = builder.Configuration["FrontendUrl"];
// Add services to the container.

AddServices();
ConfigureSwagger();
AddDbContexts();
AddAuthentication();
AddIdentity();
AddCors();

var app = builder.Build();
Migration();
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
    builder.Services.AddTransient<IEmailSender, EmailSender>();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IRepository<Headphone>, ProductRepository<Headphone>>();
    builder.Services.AddScoped<IRepository<Phone>, ProductRepository<Phone>>();
    builder.Services.AddScoped<IRepository<Computer>, ProductRepository<Computer>>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
    builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
    builder.Services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();
    builder.Services.AddScoped<IProductRepository, ProductsRepository>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<AuthenticationSeeder>();
    builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();
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
        .AddEntityFrameworkStores<UsersContext>()
        .AddDefaultTokenProviders();
}

void AddCors()
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend",
            builder => builder
                .WithOrigins(frontendUrl)
                .AllowAnyHeader()
                .AllowAnyMethod());
    });

}

void Migration()
         {
             using (var scope = app.Services.CreateScope())
             {
                 var primaryDb = scope.ServiceProvider.GetRequiredService<PrimaryPixelsContext>();
                 var usersDb = scope.ServiceProvider.GetRequiredService<UsersContext>();
                 // GetPendingMigrations: Checks the Migration history table and compare it with the project's migrations
                 if (primaryDb.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                 {
                     if (primaryDb.Database.GetPendingMigrations().Any())
                     {
                         primaryDb.Database.Migrate();
                     }
                 }
                 if (usersDb.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                 {
                     if (usersDb.Database.GetPendingMigrations().Any())
                     {
                         usersDb.Database.Migrate();
                     }
                 }
             }
         }

public partial class Program
{
            
}
using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Data;
using PrimaryPixels.Models.Users;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Repositories;
using PrimaryPixels.Models;
using PrimaryPixels.Models.ShoppingCartItem;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Headphone>, ProductRepository<Headphone>>();
builder.Services.AddScoped<IRepository<Phone>, ProductRepository<Phone>>();
builder.Services.AddScoped<IRepository<Computer>, ProductRepository<Computer>>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IRepository<OrderDetails>, OrderDetailsRepository>();
builder.Services.AddScoped<IRepository<ShoppingCartItem>, ShoppingCartItemRepository>();

builder.Services.AddDbContext<PrimaryPixelsContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
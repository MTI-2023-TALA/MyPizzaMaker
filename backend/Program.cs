using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<backend.DataAccess.EfModels.myPizzaMakerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myPizzaMaker"));
});
builder.Services.AddLogging();
builder.Services.AddSingleton(typeof(ILogger), typeof(Logger<backend.Logger>));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(backend.DataAccess.AutomapperProfiles));

builder.Services.AddTransient<backend.DataAccess.Interfaces.ICartRepository, backend.DataAccess.CartRepository>();
builder.Services.AddTransient<backend.DataAccess.Interfaces.IIngredientRepository, backend.DataAccess.IngredientRepository>();
builder.Services.AddTransient<backend.DataAccess.Interfaces.IPizzaRepository, backend.DataAccess.PizzaRepository>();
builder.Services.AddTransient<backend.Service.Interfaces.IIngredientService, backend.Service.IngredientService>();
builder.Services.AddTransient<backend.Service.Interfaces.ICartService, backend.Service.CartService>();
builder.Services.AddTransient<backend.Service.Interfaces.IStatsService, backend.Service.StatsService>();


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

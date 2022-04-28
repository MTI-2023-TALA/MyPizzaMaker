using AutoMapper; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(backend.DataAccess.AutomapperProfiles));
builder.Services.AddTransient<backend.DataAccess.Interfaces.ICartRepository, backend.DataAccess.CartRepository>();
builder.Services.AddTransient<backend.DataAccess.Interfaces.IIngredientRepository, backend.DataAccess.IngredientRepository>();
builder.Services.AddTransient<backend.DataAccess.Interfaces.IPizzaRepository, backend.DataAccess.PizzaRepository>();


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

using ProductsAPI.Business;
using ProductsAPI.Interfaces;
using ProductsAPI.Validation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IProductValidator, ProductValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


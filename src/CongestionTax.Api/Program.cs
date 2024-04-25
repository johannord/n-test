using CongestionTax.Api.Domain;
using CongestionTax.Api.Domain.Contracts;
using CongestionTax.Api.Domain.Model;
using CongestionTax.Api.Domain.Rules;
using CongestionTax.Api.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVehicleTypeRules, VehicleTypeRules>();
builder.Services.AddScoped<IDateRules, DateRules>();
builder.Services.AddScoped<ITaxRateRules, TaxRateRules>();
builder.Services.AddScoped<ICongestionTaxCalculator, CongestionTaxCalculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/calculatetax", (ICongestionTaxCalculator taxCalculator, DateTime[] timestamps) => {
    var car = new Vehicle(VehicleType.Car);

    var result = taxCalculator.GetTotalTax(car, timestamps);

    return result;
})
.WithName("CalculateCongestionTax")
.WithOpenApi();

app.Run();

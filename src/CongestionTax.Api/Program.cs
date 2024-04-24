using CongestionTax.Api.Domain;
using CongestionTax.Api.Domain.Contracts;
using CongestionTax.Api.Domain.Model;
using CongestionTax.Api.Domain.Rules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVehicleRules, VehicleRules>();
builder.Services.AddScoped<IDateRules, DateRules>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/calculatetax", (IVehicleRules vehicleRules, IDateRules dateRules, DateTime[] timestamps) => {
    var calculator = new CongestionTaxCalculator(vehicleRules, dateRules);
    var car = new Vehicle(VehicleType.Car);

    var result = calculator.GetTaxByPassages(car, timestamps);

    return result;
})
.WithName("CalculateCongestionTax")
.WithOpenApi();

app.Run();

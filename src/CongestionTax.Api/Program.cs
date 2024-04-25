using CongestionTax.Api.Application;
using CongestionTax.Api.Application.Contracts;
using CongestionTax.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/calculatetax", (CalculateCongestionTaxHandler handler, CongestionTaxCalculationRequest request) => {
    var result = handler.Handle(request);

    return result;
})
.WithName("CalculateCongestionTax")
.WithOpenApi();

app.Run();

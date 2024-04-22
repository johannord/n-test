using CongestionTax.Api.Domain;
using TollFeeCalculator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/calculatetax", (DateTime[] timestamps) => {
    var calculator = new TollCalculator();
    var car = new Car();

    var result = calculator.GetTollFee(car, timestamps);

    return result;
})
.WithName("CalculateCongestionTax")
.WithOpenApi();

app.Run();

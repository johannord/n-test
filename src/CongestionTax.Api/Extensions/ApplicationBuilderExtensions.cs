using System.Text.Json.Serialization;
using CongestionTax.Api.Application;
using CongestionTax.Api.Domain.Contracts;
using CongestionTax.Api.Domain.Rules;
using CongestionTax.Api.Domain.Services;

namespace CongestionTax.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IVehicleTypeRules, VehicleTypeRules>();
        builder.Services.AddScoped<IDateRules, DateRules>();
        builder.Services.AddScoped<ITaxRateRules, TaxRateRules>();
        builder.Services.AddScoped<ICongestionTaxCalculator, CongestionTaxCalculator>();
        builder.Services.AddScoped<CalculateCongestionTaxHandler>();

        builder.Services
            .AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.AllowTrailingCommas = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
    }
}
using InvestAndFund.Developer.Test.Api.Application;
using InvestAndFund.Developer.Test.Api.Domain;
using InvestAndFund.Developer.Test.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(api => api.SwaggerDoc("v1", new OpenApiInfo { Title = "Invest And Fund Api", Version = "v1" }));
builder.Services.AddOrderProcessing();

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/orders", ([FromBody] OrderRequest request, IOrderProcessor orderProcessor) =>
{
    orderProcessor.Process(request.Order, request.PaymentType);
    return Results.Ok(new { Message = "Order processed successfully" });
})
.WithName("ProcessOrder")
.WithTags("Orders")
.AddEndpointFilter<ValidationFilter>();

app.Run();


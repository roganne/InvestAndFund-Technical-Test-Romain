using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Invest And Fund Api", Version = "v1" });
});
builder.Services.AddScoped<OP>();

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/orders", ([FromBody] OrderRequest request, OP orderProcessor) =>
{
    orderProcessor.Process(request.Order, request.PaymentType);
    return Results.Ok(new { Message = "Order processed successfully" });
})
.WithName("ProcessOrder")
.WithTags("Orders");

app.Run();

public class OrderRequest
{
    public Order Order { get; set; }
    public string PaymentType { get; set; }
}
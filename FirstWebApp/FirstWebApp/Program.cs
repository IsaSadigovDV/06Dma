var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
// bu hisse servislerimizi yaziriq

builder.Services.AddScoped<IHttpContextAccessor>();


app.MapGet("/", () => "Hello World!");
app.MapGet("/product/", () => $"Products");
app.MapGet("/product/{num}", (int num) => $"Products {num}");
// ardicilliq 


app.Run();

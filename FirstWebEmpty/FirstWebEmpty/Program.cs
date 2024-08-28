var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// service 
//siralama mentiqi yoxdur


var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=home}/{action=index}/{id?}"
    );

//https://localhost:7276/home/


// 
app.Run();



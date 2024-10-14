using Api006.App.CustomMiddlewares;
using Api006.App.ServiceRegistrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .RegisterSwagger()
    .UserRegister(builder.Configuration)
    .RegisterSericesAndRepos(builder.Configuration);
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/admin_v1/swagger.json", "My API - admin_v1");
        c.SwaggerEndpoint("/swagger/client_v1/swagger.json", "My API - client_v1");
    });
}
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

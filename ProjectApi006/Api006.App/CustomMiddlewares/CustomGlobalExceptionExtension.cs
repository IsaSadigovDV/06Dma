using Api006.Service.Exceptions.BaseExcep;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace Api006.App.CustomMiddlewares
{
    public static class CustomGlobalExceptionExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var statuscode =  (int)HttpStatusCode.InternalServerError;
                    var message = "Interal server error";
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if(contextFeature != null)
                    {
                        if(contextFeature.Error is BaseException ex)
                        {
                            statuscode = (int)ex.StatusCode;
                            message = ex.Message;
                        }

                        context.Response.StatusCode = statuscode;
                        var data = JsonSerializer.Serialize(new { statuscode = statuscode, message = message });
                        await context.Response.WriteAsync(data);
                    }
                });
            });
        }
    }
}

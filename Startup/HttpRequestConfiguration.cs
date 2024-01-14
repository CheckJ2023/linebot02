
using Microsoft.AspNetCore.HttpOverrides; //for nginx
namespace linebot02.Startup;

public static class HttpRequestConfiguration
{
    public static WebApplication ConfigurationHttpRequest(this WebApplication app)
    {
        if (app.Environment.EnvironmentName.Equals("Production"))
        {
            //for nginx
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
        }
        return app;
    }
}
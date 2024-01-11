namespace linebot02.Startup;

public static class SwaggerConfiguration
{
    public static WebApplication ConfigurationSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        return app;
    }
}
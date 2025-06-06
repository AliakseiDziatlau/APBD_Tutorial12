namespace Tutorial12.Infrastructure.DependencyInjection;

public static class ControllerMapping
{
    public static WebApplication AddControllerMapping(this WebApplication app)
    {
        app.MapControllers();
        return app;
    }
}
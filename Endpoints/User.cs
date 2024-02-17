namespace Phonebook.Endpoints;

public class User
{
    public void RegisterEndpoints(WebApplication app)
    {
        var phonebookApi = app.MapGroup("/user");
        
        phonebookApi.MapGet("/ola-mundo", () => "Olá Mundo!");
    }
}
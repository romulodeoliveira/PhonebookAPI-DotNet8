namespace Phonebook.Endpoints;

public class Contact
{
    public void RegisterEndpoints(WebApplication app)
    {
        var phonebookApi = app.MapGroup("/contact");
        
        phonebookApi.MapGet("/ola-mundo", () => "Olá Mundo!");
    }
}
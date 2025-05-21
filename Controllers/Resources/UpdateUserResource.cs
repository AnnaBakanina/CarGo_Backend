namespace Backend.Controllers.Resources;

public class UpdateUserResource
{
    public string UserId { get; set; } // auth0|xyz123
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
}
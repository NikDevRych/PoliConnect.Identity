using Microsoft.AspNetCore.Identity;

namespace PoliConnect.Identity.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime CreateAt { get; set; }
}

using Microsoft.AspNetCore.Mvc;
using PoliConnect.Identity.Application.DTOs;

namespace PoliConnect.Identity.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController : ControllerBase
{
    public Task<IActionResult> Register(RegisterDTO registerDTO)
    {

    }
}

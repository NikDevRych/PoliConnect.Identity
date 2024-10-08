using Microsoft.AspNetCore.Mvc;
using PoliConnect.Identity.Application.DTOs;
using PoliConnect.Identity.Application.Enums.Results;
using PoliConnect.Identity.Application.Services.Interfaces;

namespace PoliConnect.Identity.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<IActionResult> Register(RegisterDTO registerDTO)
    {
        var result = await _identityService.RegisterAsync(registerDTO);

        if (!result.IsSuccess) HandleRegisterError(result.Error);

        return Created();
    }

    private IActionResult HandleRegisterError(RegisterError error)
    {
        return error switch
        {
            RegisterError.EmailExist => Conflict("Email already exists"),
            RegisterError.PhoneNumberExist => Conflict("PhoneNumber already exists"),
            _ => throw new InvalidOperationException($"Can not handle {nameof(RegisterError)}"),
        };
    }
}
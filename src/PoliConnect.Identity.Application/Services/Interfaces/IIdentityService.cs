using PoliConnect.Identity.Application.DTOs;
using PoliConnect.Identity.Application.Enums.Results;
using PoliConnect.Identity.Application.Patterns;

namespace PoliConnect.Identity.Application.Services.Interfaces;

public interface IIdentityService
{
    Task<Result<RegisterError>> RegisterAsync(RegisterDTO registerDTO);
}

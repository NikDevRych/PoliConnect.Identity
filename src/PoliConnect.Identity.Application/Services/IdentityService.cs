using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PoliConnect.Identity.Application.DTOs;
using PoliConnect.Identity.Application.Enums.Results;
using PoliConnect.Identity.Application.Exceptions;
using PoliConnect.Identity.Application.Patterns;
using PoliConnect.Identity.Application.Services.Interfaces;
using PoliConnect.Identity.Domain.Entities;
using PoliConnect.Identity.Infrastructure.Data;

namespace PoliConnect.Identity.Application.Services;

public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly DataContext _dataContext;
    private readonly ILogger _logger;

    public IdentityService(UserManager<User> userManager, DataContext dataContext, ILogger<IdentityService> logger)
    {
        _userManager = userManager;
        _dataContext = dataContext;
        _logger = logger;

    }

    public async Task<Result<RegisterError>> RegisterAsync(RegisterDTO registerDTO)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(registerDTO.Email, nameof(registerDTO.Email));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(registerDTO.PhoneNumber, nameof(registerDTO.PhoneNumber));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(registerDTO.Password, nameof(registerDTO.Password));

        var isEmailExist = await _dataContext.Users.AnyAsync(x =>
            x.NormalizedEmail != null &&
            x.NormalizedEmail.Equals(registerDTO.Email, StringComparison.CurrentCultureIgnoreCase));

        var isPhoneExist = await _dataContext.Users.AnyAsync(x =>
            x.PhoneNumber != null &&
            x.PhoneNumber.Equals(registerDTO.PhoneNumber, StringComparison.CurrentCultureIgnoreCase));

        if (isEmailExist) return Result<RegisterError>.Failure(RegisterError.EmailExist);
        if (isPhoneExist) return Result<RegisterError>.Failure(RegisterError.PhoneNumberExist);

        var userName = registerDTO.Email.Split('@')[0];

        var user = new User
        {
            UserName = userName,
            Email = registerDTO.Email,
            PhoneNumber = registerDTO.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, registerDTO.Password);

        if (result.Succeeded) return Result<RegisterError>.Success();

        foreach (var error in result.Errors)
        {
            _logger.LogError($"Create user error {error.Code} - {error.Description}");
        }

        throw new IdentityException("Can not create user");
    }
}

using IdentityService.API.Auth.DTOs;
using IdentityService.API.Auth.Models;
using IdentityService.API.Auth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace IdentityService.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly TokenService _tokenService;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult>
        Register(RegisterDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager
            .CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        // Assign default User role
        await _userManager.AddToRoleAsync(
            user,
            "User"
        );

        return Ok(new
        {
            Message = "User Registered Successfully"
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult>
        Login(LoginDto dto)
    {
        var user = await _userManager
            .FindByEmailAsync(dto.Email);

        if (user == null)
        {
            return Unauthorized();
        }

        var validPassword =
            await _userManager
            .CheckPasswordAsync(user, dto.Password);

        if (!validPassword)
        {
            return Unauthorized();
        }

        var token =
            await _tokenService.CreateToken(
                user,
                _userManager);

        return Ok(new
        {
            Token = token
        });
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("Identity Service Working");
    }
}
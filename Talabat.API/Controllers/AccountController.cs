using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.DAL.Entities.Identity;

namespace Talabat.API.Controllers;
public class AccountController : BaseApiController
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) 
            return Unauthorized(new ApiResponse(401));

        var result = await _signInManager
            .CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
            return Unauthorized(new ApiResponse(401));

        return Ok(new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
        });
    }
}

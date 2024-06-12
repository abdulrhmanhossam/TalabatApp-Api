using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities.Identity;

namespace Talabat.API.Controllers;
public class AccountController : BaseApiController
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AccountController(SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var user = new AppUser
        {
            Email = registerDto.Email,
            UserName = registerDto.Email.Split('@')[0],
            DisplayName = registerDto.DisplayName,
            PhoneNumber = registerDto.PhoneNumber,
            Address = new Addsress
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                City = registerDto.City,
                Country = registerDto.Country,
                Street = registerDto.Street,
            }
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
            return BadRequest(new ApiResponse(400));

        return Ok(new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = await _tokenService.CreateToken(user, _userManager)
        });
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
            Token = await _tokenService.CreateToken(user, _userManager)
        });
    }
}

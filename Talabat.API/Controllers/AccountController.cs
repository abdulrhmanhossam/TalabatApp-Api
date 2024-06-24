using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.API.Extensions;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities.Identity;

namespace Talabat.API.Controllers;
public class AccountController : BaseApiController
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountController(SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (CheckEmailExists(registerDto.Email).Result.Value)
            return BadRequest(new ApiValidationErrorResponse()
            { Errors = new [] {"this email is aleardy in use!!"}});

        var user = new AppUser
        {
            Email = registerDto.Email,
            UserName = registerDto.Email.Split('@')[0],
            DisplayName = registerDto.DisplayName,
            PhoneNumber = registerDto.PhoneNumber,
            Address = new Address
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

    [HttpPost("login")]
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
            Token = await _tokenService.CreateToken(user, _userManager),
            
        });
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> CurrentUser()
    {
        // current user email
        var email = User.FindFirstValue(ClaimTypes.Email);

        var user = await _userManager.FindByEmailAsync(email);

        return Ok(new UserDto()
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = await _tokenService.CreateToken(user, _userManager)
        });
    }

    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<AddressDto>> UserAddress()
    {
        var user = await _userManager.FindUserWithAddressByEmailAsync(User);

        return Ok( _mapper.Map<Address, AddressDto>(user.Address));
    }

    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto newAddress)
    {
        var user = await _userManager.FindUserWithAddressByEmailAsync(User);

        user.Address = _mapper.Map<AddressDto, Address>(newAddress);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return BadRequest(new ApiValidationErrorResponse() 
            { Errors = new [] {"an Error Occured With Updating User Address"}});

        return Ok(newAddress);
    }


    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExists([FromQuery]string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }
}

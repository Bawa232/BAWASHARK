using BAWASHARK.Interfaces;
using BAWASHARK.Models;
using BAWASHARK.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BAWASHARK.Controllers
{ 
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly ITokenService _tokenService;

        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,  ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Username or password is incorrect");
            }

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });
        }
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var appUser = new AppUser
                {
                    UserName = register.Username,
                    Email = register.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, register.Password);
                if (createdUser.Succeeded)
                {
                    var userRole = await _userManager.AddToRoleAsync(appUser, "User");
                    if (userRole.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                            );
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

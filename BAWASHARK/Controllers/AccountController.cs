using BAWASHARK.Interfaces;
using BAWASHARK.Models;
using BAWASHARK.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BAWASHARK.Controllers
{ 
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager,  ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI_simple.Models.DTO;
using WebAPI_simple.Repositories;

namespace WebAPI_simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public UserController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO dto)
        {
            var identityUser = new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Username
            };

            var identityResult = await _userManager.CreateAsync(identityUser, dto.Password);

            if (identityResult.Succeeded)
            {
                if (dto.Roles != null && dto.Roles.Any())
                {
                    await _userManager.AddToRolesAsync(identityUser, dto.Roles);
                }

                return Ok("User registered successfully! You can now login.");
            }

            return BadRequest("Registration failed.");
        }

        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Username);

            if (user == null)
                return BadRequest("Invalid username or password.");

            var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!checkPassword)
                return BadRequest("Invalid username or password.");

            var roles = await _userManager.GetRolesAsync(user);
            var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

            var response = new LoginResponseDTO
            {
                JwtToken = jwtToken
            };

            return Ok(response);
        }
    }
}

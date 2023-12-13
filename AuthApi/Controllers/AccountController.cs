
using AuthApi.Dtos;
using AuthApi.Dtos.Enteties;

using AuthApi.Services;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("registers")]
        [Authorize("RequireAdminRole")]
        public async Task<IActionResult> Register([FromBody] Registration model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                AddErrorsToModelState(result.Errors);
                return BadRequest(ModelState);
            }

            await _userManager.AddToRoleAsync(user, "User");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Send confirmation email implementation(to be added)!!
            return Ok(new
            {
                Success = true,
                username = user.UserName,
                token = await _tokenService.CreateToken(user)
            });
        }


        private void AddErrorsToModelState(IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Authentication model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Username);

                if (user == null)
                {
                    return Unauthorized();
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    //return Ok(await _tokenService.GenerateToken(user));
                    return Ok(new
                    {
                        username = user.Email,
                        token = await _tokenService.CreateToken(user)
                    });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, nameof(model));
            }
        }
              
    }
}
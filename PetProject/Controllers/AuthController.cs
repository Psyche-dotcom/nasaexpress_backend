using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetProject.Data.Context;
using PetProject.Model.DTO;
using PetProject.Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PetProject.Controllers
{
    [Route("api/user/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly PetProjectDbContext _context;
        public AuthController(IAccountService accountService, PetProjectDbContext context)
        {
            _accountService = accountService;
            _context = context;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(SignUp signUp)
        {
            var registerUser = await _accountService.RegisterUser(signUp, signUp.Role);
            if (registerUser.StatusCode == 200)
            {
                return Ok(registerUser);
            }
            else if (registerUser.StatusCode == 404)
            {
                return NotFound(registerUser);
            }
            else
            {
                return BadRequest(registerUser);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInModel signIn)
        {
            var loginUser = await _accountService.LoginUser(signIn);
            if (loginUser.StatusCode == 200)
            {
                return Ok(loginUser);
            }
            else if (loginUser.StatusCode == 404)
            {
                return NotFound(loginUser);
            }
            else
            {
                return BadRequest(loginUser);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("me/info")]
        public async Task<IActionResult> MyInfo()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            var retrieveUser = await _context.Users.Include(c => c.Wallet).FirstOrDefaultAsync(c => c.Id == userIdClaim);
            return Ok(retrieveUser);

        }
        /*        [HttpPost("forgot_password")]
                public async Task<IActionResult> ForgotPassword(string email)
                {
                    var result = await _accountService.ForgotPassword(email);
                    if (result.StatusCode == 200)
                    {
                        return Ok(result);
                    }
                    else if (result.StatusCode == 404)
                    {
                        return NotFound(result);
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
                [HttpPost("reset_password")]
                public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
                {
                    var result = await _accountService.ResetUserPassword(resetPassword);
                    if (result.StatusCode == 200)
                    {
                        return Ok(result);
                    }
                    else if (result.StatusCode == 404)
                    {
                        return NotFound(result);
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
                [HttpPost("confirm-email/{email}")]
                public async Task<IActionResult> ConfirmEmail (ConfirmEmailTokenDto token,string email)
                {

                    var result = await _accountService.ConfirmEmailAsync(token.token, email);
                    if (result.StatusCode == 200)
                    {
                        return Ok(result);
                    }
                    else if (result.StatusCode == 404)
                    {
                        return NotFound(result);
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }*/
    }

}

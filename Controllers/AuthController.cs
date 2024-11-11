using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using ProjetoRotaOeste.Models;
using ProjetoRotaOeste.DTOs;

namespace ProjetoRotaOeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IConfiguration configuration, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest("Email já está em uso.");
            }

            var user = new Usuario
            {
                UserName = request.Email,
                Nome = request.Nome,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Senha);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, request.Role);
                return Ok("Usuario registrado com sucesso");
            }

            return BadRequest(result.Errors);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                var token = GenerateJwtToken(existingUser);
                var role = await _userManager.GetRolesAsync(existingUser);
                return Ok(new { Token = token, Role = role });
            }

            return Unauthorized("Tentativa de login inválida");
        }
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            // to do: configurar serviço de email (smtp)
            var user = await _userManager.FindByEmailAsync(model.Email);
            Console.WriteLine(user.Email);
            if (user == null) return BadRequest("Endereço de email inválido");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Auth", new { token, email = user.Email }, Request.Scheme);

            await _emailSender.SendEmailAsync(model.Email, "Pedindo redefinição de senha", 
                $"Reinicie sua senha pelo: <a href='{resetLink}'>link</a>");

            return Ok("Um email foi enviado para você com as instruções para redefinir sua senha.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            // o token deve ser enviado por email.
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("Endereço de email inválido");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return Ok("Senha alterada com sucesso.");
            }

            return BadRequest(result.Errors);
        }
        
        private string GenerateJwtToken(Usuario user)
        {
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expires = DateTime.Now.AddHours(double.Parse(_configuration["Jwt:ExpiresInHours"]));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

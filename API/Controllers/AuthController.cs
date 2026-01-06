using GamesStore.Domain.Entities;
using GamesStore.Domain.Enums;
using GamesStore.Infrastructure.Repositories;
using GamesStore.Infrastructure.Security;
using GamesStore.Infrastructure.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamesStore.API.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController(
        IUserRepository userRepository,
        JwtTokenGenerator jwtService) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly JwtTokenGenerator _jwtService = jwtService;

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var errors = UserValidator.Validate(
                request.Name,
                request.Email,
                request.Password
            );

            if (errors.Count != 0)
                return BadRequest(new { errors });

            if (await _userRepository.GetByEmailAsync(request.Email) != null)
                return Conflict(new { error = "Usuário já existe" });

            var user = new User(
                request.Name,
                request.Email,
                string.Empty,
                UserRole.User
            );

            var hasher = new PasswordHasher<User>();
            user.SetPassword(
                hasher.HashPassword(user, request.Password)
            );

            await _userRepository.AddAsync(user);

            return Created("", new
            {
                user.Id,
                user.Name,
                user.Email
            });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                return Unauthorized(new { error = "Credenciais inválidas" });

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password
            );

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { error = "Credenciais inválidas" });

            var token = _jwtService.Generate(user);

            return Ok(new { token });
        }
    }
}

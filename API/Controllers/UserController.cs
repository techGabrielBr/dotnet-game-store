using GamesStore.Domain.Entities;
using GamesStore.Infrastructure.Repositories;
using GamesStore.Infrastructure.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamesStore.API.Controllers
{
    [ApiController]
    [Route("/user")]
    public class UsersController(IUserRepository userRepository, IUserGameRepository userGameRepository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserGameRepository _userGameRepository = userGameRepository;

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAll()
            => Ok(await _userRepository.GetAllAsync());

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(CreateUserRequest request)
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
                request.Role
            );

            var hasher = new PasswordHasher<User>();
            user.SetPassword(
                hasher.HashPassword(user, request.Password)
            );

            await _userRepository.AddAsync(user);

            return Created("", user);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(Guid id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            user.Update(request.Name, request.Email, request.Role);
            await _userRepository.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userRepository.DeleteAsync(user);
            return NoContent();
        }

        [Authorize]
        [HttpGet("games")]
        public async Task<IActionResult> MyGames()
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var games = await _userGameRepository.GetUserGamesAsync(userId);

            return Ok(games);
        }
    }
}

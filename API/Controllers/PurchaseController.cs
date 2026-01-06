using GamesStore.Domain.Entities;
using GamesStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamesStore.API.Controllers
{
    [ApiController]
    [Route("/purchase")]
    [Authorize]
    public class PurchasesController : ControllerBase
    {
        private readonly IUserGameRepository _userGameRepository;

        public PurchasesController(IUserGameRepository userGameRepository)
        {
            _userGameRepository = userGameRepository;
        }

        [HttpPost("{gameId:guid}")]
        public async Task<IActionResult> Purchase(Guid gameId)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            if (await _userGameRepository.ExistsAsync(userId, gameId))
                return Conflict(new { error = "Jogo já adquirido" });

            await _userGameRepository.AddAsync(
                new UserGame(userId, gameId)
            );

            return Ok(new { message = "Compra realizada (fake)" });
        }
    }
}

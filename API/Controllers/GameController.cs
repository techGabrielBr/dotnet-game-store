using GamesStore.Domain.Entities;
using GamesStore.Infrastructure.Data;
using GamesStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesStore.API.Controllers
{

    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly AppDbContext _context;

        public GameController(
            IGameRepository gameRepository,
            AppDbContext context)
        {
            _gameRepository = gameRepository;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameRepository.GetAllAsync();
            return Ok(games);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                return NotFound("Jogo não encontrado");

            return Ok(game);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(CreateGameRequest request)
        {
            if (request.Price <= 0)
                return BadRequest("Preço inválido");

            var game = new Game(request.Title, request.Price);

            await _gameRepository.AddAsync(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
        }

        [HttpPost("{id:guid}/buy")]
        [Authorize]
        public async Task<IActionResult> FakeBuy(Guid id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                return NotFound("Jogo não encontrado");

            return Ok(new
            {
                Message = "Compra simulada com sucesso",
                GameId = game.Id,
                PricePaid = game.CurrentPrice
            });
        }
    }
}

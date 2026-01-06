using GamesStore.Domain.Entities;
using GamesStore.Infrastructure.Data;
using GamesStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesStore.API.Controllers
{
    [ApiController]
    [Route("api/promotion")]
    [Authorize(Policy = "AdminOnly")]
    public class PromotionController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly AppDbContext _context;

        public PromotionController(
            IGameRepository gameRepository,
            IPromotionRepository promotionRepository,
            AppDbContext context)
        {
            _gameRepository = gameRepository;
            _promotionRepository = promotionRepository;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePromotionRequest request)
        {
            var game = await _gameRepository.GetByIdAsync(request.GameId);
            if (game == null)
                return NotFound("Jogo não encontrado");

            if (request.DiscountedPrice >= game.CurrentPrice)
                return BadRequest("Preço promocional deve ser menor que o atual");

            var promotion = new Promotion(
                game.Id,
                request.DiscountedPrice,
                request.StartsAt,
                request.EndsAt
            );

            game.ApplyPromotion(promotion);

            await _promotionRepository.AddAsync(promotion);
            await _context.SaveChangesAsync();

            return Ok(promotion);
        }
    }
}

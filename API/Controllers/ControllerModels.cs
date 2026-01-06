using GamesStore.Domain.Enums;

namespace GamesStore.API.Controllers
{

    #region AUTH

    public record RegisterRequest(
        string Name,
        string Email,
        string Password
    );

    public record LoginRequest(
        string Email,
        string Password
    );

    public record AuthResponse(
        string Token,
        DateTime ExpiresAt
    );

    #endregion

    #region USERS

    public record CreateUserRequest(
        string Name,
        string Email,
        string Password,
        UserRole Role
    );

    public record UpdateUserRequest(
        string Name,
        string Email,
        UserRole Role
    );

    public record UserResponse(
        Guid Id,
        string Name,
        string Email,
        string Role
    );

    #endregion

    #region GAMES

    public record CreateGameRequest(
        string Title,
        decimal Price
    );

    public record UpdateGamePriceRequest(
        decimal NewPrice
    );

    public record GameResponse(
        Guid Id,
        string Title,
        decimal CurrentPrice,
        decimal? PreviousPrice
    );

    #endregion

    #region PURCHASES

    public record PurchaseResponse(
        Guid GameId,
        DateTime AcquiredAt
    );

    #endregion

    #region PROMOTIONS

    public record CreatePromotionRequest(
        Guid GameId,
        decimal DiscountedPrice,
        DateTime StartsAt,
        DateTime EndsAt
    );

    #endregion
}

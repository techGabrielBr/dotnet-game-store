using GamesStore.Extensions;
using GamesStore.Infrastructure.Data;
using GamesStore.Infrastructure.Repositories;
using GamesStore.Infrastructure.Security;
using GamesStore.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// ======================
// Database (Supabase)
// ======================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ======================
// JWT
// ======================
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

// ======================
// Controllers + Swagger
// ======================
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "GamesStore API",
        Version = "v1"
    });
});

builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IUserGameRepository, UserGameRepository>();
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();

// ======================
var app = builder.Build();
// ======================

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Database.Migrate();
    DbSeeder.SeedAdmin(context);
}

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
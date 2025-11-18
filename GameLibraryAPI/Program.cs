using GameLibraryAPI.Mapping;
using GameLibraryAPI.Middleware;
using GameLibraryAPI.Repositories;
using GameLibraryAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GameLibraryAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
    
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(v => v.Value.Errors.Count > 0)
            .Select(v => new
            {
                field = v.Key,
                error = v.Value.Errors.First().ErrorMessage
            });

        return new BadRequestObjectResult(new
        {
            status = 400,
            message = "Ошибка валидации",
            errors = errors
        });
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GameLibraryDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var jwtKey = builder.Configuration["Jwt:Key"] ?? "MySuperSecretKey";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "GameLibraryAPI";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();   
app.UseAuthErrorHandling();                     

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

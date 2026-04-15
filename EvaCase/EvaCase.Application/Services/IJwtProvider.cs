using EvaCase.Application.Features.Auth.Login;
using EvaCase.Domain.Entities;

namespace EvaCase.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(AppUser user);
    }
}

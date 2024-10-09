using Api006.Service.Dtos;
using Api006.Service.Dtos.Auth;
using Api006.Service.Responses;

namespace Api006.Service.Services.Abstractions
{
    public interface IAuthService
    {
        public Task<ApiResponse> Register(RegisterDto dto);
        public Task<ApiResponse> Login(LoginDto dto);
    }
}

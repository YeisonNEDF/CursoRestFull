using Core.DTOs.User;
using Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponseDto>> AuthenticateAsync(AuthenticationRequestDto request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequestDto request, string origin);
    }
}

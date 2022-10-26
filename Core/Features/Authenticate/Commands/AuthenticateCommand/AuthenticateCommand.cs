using Core.DTOs.User;
using Core.Interfaces;
using Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Authenticate.Commands.AuthenticateCommand
{
    public class AuthenticateCommand : IRequest<Response<AuthenticationResponseDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Response<AuthenticationResponseDto>>
    {
        private readonly IAccountService _accountService;

        public AuthenticateCommandHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Response<AuthenticationResponseDto>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await _accountService.AuthenticateAsync(new AuthenticationRequestDto
            {
                Email = request.Email,
                Password = request.Password
            }, request.IpAddress);
        }
    }
}

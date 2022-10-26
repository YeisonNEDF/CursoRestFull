using Core.DTOs.User;
using Core.Features.Authenticate.Commands.AuthenticateCommand;
using Core.Interfaces;
using Moq;
using System.Net;

namespace TestProject1
{
    [TestClass]
    public class AuthenticateCommandTest
    {
        [TestMethod]
        public async Task HandleSuceeded()
        {
            //Arrange
            var IpAddress = "0.0.0.1";
            AuthenticateCommand request = new AuthenticateCommand
            {
                Email = "userAdmin@hotmail.com",
                Password = "123Pa$Word",
                IpAddress = "0.0.0.1"
            };
            AuthenticationResponseDto response = new AuthenticationResponseDto();
            Mock <IAccountService> mockAccountService = new Mock<IAccountService>();
            mockAccountService.Setup(a => a.AuthenticateAsync(new AuthenticationRequestDto {
                Email = "userAdmin@hotmail.com",
                Password = "123Pa$Word"
            }, "0.0.0.1"));
            CancellationToken cancellationToken = new CancellationToken();
            AuthenticateCommandHandler authenticateCommand = new AuthenticateCommandHandler(mockAccountService.Object);
            //AuthenticationResponseDto authenticationResponseDto = new AuthenticationResponseDto();
            //Action
            var result = await authenticateCommand.Handle(request, cancellationToken );

            //assert

        }
    
    }
}
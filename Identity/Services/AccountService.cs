using Core.Domain.Settings;
using Core.DTOs.User;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using Core.Wrappers;
using Identity.Helpers;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;

        public AccountService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
        }

        public async Task<Response<AuthenticationResponseDto>> AuthenticateAsync(AuthenticationRequestDto request, string ipAddress)
        {
           var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                throw new ApiException($"No hay una cuenta registrada con el email {request.Email}.");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new ApiException($"Las credenciales del usuario no son validas {request.Email}");
            }
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            AuthenticationResponseDto responseDto = new AuthenticationResponseDto();
            responseDto.Id = user.Id;
            responseDto.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            responseDto.Email = user.Email;
            responseDto.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            responseDto.Roles = rolesList.ToList();
            responseDto.IsVerified = user.EmailConfirmed;

            var refrehsToken = GenerateRefreshTokenDto(ipAddress);
            responseDto.RefreshToken = refrehsToken.Token;
            return new Response<AuthenticationResponseDto>(responseDto, $"Usuario autenticado {user.UserName}");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequestDto request, string origin)
        {
            var userWithEqualsName = await _userManager.FindByNameAsync(request.UserName);
            if(userWithEqualsName != null)
            {
                throw new ApiException($"El nombre de usuario {request.UserName} ya fue registrado previamente.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Nombre = request.Nombre,
                Apellido =  request.Apellido,
                UserName = request.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userWithEqualsEmail = await _userManager.FindByEmailAsync(request.Email);
            if(userWithEqualsEmail != null)
            {
                throw new ApiException($"El email {request.Email} ya fue registrado previamente.");
            }
            else
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    return new Response<string>(user.Id, message: $"Usuario registrado correctamente. {request.UserName}.");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }

        private RefreshTokenDto GenerateRefreshTokenDto(string ipAddress)
        {
            return new RefreshTokenDto
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }
}

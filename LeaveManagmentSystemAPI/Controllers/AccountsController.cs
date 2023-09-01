﻿using AutoMapper;
using Domain.Requests;
using Domain.Response;
using LeaveManagmentSystemAPI.Data;
using LeaveManagmentSystemAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LeaveManagmentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;

        public AccountsController(ILogger<AccountsController> logger, UserManager<Employee> userManager, IMapper mapper, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Unauthorized(new AuthenticationResponse { ErrorMessage = "Invalid Authentication" });

            var signingCredentials = _jwtHandler.GetSigningCredentials();

            IList<string> userRoles = await _userManager.GetRolesAsync(user) ?? new List<string>();

            var claims = _jwtHandler.GetClaims(user);
            claims.Add(new Claim("userId", user.Id.ToString()));
            claims.Add(new Claim("username", user.UserName));
            claims.Add(new Claim("UserRole", string.Join(',', userRoles).ToString()));
            claims.Add(new Claim(ClaimTypes.Role, string.Join(',', userRoles).ToString()));
            
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthenticationResponse { IsAuthSuccessful = true, 
                Token = token, UserName = user.UserName, Firstname = user.Firstname,
                Lastname = user.Lastname, Email = user.Email,
                UserRole = string.Join(',', userRoles).ToString() 
            });
        }

    }
}

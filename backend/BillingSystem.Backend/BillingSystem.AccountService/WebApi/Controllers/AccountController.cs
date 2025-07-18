﻿using BillingSystem.AccountService.Applicaiton;
using BillingSystem.AccountService.Applicaiton.Service.Implementation;
using BillingSystem.AccountService.Applicaiton.Service.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingSystem.AccountService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authManager;
        private readonly IValidator<UserForRegisterDto> _userValidator;

        public AccountController(IAuthService authManager, IValidator<UserForRegisterDto> userValidator)
        {
            _authManager = authManager;
            _userValidator = userValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto dto)
        {
            var userToLogin = await _authManager.Login(dto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = await _authManager.CreateAccessToken(userToLogin.Data);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }


            return Ok(new LoginResponseDto { Token = result.Data, UserId = userToLogin.Data.UserId });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var isValid = await _userValidator.ValidateAsync(userForRegisterDto);
            if (!isValid.IsValid)
            {
                return BadRequest(isValid.Errors);
            }
            var result = await _authManager.Register(userForRegisterDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            
            return Ok(result.Data);
        }

    }
}

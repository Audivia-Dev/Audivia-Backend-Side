﻿using Audivia.Domain.DTOs;
using Audivia.Domain.ModelRequests.Auth;
using Audivia.Domain.ModelResponses.Auth;
using System.Security.Claims;

namespace Audivia.Application.Services.Interface
{
    public interface IAuthService
    {
        // login
        Task<LoginResponse> LoginWithEmailAndPassword(LoginRequest request);

        Task<LoginResponse> LoginWithGoogle(string token);

        // register
        Task<RegisterResponse> Register(RegisterRequest request);

        //confirm email
        Task<ConfirmEmailResponse> VerifyEmail(ConfirmEmailRequest request);

        // get profile
        Task<UserDTO?> GetCurrentUserAsync(ClaimsPrincipal userClaims);

        Task<UserDTO?> GetCurrentUserAsync();
        //confirm otp
        Task SendResetPasswordOtpAsync(ForgotPasswordRequest request);
        Task<OTPConfirmResponse> VerifyResetPasswordOtpAsync(ConfirmEmailOTP request);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
    }
}

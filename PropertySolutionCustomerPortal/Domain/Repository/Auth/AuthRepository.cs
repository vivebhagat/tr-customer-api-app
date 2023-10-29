using Azure.Core;
using Castle.Core.Resource;
using Castle.Core.Smtp;
using Dapper;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PropertySolutionCustomerPortal.Api.Dto.Auth;
using PropertySolutionCustomerPortal.Domain.Entities.Auth;
using PropertySolutionCustomerPortal.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Domain.Entities.Shared;
using PropertySolutionCustomerPortal.Domain.Entities.Users;
using PropertySolutionCustomerPortal.Domain.Service.Email;
using PropertySolutionCustomerPortal.Infrastructure.DataAccess;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace PropertySolutionCustomerPortal.Domain.Repository.Auth
{
    public interface IAuthRepository
    {
        Task<string> RegisterUser(BaseApplicationUser applicationUser);
        Task<bool> UpdateUser(BaseApplicationUser applicationUser, string userId);
        Task<BaseApplicationUser> ValidateUser(string userName, string password);
        string GenerateJwtToken(BaseApplicationUser user, string authRole);
        Task<bool> AddRefreshToken(RefreshToken refreshToken);
        Task<bool> RemoveRefreshToken(string userId);
        Task<bool> DeletUser(string userId);
        Task<bool> EmailConfirmation(EmailConfirmation @object);
        Task<bool> ResetPassword(ResetPassword resetPassword);
        Task<bool> ForgotPassword(string email);
        Task<bool> SendConfirmationEmail(BaseApplicationUser user);
        Task<BaseApplicationUser> GetUserByUserId(string userId);
    }

    public class AuthRepository : IAuthRepository
    {
        IAuthDbContext _authDb;
        private readonly UserManager<BaseApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        IAzureEmailService _azureEmailService;
        IWebHostEnvironment _environment;

        public AuthRepository(UserManager<BaseApplicationUser> userManager, IConfiguration configuration, IAuthDbContext authDb, IAzureEmailService azureEmailService, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _configuration = configuration;
            _authDb = authDb;
            _azureEmailService = azureEmailService;
            _environment = environment;
        }

        private string LoadEmailTemplate(string templateFileName)
        {
            try
            {
                string templatePath = Path.Combine(_environment.ContentRootPath, "Files", templateFileName);
                using StreamReader reader = new StreamReader(templatePath);
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> RegisterUser(BaseApplicationUser applicationUser)
        {
            try
            {
                var existingUser = await _userManager.FindByNameAsync(applicationUser.UserName);

                if (existingUser != null)
                    throw new Exception("User name already in use.");

                var result = await _userManager.CreateAsync(applicationUser, applicationUser.Password);

                if (result.Succeeded)
                {
                    var newUser = await _userManager.FindByNameAsync(applicationUser.UserName);
                    //send verify email
                    await SendConfirmationEmail(newUser);
                    return newUser.Id;
                }
                else
                    throw new Exception("Error while creating user.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> SendConfirmationEmail(BaseApplicationUser user)
        {
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = HttpUtility.UrlEncode(token);
                string baseUrl = _configuration["WebPortal"];//"http://localhost:4200/";//
                string activationLink = $"{baseUrl}/email-confirmation/?email={user.Email}&token={encodedToken}";

                string emailBody = LoadEmailTemplate("EmailConfirmationTemplate.html");

                if (string.IsNullOrEmpty(emailBody))
                    return false;

                emailBody = ReplaceConfimationEmailTokens(emailBody, user.Email, activationLink);

                if (string.IsNullOrEmpty(emailBody))
                    return false;

                if (!await Send(user.Email, "Verify Email", emailBody))
                    return false;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private async Task<bool> Send(string email, string subject, string emailBody)
        {
            try
            {
                var emailRequest = new EmailRequest
                {
                    RecieverAddresses = new List<string>(),
                    PrimaryRecieverAddress = email,
                    Subject = subject,
                    Body = emailBody
                };

                await _azureEmailService.SendEmail(emailRequest);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private string ReplaceConfimationEmailTokens(string emailBody, string user, string activationLink)
        {
            try
            {
                emailBody = emailBody.Replace("{User}", user.Trim());
                emailBody = emailBody.Replace("{activationLink}", activationLink);
                return emailBody;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private string ReplaceForgotpasswordTokens(string emailBody, string user, string activationLink)
        {
            try
            {
                emailBody = emailBody.Replace("{User}", user.Trim());
                emailBody = emailBody.Replace("{activationLink}", activationLink);
                return emailBody;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);
            string baseUrl =  _configuration["WebPortal"]; //"http://localhost:4200/";//
            string activationLink = $"{baseUrl}/reset-password/?email={user.Email}&token={encodedToken}";

            string emailBody = LoadEmailTemplate("ForgotPasswordTemplate.html");

            if (string.IsNullOrEmpty(emailBody))
                return false;

            emailBody = ReplaceForgotpasswordTokens(emailBody, user.Email, activationLink);

            if (string.IsNullOrEmpty(emailBody))
                return false;

            if (!await Send(user.Email, "Reset Password", emailBody))
                return false;

            return true;
        }

        public async Task<bool> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user == null)
                return false;

            user.Password = resetPassword.Password;

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

            if (!resetPassResult.Succeeded)
                return false;

            return true;
        }

        public async Task<bool> EmailConfirmation(EmailConfirmation @object)
        {
            var user = await _userManager.FindByEmailAsync(@object.Email);

            if (user == null)
                return false;

            if (await _userManager.IsEmailConfirmedAsync(user))
                return false;

            var confirmResult = await _userManager.ConfirmEmailAsync(user, @object.Token);

            if (!confirmResult.Succeeded)
                return false;

            return true;
        }

        public async Task<bool> UpdateUser(BaseApplicationUser applicationUser, string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                user.UserName = applicationUser.Email;
                user.Email = applicationUser.Email;

                if (user == null)
                    throw new Exception("Failed to update user details. User not found.");

                if (applicationUser.Password != null)
                {
                    var passwordChangeResult = await _userManager.RemovePasswordAsync(user);

                    if (!passwordChangeResult.Succeeded)
                        throw new Exception("Error while changing password.");

                    var passwordChanegd = await _userManager.AddPasswordAsync(user, applicationUser.Password);

                    if (!passwordChanegd.Succeeded)
                        throw new Exception("Error while changing password.");

                }

                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                    return true;

                throw new Exception("Error while updating user.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> AddRefreshToken(RefreshToken refreshToken)
        {
            var result = await RemoveRefreshToken(refreshToken.UserId);

            if (!result)
                return false;

            _authDb.RefreshTokens.Add(refreshToken);

            return await _authDb.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string userId)
        {
            var existingRefreshToken = _authDb.RefreshTokens.Where(m => m.UserId == userId).FirstOrDefault();

            if (existingRefreshToken == null)
                return true;

            _authDb.RefreshTokens.Remove(existingRefreshToken);
            return await _authDb.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<BaseApplicationUser> GetUserByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            return user;
        }

        public async Task<BaseApplicationUser> ValidateUser(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return null;

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
                return user;

            return null;
        }


        public string GenerateJwtToken(BaseApplicationUser user, string authRole)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JWT");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("IsVerified", user.EmailConfirmed.ToString(), ClaimValueTypes.Boolean),
                    new Claim("DataRoute", user.DataRoute)
                };

                if (!string.IsNullOrEmpty(authRole))
                    claims.Add(new Claim("authRole", authRole));

                DateTime specificLocalTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["ValidIssuer"],
                    audience: jwtSettings["ValidAudience"],
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(token);

            }
            catch (Exception e)
            {
                throw new Exception("Failed to generate token. " + e.Message);
            }
        }
    }
}
﻿namespace PropertySolutionCustomerPortal.Domain.Entities.Auth
{
    public class ResetPassword
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}

﻿using Microsoft.AspNetCore.Identity;
using Controlinventarioissn.Data.Entities;
using Controlinventarioissn.Models;

namespace Controlinventarioissn.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        //Task<User> AddUserAsync(AddUserViewModel model); //sobrecargar

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

       // Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        //Task<IdentityResult> UpdateUserAsync(User user);

        //Task<User> GetUserAsync(Guid userId);

       // Task<string> GenerateEmailConfirmationTokenAsync(User user);

        //Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        //Task<string> GeneratePasswordResetTokenAsync(User user);

       // Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);


    }
}

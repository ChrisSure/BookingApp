﻿using BookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApp.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUserRoleAsync(string userId, string role);
        Task AddUsersRoleAsync(ApplicationUser user, IEnumerable<string> roles);
        Task ChangePassword(string userId, string currentpassword, string newpassword);
        Task<bool> CheckPassword(ApplicationUser user, string password);
        Task CreateAdmin(ApplicationUser user, string password);
        Task CreateUser(ApplicationUser user);
        Task CreateUser(ApplicationUser user, string password);
        Task DeleteUser(ApplicationUser user);
        Task DeleteUser(string id);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<ApplicationUser> GetUserById(string id);
        Task<IList<string>> GetUserRoles(ApplicationUser user);
        Task<IList<string>> GetUserRolesById(string userId);
        Task<IEnumerable<ApplicationUser>> GetUsersList();
        Task<bool> IsInRoleAsync(ApplicationUser user, string role);
        Task RemoveUserRoleAsync(string userId, string role);
        Task RemoveUsersRoleAsync(ApplicationUser user, IEnumerable<string> roles);
        Task ResetUserPassword(string userId, string token, string newPassword);
        Task UpdateUser(ApplicationUser user);
        Task UserApproval(string userId, bool IsApproved);
        Task UserBlocking(string userId, bool IsBlocked);
    }
}
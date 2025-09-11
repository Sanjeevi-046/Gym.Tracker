using Gym.Tracker.Core.ServiceModel;
using Gym.Tracker.Data.Context;
using Gym.Tracker.Data.Entities;
using Gym.Tracker.Security;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Role = Gym.Tracker.Common.Enum.RoleType;

namespace Gym.Tracker.Core.Services.v1
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// For Validating the Role / Delegate(Func) is fater for specific validation
        /// </summary>
        Func<long, bool> isValidRole = roleId => roleId == (int)Role.Admin || roleId == (int)Role.Trainer || roleId == (int)Role.Staff ||
        roleId == (int)Role.Member ||  roleId == (int)Role.Guest;


        // Helper to check if a role is restricted (Admin, Trainer, Staff)
        private bool IsRestrictedRole(long roleId)
        {
            var restrictedRoles = new List<long> { (long)Role.Admin, (long)Role.Trainer, (long)Role.Staff }; 
            return restrictedRoles.Contains(roleId);
        }

        // Helper to check if current user is Admin
        private bool IsAdmin(long roleId)
        {
            return roleId == (long)Role.Admin;
        }
        //Helper to verify Password 
        private bool VerifyPassword (string password , byte[] passwordBytes)
        {
            return PasswordHasher.VerifyPassword(password, passwordBytes);           
        }

        private async  Task<UserVerificationResult> IsExistingUser (string email , string password)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(_ => _.Email.Equals(email));
            if (existingUser == null) 
            {
                return new UserVerificationResult { IsNewUser=true};
            }
            bool isValidPassword = VerifyPassword(password, existingUser.Password);
            return new UserVerificationResult { IsExistingUser = true , IsPasswordInvalid = isValidPassword};

        }

        /// <summary>
        /// Save the User
        /// </summary>
        /// <param name="saveUserServiceModel"></param>
        /// <param name="loginUserId"></param>
        /// <param name="loginUserRoleId"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<dynamic> SaveUserAsync(UserRequestModel saveUserServiceModel, long loginUserId, long loginUserRoleId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(saveUserServiceModel.FirstName);

                // Only Admin can create Admin/Trainer/Staff/Member/Guest
                if (IsRestrictedRole(saveUserServiceModel.RoleId) && !IsAdmin(loginUserRoleId))
                    throw new UnauthorizedAccessException("You are not authorized to create this type of user.");

                if (!isValidRole(saveUserServiceModel.RoleId)) 
                    throw new ArgumentException($"Invalid RoleId: {saveUserServiceModel.RoleId}");

                UserVerificationResult existingUser = await IsExistingUser(saveUserServiceModel.Email, saveUserServiceModel.Password);
                if(existingUser.IsExistingUser && !existingUser.IsPasswordInvalid)
                {
                    return "Password Invalid";
                }
                if (existingUser.IsNewUser)
                {
                    return await SaveNewUser(saveUserServiceModel,loginUserId);
                }
                else
                {
                    return "Existing User";
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }


        private async  Task<object> SaveNewUser(UserRequestModel saveUserServiceModel,long loginUserId)
        {
            byte[] hashedPassword = PasswordHasher.HashPassword(saveUserServiceModel.Password);
            User newUser = new ()
            {
                FirstName = saveUserServiceModel.FirstName,
                LastName = saveUserServiceModel.LastName,
                Email = saveUserServiceModel.Email,
                MiddleName = saveUserServiceModel.MiddleName,
                FullName = saveUserServiceModel.FirstName +" "+saveUserServiceModel.MiddleName + " " + saveUserServiceModel.LastName,
                PhoneNumber = saveUserServiceModel.PhoneNumber,
                Gender = saveUserServiceModel.Gender,
                CountryId = saveUserServiceModel.CountryId,
                UserTypeId=saveUserServiceModel.UserTypeId,
                Nationality = saveUserServiceModel.Nationality,
                CreatedAt = DateTime.Now,
                CreatedBy = loginUserId,
                RoleId = isValidRole(saveUserServiceModel.RoleId) ? saveUserServiceModel.RoleId : throw new ValidationException("No Role Found"),
                IsActive = true,
                IsMailNotificationAllowed = true,
                IsForcePwdChange = false,
                IsTerminated = false,
                IsUserLocked=false,
                Password = hashedPassword
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }
    }
}

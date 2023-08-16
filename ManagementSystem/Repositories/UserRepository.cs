using ManagementSystem.Data;
using ManagementSystem.Exceptions;
using ManagementSystem.Helpers;
using ManagementSystem.Interfaces.Repositories;
using ManagementSystem.Models.Entities;
using ManagementSystem.Models.UserDTO;
using ManagementSystem.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context context;

        public UserRepository(Context _context) 
        {
            context = _context;
        }

        public async Task<bool> CheckUserExistAsync(string email)
        {
            return await context.Users.AnyAsync(u => u.Email.ToUpper() == email.Trim().ToUpper());
        }

        public async Task CreateUserAsync(SignUpModel signUpModel)
        {
            if (CheckUserExistAsync(signUpModel.Email).Result)
            {
                throw new InternalException("Such user already exists");
            }
            string passErrors = PasswordValidator.CheckPasswordStrength(signUpModel.Password);
            if (!string.IsNullOrEmpty(passErrors))
            {
                throw new InternalException(passErrors);
            }
            UserEntity userModel = new()
            {
                Email = signUpModel.Email.Trim(),
                FirstName = signUpModel.Firstname.Trim(),
                LastName = signUpModel.Lastname.Trim(),
                Role = Models.Enums.Roles.Admin,
                Password = BCrypt.Net.BCrypt.HashPassword(signUpModel.Password)
            };
            context.Users.Add(userModel);
            await context.SaveChangesAsync();
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == email.Trim().ToUpper());
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<UserInfoModel>> GetUsersAsync()
        {
            return await context.Users.Select(user => new UserInfoModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
            }).ToListAsync();
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
    }
}

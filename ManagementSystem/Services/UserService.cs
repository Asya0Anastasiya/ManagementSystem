using ManagementSystem.Data;
using ManagementSystem.Entities;
using ManagementSystem.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ManagementSystem.Services
{
    public class UserService : IUser
    {
        private readonly Context context;
        private readonly UserManager<UserIdentity> userManager;
        private readonly SignInManager<UserIdentity> signInManager;
        private readonly IHttpContextAccessor httpContext;

        public UserService(Context _context, 
                           UserManager<UserIdentity> _userManager,
                           SignInManager<UserIdentity> _signInManager,
                           IHttpContextAccessor _httpContext)
        {
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
            httpContext = _httpContext;
        }

        //USING IDENTITYCORE
        public async Task<IdentityResult> CreateIdentity(SignUpEntity userLogin)
        {
            var user = new UserIdentity()
            {
                Firstname = userLogin.Firstname,
                Lasttname = userLogin.Lastname,
                Email = userLogin.Email,
                UserName = userLogin.Email
            };
            var result = await userManager.CreateAsync(user, userLogin.Password);
            return result;
        }

        public List<UserIdentity> GetAll()
        {
            return userManager.Users.ToList();
        }

        //public UserEntity GetById(int id)
        //{
        //    var user = context.AppUsers.Find(id);

        //    if (user != null) return user;
        //    return null;
        //}

        public async Task<SignInResult> PasswordSignInAsync(SignInEntity signInEntity)
        {
            var result = await signInManager.PasswordSignInAsync(signInEntity.Email, signInEntity.Password, signInEntity.RememberMe, false);
            return result;
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public string GetUserId()
        {
            return httpContext.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public bool IsAuthenticated()
        {
            return httpContext.HttpContext.User.Identity.IsAuthenticated;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePassword changePassword)
        {
            var userId = GetUserId();
            var user = await userManager.FindByIdAsync(userId);
            return await userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
        }
    }
}

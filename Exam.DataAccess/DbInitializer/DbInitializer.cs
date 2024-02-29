using Exam.DataAccess.Data;
using Exam.Entities.Enums;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exam.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public async Task Initialize()
        {
            //create roles if they are not created
            if (!await _roleManager.RoleExistsAsync(Roles.Admin.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Examinee.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

                //if roles are not created, then we will create admin user as well
                var userName = "admin_examApp";
                var email = "admin@gmail.com";
                var password = "Admin@12345";

                if (await _userManager.FindByEmailAsync(email) == null)
                {
                    AppUser user = new()
                    {
                        UserName = userName,
                        Email = email,
                    };

                    await _userManager.CreateAsync(user, password);
                    await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }
            }

            return;
        }
    }
}

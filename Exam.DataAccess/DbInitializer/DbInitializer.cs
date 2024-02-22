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

        public async void Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    await _db.Database.MigrateAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(Roles.Admin.ToString()).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Roles.Examiner.ToString())).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString())).GetAwaiter().GetResult();

                //if roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new AppUser
                {
                    UserName = "admin_examApp",
                    Email = "admin@gmail.com"
                }, "Admin@12345").GetAwaiter().GetResult();

                AppUser user = await _userManager.FindByEmailAsync("admin@gmail.com");
                _userManager.AddToRoleAsync(user, Roles.Admin.ToString()).GetAwaiter().GetResult();
            }

            return;
        }
    }
}

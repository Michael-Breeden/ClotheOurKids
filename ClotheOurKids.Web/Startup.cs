using ClotheOurKids.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Web;

[assembly: OwinStartupAttribute(typeof(ClotheOurKids.Web.Startup))]
namespace ClotheOurKids.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Administrator"))
            {
                var role = new IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);

                string AdminUserName = "givekidsclothes@gmail.com";
                var objAdminUser = userManager.FindByEmail(AdminUserName);
                if (objAdminUser != null)
                {
                    userManager.AddToRole(objAdminUser.Id, "Administrator");
                }
            }

            if(!roleManager.RoleExists("Officer"))
            {
                var role = new IdentityRole();
                role.Name = "Officer";
                roleManager.Create(role);
            }

            if(!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }

    }
}

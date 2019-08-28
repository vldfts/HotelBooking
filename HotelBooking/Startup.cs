using HotelBooking.Entities;
using HotelBooking.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(HotelBooking.Startup))]
namespace HotelBooking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            ConfigureAuth(app);
            CreateDefaultUsers();
        }
        private void CreateDefaultUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("admin"))
            {
                var role = new IdentityRole { Name = "admin" };
                roleManager.Create(role);

                var admin = new ApplicationUser { Email = "h@gmail.com", UserName = "h@gmail.com" };
                var result = UserManager.Create(admin, "aS123_");
                if (result.Succeeded)
                {
                    UserManager.AddToRole(admin.Id, role.Name);
                }
            }
            if (!roleManager.RoleExists("user"))
            {
                roleManager.Create(new IdentityRole { Name = "user" });
            }


        }
    }
}

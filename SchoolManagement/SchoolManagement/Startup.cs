using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SchoolManagement.Models;

[assembly: OwinStartupAttribute(typeof(SchoolManagement.Startup))]
namespace SchoolManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            CreateRoleAndUser();
            ConfigureAuth(app);
        }

        public void CreateRoleAndUser()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var roleAdmin = new IdentityRole()
                {
                    Name = "Admin",
                };
                roleManager.Create(roleAdmin);

                var userAdmin = new ApplicationUser()
                {
                    UserName="Admin",
                    Email = "adminemail@gmail.com",
                    BirthDate = DateTime.Now,

                };
                var password = "Password@123";

                var userCreation =userManager.Create(userAdmin, password);

                if(userCreation.Succeeded)
                {
                    var addingToRole = userManager.AddToRole(userAdmin.Id, "Admin");
                }

            }

            if(!roleManager.RoleExists("Teacher"))
            {
                var roleTeacher = new IdentityRole()
                {
                    Name = "Teacher",
                };
                roleManager.Create(roleTeacher);
            }

            if(!roleManager.RoleExists("Student"))
            {
                var roleStudent = new IdentityRole()
                {
                    Name = "Student",
                };
                roleManager.Create(roleStudent);
            }


        }
    }
}

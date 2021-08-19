﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace kursach.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
        public bool IsAdminIn { get; set; }
        public bool IsBlocked { get; set; }

        //public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<UserCollection> UserCollections { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { }

        public DbSet<Collection> Collections { get; set; }
        public DbSet<UserCollection> UserCollection { get; set; }
        public DbSet<CollectionTopic> CollectonTopic { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
//#if DEBUG
//            context.Database.Delete();
//#endif
            base.InitializeDatabase(context);
            Seed(context);
        }

        protected override void Seed(ApplicationDbContext context)
        {

            if (CheckIfInitialized(context))
            {
                return;
            }

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { Email = "bigadmin@gmail.com", UserName = "bigadmin@gmail.com", IsAdminIn = true, IsBlocked = false };
            string password = "Vadya123";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            context.CollectonTopic.AddOrUpdate(new CollectionTopic { Name = "Алкоголь" });
            context.CollectonTopic.AddOrUpdate(new CollectionTopic { Name = "Книги" });
            context.CollectonTopic.AddOrUpdate(new CollectionTopic { Name = "Мячи" });
            context.SaveChanges();
            context.Collections.AddOrUpdate(new Collection { Name = "Чайка", Description = "Это кника напасана А. Чеховым", CollectonTopicId = 2});
            context.Collections.AddOrUpdate(new Collection { Name = "Водка", Description = "Это алкогольный напиток, 40%", CollectonTopicId = 1});
            context.Collections.AddOrUpdate(new Collection { Name = "Adidas UCL 21 Pro SALA", Description = "Мяч для футбола на траве", CollectonTopicId  = 3});
            context.Collections.AddOrUpdate(new Collection { Name = "Война и мир", Description = "Это кника напасана Л. Толстовым", CollectonTopicId = 2 });
            context.Collections.AddOrUpdate(new Collection { Name = "Виски", Description = "Это алкогольный напиток, 60%", CollectonTopicId = 1 });
            context.Collections.AddOrUpdate(new Collection { Name = "Select Briliant", Description = "Мяч для футзала", CollectonTopicId = 3 });
            context.SaveChanges();

            base.Seed(context);
        }

        private bool CheckIfInitialized(ApplicationDbContext context)
        {
            return context.Users.Any(u => u.Email.Equals("bigadmin@gmail.com"));
        }
    }
}
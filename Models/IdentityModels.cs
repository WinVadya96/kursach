using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace kursach.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
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
    }

    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TopicName { get; set; } 

    }

    public class Topic
    {
        public int TopicId { get; set; }
        public string String1 { get; set; }
        public string String2 { get; set; }
        public string String3 { get; set; }
        public int? Number1 { get; set; }
        public int? Number2 { get; set; }
        public int? Number3 { get; set; }
        public DateTime? Date1 { get; set; }
        public DateTime? Date2 { get; set; }
        public DateTime? Date3 { get; set; }
        public bool Checkbox1 { get; set; }
        public bool Checkbox2 { get; set; }
        public bool Checkbox3 { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Collection> Collections { get; set; }
        public DbSet<Topic> Topics { get; set; }

        // Добавление инициализатора
        //public class CollectionDbInitialiser : DropCreateDatabaseAlways<ApplicationDbContext>
        //{
        //    protected override void Seed(ApplicationDbContext db)
        //    {
        //        db.Collections.Add(new Collection { Name = "Вщйна и мир", })
        //    }
        //}

        public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var role1 = new IdentityRole { Name = "admin" };
                var role2 = new IdentityRole { Name = "user" };
                roleManager.Create(role1);
                roleManager.Create(role2);

                var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
                string password = "Vasya123";
                var result = userManager.Create(admin, password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, role1.Name);
                    userManager.AddToRole(admin.Id, role2.Name);
                }
                base.Seed(context);
            }
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
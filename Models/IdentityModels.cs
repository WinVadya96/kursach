using System;
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

    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TopicName { get; set; } 

        //public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<UserCollection> UserCollections { get; set; }
        public virtual ICollection<CollectionTopic> CollectonTopics { get; set; }
    }

    public class Topic
    {
        public int TopicId { get; set; }
        public string String1 { get; set; }
        public bool BoolForString1 { get; set; }
        public string String2 { get; set; }
        public bool BoolForString2 { get; set; }
        public string String3 { get; set; }
        public bool BoolForString3 { get; set; }
        public int? Number1 { get; set; }
        public bool BoolForNumber1 { get; set; }
        public int? Number2 { get; set; }
        public bool BoolForNumber2 { get; set; }
        public int? Number3 { get; set; }
        public bool BoolForNumber3 { get; set; }
        public DateTime? Date1 { get; set; }
        public bool BoolForDateTime1 { get; set; }
        public DateTime? Date2 { get; set; }
        public bool BoolForDateTime2 { get; set; }
        public DateTime? Date3 { get; set; }
        public bool BoolForDateTime3 { get; set; }
        public bool Checkbox1 { get; set; }
        public bool BoolForCheckBox1 { get; set; }
        public bool Checkbox2 { get; set; }
        public bool BoolForCheckBox2 { get; set; }
        public bool Checkbox3 { get; set; }
        public bool BoolForCheckBox3 { get; set; }
        //public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<CollectionTopic> CollectonTopics { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        
        
        //public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Topic> Topics { get; set; }
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

            context.Collections.AddOrUpdate(new Collection { Name = "Чайка", Description = "Это кника напасана А. Чеховым", TopicName = "Книги" });
            context.Collections.AddOrUpdate(new Collection { Name = "Водка", Description = "Это алкогольный напиток, 40%", TopicName = "Алкоголь" });
            context.Collections.AddOrUpdate(new Collection { Name = "Adidas UCL 21 Pro SALA", Description = "Мяч для футбола на траве", TopicName = "Футбольные мячи" });
            context.SaveChanges();


            base.Seed(context);
        }

        

        private bool CheckIfInitialized(ApplicationDbContext context)
        {
            return context.Users.Any(u => u.Email.Equals("bigadmin@gmail.com"));
        }
    }
}
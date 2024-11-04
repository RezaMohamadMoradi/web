using _1.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;


namespace YourNamespace
{
    public class YourDbContext : IdentityDbContext<User>
    {

        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
        {

        }

        public DbSet<categury> Categuries { get; set; }
        public DbSet<productt> products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<cart> carts { get; set; }
        public DbSet<item> items { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            base.OnModelCreating(modelBuilder);


            // تعریف کلید اصلی برای IdentityUserLogin (در صورت نیاز)
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // تعریف داده‌های اولیه (Seeding) برای دسته‌بندی‌ها
            modelBuilder.Entity<categury>().HasData(
                new categury { id = 1, name = "elector", description = "bargh" },
                new categury { id = 2, name = "دوره php", description = "gfdv" }
            );

            // تعریف داده‌های اولیه (Seeding) برای محصولات
            modelBuilder.Entity<productt>().HasData(
                new productt { id = 1, Name = "دوره C#", itemid = 1, categuryid = 1, Description = "از مقدماتی تا حرفه ای", img = "~/Img/1.jpg" },
                new productt { id = 2, Name = "دوره php", itemid = 2, categuryid = 2, Description = "از مقدماتی تا حرفه ای", img = "~/Img/2.jpg" },
                new productt { id = 3, Name = "دوره java", itemid = 3, categuryid = 1, Description = "از مقدماتی تا حرفه ای", img = "~/Img/3.jpg" }
            );

            // تعریف داده‌های اولیه (Seeding) برای آیتم‌ها
            modelBuilder.Entity<item>().HasData(new item()
            { id = 1, price = 123, quantity = 5644 },

             new item() { id = 2, price = 123, quantity = 1 },
             new item() { id = 3, price = 123, quantity = 1 });


            // تنظیم Cascade Delete برای ارتباط بین cart و cartitem
            modelBuilder.Entity<cart>()
                .HasMany(c => c.Cartitems)  // cart دارای چندین cartitem است
                .WithOne(ci => ci.cart)      // هر cartitem به یک cart مرتبط است
                .HasForeignKey(ci => ci.cartid) // کلید خارجی cartId در cartitem
                .OnDelete(DeleteBehavior.Cascade); // تنظیم برای حذف cascade
        }
        public List<productt> read()
        {
            return products.ToList();
        }
        public List<categury> readcategory()
        {
            return Categuries.ToList();
        }
        public object adduser(userviewmodel u)
        {
            User user = new User();
            user.name = u.name;
            // استفاده از PasswordHasher برای هش کردن رمز عبور
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, u.password); // ذخیره هش رمز عبور
            Users.Add(user);
            SaveChanges();
            return u;



        }

        public User login(string name, string pas)
        {

            var user = Users
                        .SingleOrDefault(u => u.name == name);

            return user;
        }






    }
}
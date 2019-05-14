namespace Tedushop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Tedushop.Model.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Tedushop.Data.TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tedushop.Data.TeduShopDbContext context)
        {
            //CreateProductCategorySample(context);
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TeduShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "tedu",
                Email = "nclong92@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Technology Education"

            };

            manager.Create(user, "123654$");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("nclong92@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateProductCategorySample(TeduShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> productCategories = new List<ProductCategory>()
                {
                    new ProductCategory(){Name = "điện lạnh", Alias="dien-lanh", Status=true},
                    new ProductCategory(){Name = "viễn thông", Alias="vien-thong", Status=true},
                    new ProductCategory(){Name = "đồ gia dụng", Alias="do-gia-dung", Status=true},
                    new ProductCategory(){Name = "Mỹ phẩm", Alias="my-pham", Status=true}
                };

                context.ProductCategories.AddRange(productCategories);
                context.SaveChanges();
            }
        }
    }
}

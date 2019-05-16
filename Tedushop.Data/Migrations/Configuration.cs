namespace Tedushop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Tedushop.Common;
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
            //CreateApplicationUser(context);
            CreateFooter(context);


        }

        private void CreateApplicationUser(TeduShopDbContext context)
        {
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

        private void CreateFooter(TeduShopDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterID) == 0)
            {
                string content = @"<div class='footer-bottom-cate'>
                <h6>CATEGORIES</h6>
                <ul>
                    <li><a href='#'>Curabitur sapien</a></li>
                    <li><a href='#'>Dignissim purus</a></li>
                    <li><a href='#'>Tempus pretium</a></li>
                    <li><a href='#'>Dignissim neque</a></li>
                    <li><a href='#'>Ornared id aliquet</a></li>
                    <li><a href='#'>Ultrices id du</a></li>
                    <li><a href='#'>Commodo sit</a></li>
                    <li><a href='#'>Urna ac tortor sc</a></li>
                    <li><a href='#'>Ornared id aliquet</a></li>
                    <li><a href='#'>Urna ac tortor sc</a></li>
                    <li><a href='#'>Eget nisi laoreet</a></li>
                    <li><a href='#'>Faciisis ornare</a></li>
                </ul>
            </div>
            <div class='footer-bottom-cate bottom-grid-cat'>
                <h6>FEATURE PROJECTS</h6>
                <ul>
                    <li><a href='#'>Curabitur sapien</a></li>
                    <li><a href='#'>Dignissim purus</a></li>
                    <li><a href='#'>Tempus pretium</a></li>
                    <li><a href='#'>Dignissim neque</a></li>
                    <li><a href='#'>Ornared id aliquet</a></li>
                    <li><a href='#'>Ultrices id du</a></li>
                    <li><a href='#'>Commodo sit</a></li>
                </ul>
            </div>
            <div class='footer-bottom-cate'>
                <h6>TOP BRANDS</h6>
                <ul>
                    <li><a href='#'>Curabitur sapien</a></li>
                    <li><a href='#'>Dignissim purus</a></li>
                    <li><a href='#'>Tempus pretium</a></li>
                    <li><a href='#'>Dignissim neque</a></li>
                    <li><a href='#'>Ornared id aliquet</a></li>
                    <li><a href='#'>Ultrices id du</a></li>
                    <li><a href='#'>Commodo sit</a></li>
                    <li><a href='#'>Urna ac tortor sc</a></li>
                    <li><a href='#'>Ornared id aliquet</a></li>
                    <li><a href='#'>Urna ac tortor sc</a></li>
                    <li><a href='#'>Eget nisi laoreet</a></li>
                    <li><a href='#'>Faciisis ornare</a></li>
                </ul>
            </div>
            <div class='footer-bottom-cate cate-bottom'>
                <h6>OUR ADDERSS</h6>
                <ul>
                    <li>Aliquam metus  dui. </li>
                    <li>orci, ornareidquet</li>
                    <li> ut,DUI.</li>
                    <li>nisi, dignissim</li>
                    <li>gravida at.</li>
                    <li class='phone'>PH : 6985792466</li>
                    <li class='temp'> <p class='footer-class'>Design by <a href='http://w3layouts.com/' target='_blank'>W3layouts</a> </p></li>
                </ul>
            </div>
            <div class='clearfix'> </div>";

                var footer = new Footer()
                {
                    ID = CommonConstants.DefaultFooterID,
                    Content = content
                };

                context.Footers.Add(footer);
                context.SaveChanges();
            }
        }
    }
}

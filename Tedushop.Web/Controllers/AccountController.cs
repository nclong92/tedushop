using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tedushop.Web.App_Start;
using Tedushop.Web.Models;
using Tedushop.Model.Models;
using System.Threading.Tasks;
using BotDetect.Web.Mvc;
using Tedushop.Common;

namespace Tedushop.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("RegisterCaptchaCode", "RegisterCaptcha", "Mã xác nhận không đúng")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userByEmail = await _userManager.FindByEmailAsync(model.Email);
                if(userByEmail != null)
                {
                    ModelState.AddModelError("error", "Email đã tồn tại");
                    return View(model);
                }

                var userByUserName = await _userManager.FindByNameAsync(model.UserName);
                if(userByUserName != null)
                {
                    ModelState.AddModelError("username", "Tài khoản đã tồn tại");
                    return View(model);
                }

                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = model.FullName,
                    PhoneNumber = model.FullName,
                    Address = model.Address
                };

                await _userManager.CreateAsync(user, model.Password);

                var adminUser = await _userManager.FindByEmailAsync(model.Email);
                if(adminUser != null)
                {
                    await _userManager.AddToRolesAsync(adminUser.Id, new string[] { "User" });
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/templates/newuser.html"));
                content = content.Replace("{{UserName}}", adminUser.UserName);
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap");

                MailHelper.SendMail(adminUser.Email, "Đăng ký thành công.", content);

                ViewData["SuccessMsg"] = "Đăng ký thành công";
            }

            return View();
        }
    }
}
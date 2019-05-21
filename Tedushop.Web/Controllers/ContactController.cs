using AutoMapper;
using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tedushop.Common;
using Tedushop.Model.Models;
using Tedushop.Service;
using Tedushop.Web.Infrastructure.Extensions;
using Tedushop.Web.Models;

namespace Tedushop.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactDetailService _contactDetailService;
        private readonly IFeedBackSerive _feedBackSerive;

        public ContactController(IContactDetailService contactDetailService, IFeedBackSerive feedBackSerive)
        {
            this._contactDetailService = contactDetailService;
            this._feedBackSerive = feedBackSerive;
        }
        public ActionResult Index()
        {
            FeedBackViewModel model = new FeedBackViewModel();

            model.ContactDetailViewModel = GetContactDetail();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Mã xác nhận không đúng")]
        public ActionResult SendFeedBack(FeedBackViewModel feedBackViewModel)
        {
            if (ModelState.IsValid)
            {
                var newFeedBack = new FeedBack();
                newFeedBack.UpdateFeedBack(feedBackViewModel);
                _feedBackSerive.Create(newFeedBack);
                _feedBackSerive.Save();

                ViewData["SuccessMsg"] = "Gửi phản hồi thành công";

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/templates/contact_template.html"));
                content = content.Replace("{{Name}}", feedBackViewModel.Name);
                content = content.Replace("{{Email}}", feedBackViewModel.Email);
                content = content.Replace("{{Message}}", feedBackViewModel.Message);

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content);

                feedBackViewModel.Name = "";
                feedBackViewModel.Email = "";
                feedBackViewModel.Message = "";

            }
            else
            {
                MvcCaptcha.ResetCaptcha("contactCaptcha");
            }

            feedBackViewModel.ContactDetailViewModel = GetContactDetail();

            return View("Index", feedBackViewModel);
        }

        private ContactDetailViewModel GetContactDetail()
        {
            var contact = _contactDetailService.GetDefaultContact();

            var contactDetailViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(contact);

            return contactDetailViewModel;
        }
    }
}
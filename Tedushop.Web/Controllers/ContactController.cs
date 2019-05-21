using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult SendFeedBack(FeedBackViewModel feedBackViewModel)
        {
            if (ModelState.IsValid)
            {
                var newFeedBack = new FeedBack();
                newFeedBack.UpdateFeedBack(feedBackViewModel);
                _feedBackSerive.Create(newFeedBack);
                _feedBackSerive.Save();

                ViewData["SuccessMsg"] = "Gửi phản hồi thành công";
                feedBackViewModel.Name = "";
                feedBackViewModel.Email = "";
                feedBackViewModel.Message = "";

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
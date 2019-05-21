using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tedushop.Model.Models;
using Tedushop.Service;
using Tedushop.Web.Models;

namespace Tedushop.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactDetailService _contactDetailService;

        public ContactController(IContactDetailService contactDetailService)
        {
            this._contactDetailService = contactDetailService;
        }
        public ActionResult Index()
        {
            var contact = _contactDetailService.GetDefaultContact();

            var model = Mapper.Map<ContactDetail, ContactDetailViewModel>(contact);

            return View(model);
        }
    }
}
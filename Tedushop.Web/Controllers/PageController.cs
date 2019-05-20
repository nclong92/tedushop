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
    public class PageController : Controller
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }
        public ActionResult Index(string alias)
        {
            var page = _pageService.GetPageByAlias(alias);

            var model = Mapper.Map<Page, PageViewModel>(page);

            return View(model);
        }
    }
}
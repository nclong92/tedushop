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
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;
        ICommonService _commonService;
        

        public HomeController(IProductCategoryService productCategoryService, IProductService productService, ICommonService commonService)
        {
            this._productCategoryService = productCategoryService;
            this._productService = productService;
            this._commonService = commonService;
        }

        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlides();

            var slideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);

            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideViewModel;

            var lastestProductModel = _productService.GetLastest(3);
            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            homeViewModel.LastestProducts = lastestProductViewModel;

            var topSaleProductModel = _productService.GetHotProduct(3);
            var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            homeViewModel.TopSaleProducts = topSaleProductViewModel;

            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = _commonService.GetFooter();

            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(model);

            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}
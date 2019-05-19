using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Tedushop.Common;
using Tedushop.Model.Models;
using Tedushop.Service;
using Tedushop.Web.Infrastructure.Core;
using Tedushop.Web.Models;

namespace Tedushop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }

        public ActionResult Detail(int id)
        {
            var productModel = _productService.GetById(id);
            var viewModel = Mapper.Map<Product, ProductViewModel>(productModel);

            int relatedProductQuanlity = int.Parse(ConfigHelper.GetByKey("RelatedProductQuantity"));
            IEnumerable<Product> relatedProduct = _productService.GetRelatedProducts(id, relatedProductQuanlity);
            var relatedProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProduct);
            ViewBag.RelatedProducts = relatedProductViewModel;

            var moreImages = viewModel.MoreImages;
            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(moreImages);
            ViewBag.MoreImages = listImages;

            var tagModels = _productService.GetListTagByProductId(id);
            var tagViewModels = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(tagModels);
            ViewBag.Tags = tagViewModels;

            return View(viewModel);
        }

        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPages = (int) Math.Ceiling((double)totalRow / pageSize);

            var category = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPages
            };

            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPages = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Keyword = keyword;

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPages
            };

            return View(paginationSet);
        }

        public ActionResult ListByTag(string tagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByTag(tagId, page, pageSize, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPages = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Tag = Mapper.Map<Tag, TagViewModel>(_productService.GetTag(tagId));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPages
            };

            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            IEnumerable<string> model = _productService.GetListProductByName(keyword);

            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
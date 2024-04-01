using Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ViewModel.Product;
using ViewModel.ProductList;
using ViewModel.Purchase;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;
        public ProductController(IProductServices productServices, IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor,IMemoryCache cache)
        {
            _productServices = productServices;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }
        //public IActionResult Index(ProductViewModel productViewModel)
        //{
        //    return View(productViewModel);
        //}

        public IActionResult Welcome()
        {
            return View();
        }
        public IActionResult Create(ProductViewModel productViewModel)
        {
            return View(productViewModel);
        }
        public async Task<IActionResult> List()
        {

            var result = await _productServices.List();
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Cart(ProductViewModel model)

        {
            var result = await _productServices.BuyProduct();
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Display(ProductViewModel model)

        {
            var result = await _productServices.DisplayProductDetail();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertProductDetail(ProductViewModel productViewModel, IFormFile file)
        {
            string uniqueFile = null;
            try
            {
                if (file != null)
                {
                    string folder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/ProductImages/");
                    uniqueFile = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(folder, uniqueFile);
                    file.CopyTo(new FileStream(filePath, FileMode.Create));
                    productViewModel.Photo = uniqueFile;
                }
            }
            catch (Exception)
            {
            }
            var result = await _productServices.InsertProductDetail(productViewModel);
            return new JsonResult(result);
            //return new JsonResult(new { success = true });
        }
        //public async Task<IActionResult> UploadMultiStream(ProductViewModel productViewModel, IFormFile file)
        //{
        //    try
        //    {
        //        //accessing header properties values
        //        var filetype = HttpContext.Request.Headers["type"].FirstOrDefault();
        //        var description = HttpContext.Request.Headers["session"].FirstOrDefault();

        //        //accessing uploaded files
        //        var files = HttpContext.Request.Form.Files;
        //        foreach (var item in files)
        //        {
        //            var currentfile = Path.Combine("wwwroot", "ProductImages\\" + item.FileName);
        //            using (var stream = new FileStream(currentfile, FileMode.Create))
        //            {
        //                await item.CopyToAsync(stream);
        //            }
        //        }


        //        return Json(new { status = true, message = "message from server" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { status = false });
        //    }
        //    finally
        //    {
        //        //file.InputStream.Close();
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Upload([FromBody] ProductListViewModel productListViewModel)
        {
            var result = await _productServices.Upload(productListViewModel);
            return new JsonResult(result);
        }
        public async Task<IActionResult> DisplayProductDetail(ProductViewModel model)
        {
            var result = await _productServices.DisplayProductDetail();
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(ProductViewModel productViewModel, IFormFile file)
        {
            string uniqueFile = null;
            try
            {
                if (file != null)
                {
                    string folder = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot/ProductImages/");
                    uniqueFile = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(folder, uniqueFile);
                    file.CopyTo(new FileStream(filePath, FileMode.Create));
                    productViewModel.Photo = uniqueFile;
                }
            }
            catch (Exception)
            {
            }
            var data = await _productServices.UpdateProductDetail(productViewModel);
            return new JsonResult(data);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantityDetail(PurchaseViewModel purchaseViewModel)
        {
            var session = HttpContext.Session.GetInt32("Id");
            purchaseViewModel.UserId = (int)session;
            var result = await _productServices.UpdateQuantityDetail(purchaseViewModel);
            return new JsonResult(result);
        }
        [HttpGet]
        public async Task<JsonResult> GetById(int id)
        {
            if (_cache.TryGetValue(id, out ProductViewModel data))
            {
                _cache.Remove(id);
                return new JsonResult(data);
            }
            data = await _productServices.GetById(id);
            var cacheOptions = new MemoryCacheEntryOptions()
                 .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            _cache.Set(id, data, cacheOptions);
            return new JsonResult(data);
        }
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            
            var  data = await _productServices.DeleteProductDetail(id);
            
            return new JsonResult(data);
        }
        public async Task<IActionResult> Search(string SearchProduct)
        {
            var data = await _productServices.SearchProducts(SearchProduct);
            return PartialView("Cart", data);
        }
        public async Task<IActionResult> SearchDate(string fromDate, string toDate)
        {
            var data = await _productServices.SearchDate(fromDate, toDate);
            return PartialView("Display", data);
        }
    }
}

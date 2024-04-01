using Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using ViewModel.Login;

namespace OnlineShop.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILoginServices _loginServices;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;
        public RegisterController(ILoginServices loginServices, IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _loginServices = loginServices;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }
        public IActionResult Register(LoginViewModel loginViewModel)
        {
            return View(loginViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Display(LoginViewModel model)
        {
            
            var result = await _loginServices.DisplayUserDetail();
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertUserDetail(LoginViewModel loginViewModel)
        {
            if (loginViewModel != null)
            {
                var result = await _loginServices.InsertUserDetail(loginViewModel);
                if (result == true)
                {
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError("email", "Email Already Exist!!");
                    return View("Register");
                }
            }
            return View("Register");
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (loginViewModel.Email != null && loginViewModel.Password != null)
            {
                var result = await _loginServices.Index(loginViewModel);
                HttpContext.Session.SetInt32("Id", loginViewModel.Id);
                if (result == null)
                {
                    return RedirectToAction("Index", "Register");
                }
                else
                {
                    return RedirectToAction("Welcome", "Register");
                }
            }
            else
            {
                return View("Index");
            }
        }
    }
}


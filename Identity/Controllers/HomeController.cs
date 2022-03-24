using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            if(user != null)
            {
               var result = await signInManager.PasswordSignInAsync(user, password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Register");
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {

            var user = await userManager.FindByNameAsync(username);

            if (user != null) return View();

            var identityUser = new IdentityUser
            {
                UserName = username,
                Email = username + "@mail.com"
            };

            var signInResult = await userManager.CreateAsync(identityUser, password);

            if (signInResult.Succeeded)
            {
                var result = await signInManager.PasswordSignInAsync(identityUser, password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}


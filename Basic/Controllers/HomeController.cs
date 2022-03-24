using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var baseAuthCliams = new List<Claim>{

                new Claim(ClaimTypes.Name, "BaseAuthName"),
                new Claim(ClaimTypes.Email, "BaseAuthName@mail.com"),

            };

            var otherAuthCliams = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "OtherCliam"),
                new Claim(ClaimTypes.HomePhone, "0922347543")
            };

            var baseAuthCliamsIdentity = new ClaimsIdentity(baseAuthCliams, "BaseAuthCliamsIdentity");

            var otherAuthCliamsIdentity = new ClaimsIdentity(otherAuthCliams, "BtherAuthCliamsIdentity");

            var baseCliamPrinciple = new ClaimsPrincipal(new[] { baseAuthCliamsIdentity, otherAuthCliamsIdentity });

            HttpContext.SignInAsync(baseCliamPrinciple);

            return RedirectToAction("Index");
        }
    }
}


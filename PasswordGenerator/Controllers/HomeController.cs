using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PasswordGenerator.Models;

namespace PasswordGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(PasswordModel? passwordModel)
        {
            if (passwordModel.Length == 0)
            {
                passwordModel = new PasswordModel();
                passwordModel = SetDefaultSettings(passwordModel);
            }

            return View(passwordModel);
        }

        [HttpPost]
        public IActionResult PasswordGenerator(PasswordModel passwordModel)
        {
            var passwordGenerator = new Password();

            if (passwordModel.IsIncludeLowercase == true)
            {
                passwordGenerator.IncludeLowercase();
            }

            if (passwordModel.IsIncludeUppercase == true)
            {
                passwordGenerator.IncludeUppercase();
            }

            if (passwordModel.IsIncludeNumeric == true)
            {
                passwordGenerator.IncludeNumeric();
            }

            if (passwordModel.IsIncludeSpecial == true)
            {
                passwordGenerator.IncludeSpecial();
            }

            if (passwordModel.Length < 4)
            {
                passwordModel.Length = 4;
            }

            if (passwordModel.Count < 1)
            {
                passwordModel.Count = 1;
            }

            passwordGenerator.LengthRequired(passwordModel.Length);

            passwordModel.Passwords = new List<string>();
            passwordModel.Passwords.AddRange(passwordGenerator.NextGroup(passwordModel.Count));
            return View("Index", passwordModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public PasswordModel SetDefaultSettings(PasswordModel passwordModel)
        {
            passwordModel.IsIncludeUppercase = false;
            passwordModel.IsIncludeSpecial = false;
            passwordModel.IsIncludeNumeric = true;
            passwordModel.Length = 4;
            passwordModel.Count = 1;
            passwordModel.Passwords = new List<string>();
            return passwordModel;
        }
    }
}

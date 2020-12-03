using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreApp.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Library.Interfaces;

namespace StoreApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreRepo _storeRepo;
        private readonly ICustomerRepo _customerRepo;

        public HomeController(ILogger<HomeController> logger, IStoreRepo StoreRepo, ICustomerRepo CustomerRepo)
        {
            _logger = logger;
            _storeRepo = StoreRepo;
            _customerRepo = CustomerRepo;
        }

        public IActionResult Index()
        {
            var totalStores = _storeRepo.GetAllStores().Count();
            var totalCustomers = _customerRepo.GetAllCustomers().Count();

            ViewData["totalStores"] = totalStores;
            ViewData["totalCustomers"] = totalCustomers;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

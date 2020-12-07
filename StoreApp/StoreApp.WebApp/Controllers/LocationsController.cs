using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreApp.WebApp.Models;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Library.Interfaces;
using StoreApp.Library;

namespace StoreApp.WebApp.Controllers
{

    public class LocationsController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IStoreRepo _storeRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IProductRepo _productRepo;

        public LocationsController(ILogger<HomeController> logger, IStoreRepo StoreRepo, ICustomerRepo CustomerRepo, IOrderRepo OrderRepo, IProductRepo ProductRepo)
        {

            _logger = logger;
            _storeRepo = StoreRepo;
            _customerRepo = CustomerRepo;
            _orderRepo = OrderRepo;
            _productRepo = ProductRepo;
        }

        // GET: LocationsController
        public IActionResult Index()
        {
            var stores = _storeRepo.GetAllStores().Select(s => new LocationViewModel
            {
                Id = s.StoreId,
                Name = s.StoreName,
                Inventory = s.StoreInventory
            });

            return View(stores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IFormCollection Name)
        {
            var stores = _storeRepo.GetAllStores().Select(s => new LocationViewModel
            {
                Id = s.StoreId,
                Name = s.StoreName,
                Inventory = s.StoreInventory
            });

            IEnumerable<LocationViewModel> filteredStores = stores.Where(s => s.Name.Contains(Name["Name"]));

            if (filteredStores.Any())
            {
                return View(filteredStores);
            }

            return View(stores);
        }


        // GET: LocationsController/Details/5
        public IActionResult Details(int id)
        {
            var orders = _orderRepo.GetOrdersByStoreId(id);
            string storeName = _storeRepo.GetStoreById(id).StoreName;
            ViewData["Store"] = storeName;
            List<OrderViewModel> ViewOrders = new List<OrderViewModel>();

            if (orders.Any())
            {
                foreach (var order in orders)
                {
                    string customerName = _customerRepo.GetCustomerById(order.GetCustomerId).CustomerFullName;
                    DateTime time = order.GetTime;
                    decimal total = order.GetTotal;
                    Dictionary<string, int> items = new Dictionary<string, int>();
                    foreach (var kvp in order.GetOrderItems)
                    {
                        string productName = _productRepo.GetProductById(kvp.Key).ProductName;
                        items[productName] = kvp.Value;
                    }

                    ViewOrders.Add(new OrderViewModel
                    {
                        Id = order.OrderId,
                        Store = storeName,
                        Customer = customerName,
                        Time = time,
                        Total = total,
                        Products = items
                    });
                }
            }

            return View(ViewOrders);
        }
    }
}
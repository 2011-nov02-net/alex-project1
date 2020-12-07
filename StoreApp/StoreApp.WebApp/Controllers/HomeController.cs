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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreRepo _storeRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IProductRepo _productRepo;

        public HomeController(ILogger<HomeController> logger, IStoreRepo StoreRepo, ICustomerRepo CustomerRepo, IOrderRepo OrderRepo, IProductRepo ProductRepo)
        {

            _logger = logger;
            _storeRepo = StoreRepo;
            _customerRepo = CustomerRepo;
            _orderRepo = OrderRepo;
            _productRepo = ProductRepo;
        }

        //GET  /Home/Index
        public IActionResult Index()
        {
            var customers = _customerRepo.GetAllCustomers().Select(c => new CustomerViewModel
            {
                Id = c.CustomerId,
                FirstName = c.CustomerFirstName,
                LastName = c.CustomerLastName,
                FullName = c.CustomerFullName,
                PhoneNumber = c.PhoneNumber
            });

            return View(customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IFormCollection Name)
        {

            var customers = _customerRepo.GetAllCustomers().Select(c => new CustomerViewModel
            {
                Id = c.CustomerId,
                FirstName = c.CustomerFirstName,
                LastName = c.CustomerLastName,
                FullName = c.CustomerFullName,
                PhoneNumber = c.PhoneNumber
            });

            IEnumerable<CustomerViewModel> filteredCustomers = new List<CustomerViewModel>();
            if (String.IsNullOrEmpty(Name["First"]))
            {
                filteredCustomers = customers.Where(c => c.LastName.Contains(Name["Last"]));
            }else if (String.IsNullOrEmpty(Name["Last"]))
            {
                filteredCustomers = customers.Where(c => c.FirstName.Contains(Name["First"]));
            }
            else
            {
                filteredCustomers = customers.Where(c => c.FirstName.Contains(Name["First"]) && c.LastName.Contains(Name["Last"]));
            }
            
            if (filteredCustomers.Any())
            {
                return View(filteredCustomers);
            }
            

            
            return View(customers);
        }

        public IActionResult Details(int id)
        {
            var orders = _orderRepo.GetOrdersCustomerId(id);
            string customerName = _customerRepo.GetCustomerById(id).CustomerFullName;
            ViewData["Customer"] = customerName;
            List<OrderViewModel> ViewOrders = new List<OrderViewModel>();
            if (orders.Any())
            {
                foreach(var order in orders)
                {
                    string storeName = _storeRepo.GetStoreById(order.GetStoreId).StoreName;
                    DateTime time = order.GetTime;
                    decimal total = order.GetTotal;
                    Dictionary<string, int> items = new Dictionary<string, int>();
                    foreach(var kvp in order.GetOrderItems)
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                Customer customer = new Customer(viewModel.FirstName, viewModel.LastName, viewModel.PhoneNumber);
                bool success = _customerRepo.AddNewCustomer(customer);

                if (!success)
                {
                    ViewData["AddedCustomer"] = "none";
                    return View(viewModel);
                }

                ViewData["AddedCustomer"] = customer.CustomerFullName;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                
                ModelState.AddModelError("", "There was a problem registering new customer");
                // this error should be more specific if possible, e.g. if i can tell it's because
                // of a duplicate name or something
                return View(viewModel);
            }
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

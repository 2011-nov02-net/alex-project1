﻿using Microsoft.AspNetCore.Mvc;
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
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCartController
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IStoreRepo _storeRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IProductRepo _productRepo;
        private IShoppingCart _shoppingCart;
        public ShoppingCartController(ILogger<ShoppingCartController> logger, IStoreRepo StoreRepo, ICustomerRepo CustomerRepo, IOrderRepo OrderRepo, IProductRepo ProductRepo, IShoppingCart ShoppingCart)
        {

            _logger = logger;
            _storeRepo = StoreRepo;
            _customerRepo = CustomerRepo;
            _orderRepo = OrderRepo;
            _productRepo = ProductRepo;
            _shoppingCart = ShoppingCart;
        }
        public ActionResult Index()
        {
            var items = _shoppingCart.GetCart();
            List<ProductViewModel> viewModel = new List<ProductViewModel>();
            foreach(var item in items)
            {
                IProduct product = _productRepo.GetProductById(item.Key);

                viewModel.Add(new ProductViewModel
                {
                    Id = product.ProductId,
                    Name = product.ProductName,
                    Price = product.Price,
                    Amount = item.Value
                });

            }

            _logger.LogInformation("Getting cart information from ShoppingCart/Index");
            return View(viewModel);
        }

        public ActionResult EmptyCart()
        {
            _shoppingCart.EmptyCart();

            _logger.LogInformation("Cart cleared from ShoppingCart/EmptyCart");
            return RedirectToAction("Index");
        }

        public IActionResult CheckOut()
        {
            var store = _storeRepo.GetStoreById(_shoppingCart.StoreId);
            foreach (var kvp in _shoppingCart.GetCart())
            {
                if(kvp.Value > store.StoreInventory[kvp.Key])
                {
                    TempData["noStock"] = "true";
                    return RedirectToAction("Index");
                }
            }

            TempData["noStock"] = "false";
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(CustomerViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Invalid Model State while trying to check out returning checkout view");
                    return View(viewModel);
                }

                var customer = _customerRepo.GetCustomerByFirstAndLastName(viewModel.FirstName, viewModel.LastName);

                if(customer == null)
                {
                    ViewData["NoCustomer"] = true;
                    return View(viewModel);
                }

                decimal cartTotal = 0;
                foreach(var kvp in _shoppingCart.GetCart())
                {
                    decimal price = _productRepo.GetProductById(kvp.Key).Price;
                    cartTotal += price * kvp.Value;
                }
                Order order = new Order(_shoppingCart.StoreId, customer.CustomerId,DateTime.Now, cartTotal, _shoppingCart.GetCart());
                bool srSuccess = _storeRepo.PlaceOrder(_shoppingCart.StoreId, _shoppingCart.GetCart());
                bool orSuccess = _orderRepo.AddNewOrder(order);

                _shoppingCart.EmptyCart();

                _logger.LogInformation("Order placed successfully returning to cart");
                return RedirectToAction("Index");


            }
            catch (Exception)
            {

                ModelState.AddModelError("", "There was a problem Logging In");
                _logger.LogWarning("Error Logging in Customer");
                return View(viewModel);
            }

        }

        // GET: ShoppingCartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingCartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShoppingCartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

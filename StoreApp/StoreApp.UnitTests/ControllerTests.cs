using System;
using System.Collections.Generic;
using Xunit;
using StoreApp.Library;
using StoreApp.Library.Interfaces;
using StoreApp.WebApp;
using StoreApp.WebApp.Controllers;
using StoreApp.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Linq;

namespace StoreApp.UnitTests
{
    public class ControllerTests
    {
        [Fact]
        public void HomeIndex_WithCustomers_DisplaysCustomers()
        {
            var mockCustomerRepo = new Mock<ICustomerRepo>();
            var mockStoreRepo = new Mock<IStoreRepo>();
            var mockProductRepo = new Mock<IProductRepo>();
            var mockOrderRepo = new Mock<IOrderRepo>();

            mockCustomerRepo.Setup(r => r.GetAllCustomers())
                .Returns(new List<Customer> {new Customer(1,"Andrew", "Cortez", "(567)988-7867"), new Customer(2,"Jon", "Snow","(456)009-6789")});

            var controller = new HomeController(new NullLogger<HomeController>(), mockStoreRepo.Object, mockCustomerRepo.Object, mockOrderRepo.Object, mockProductRepo.Object);

            IActionResult actionResult = controller.Index();

            var viewResult = Assert.IsAssignableFrom<ViewResult>(actionResult);
            var customers = Assert.IsAssignableFrom<IEnumerable<CustomerViewModel>>(viewResult.Model);
            var customerList = customers.ToList();

            Assert.Equal(2, customerList.Count());
            Assert.Equal("Andrew", customerList[0].FirstName);
            Assert.Equal("Cortez", customerList[0].LastName);
            Assert.Null(viewResult.ViewName);

        }

        [Fact]
        public void LocationIndex_WithLocations_DisplaysLocations()
        {
            var mockCustomerRepo = new Mock<ICustomerRepo>();
            var mockStoreRepo = new Mock<IStoreRepo>();
            var mockProductRepo = new Mock<IProductRepo>();
            var mockOrderRepo = new Mock<IOrderRepo>();
            var mockShoppingCartRepo = new Mock<IShoppingCart>();

            Dictionary<int, int> products = new Dictionary<int, int>()
            {
                {1,10 },
                {2,20 },
            };

            mockStoreRepo.Setup(r => r.GetAllStores())
                .Returns(new List<Store> {new Store(1, "Store", products)});

            var controller = new LocationsController(new NullLogger<LocationsController>(), mockStoreRepo.Object, mockCustomerRepo.Object, mockOrderRepo.Object, mockProductRepo.Object, mockShoppingCartRepo.Object);

            IActionResult actionResult = controller.Index();

            var viewResult = Assert.IsAssignableFrom<ViewResult>(actionResult);
            var locations = Assert.IsAssignableFrom<IEnumerable<LocationViewModel>>(viewResult.Model);
            var locationsList = locations.ToList();

            Assert.Equal(1, locationsList.Count());
            Assert.Equal("Store", locationsList[0].Name);
            Assert.Equal(products, locationsList[0].Inventory);
            Assert.Null(viewResult.ViewName);
        }

        [Fact]
        public void ShoppingCartIndex_WithProducts_DisplaysProducts()
        {
            var mockCustomerRepo = new Mock<ICustomerRepo>();
            var mockStoreRepo = new Mock<IStoreRepo>();
            var mockProductRepo = new Mock<IProductRepo>();
            var mockOrderRepo = new Mock<IOrderRepo>();
            var mockShoppingCartRepo = new Mock<IShoppingCart>();

            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>()
            {
                {1,10 },
            };

            mockShoppingCartRepo.Setup(r => r.GetCart())
                .Returns(keyValuePairs);

            mockProductRepo.Setup(r => r.GetProductById(It.IsAny<int>()))
                .Returns(new Product(1, "Product", 10.00M));

            var controller = new ShoppingCartController(new NullLogger<ShoppingCartController>(), mockStoreRepo.Object, mockCustomerRepo.Object, mockOrderRepo.Object, mockProductRepo.Object, mockShoppingCartRepo.Object);

            IActionResult actionResult = controller.Index();

            var viewResult = Assert.IsAssignableFrom<ViewResult>(actionResult);
            var products = Assert.IsAssignableFrom<IEnumerable<ProductViewModel>>(viewResult.Model);
            var productsList = products.ToList();

            Assert.Equal(1, productsList.Count());
            Assert.Equal("Product", productsList[0].Name);
            Assert.Equal(10.00M, productsList[0].Price);
            Assert.Null(viewResult.ViewName);
        }
    }
}

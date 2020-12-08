using System;
using System.Collections.Generic;
using Xunit;
using StoreApp.Library;
using StoreApp.Library.Interfaces;

namespace StoreApp.UnitTests
{
    public class LibraryTests
    {

        [Fact]
        public void AddToCartTest()
        {
            IShoppingCart shoppingCart = new ShoppingCart();

            shoppingCart.AddToCart(1, 1, 5);

            Assert.True(shoppingCart.GetCart().Count == 1);
            Assert.True(shoppingCart.GetCart().ContainsKey(1));
            Assert.Equal(5, shoppingCart.GetCart()[1]);
            Assert.Equal(1, shoppingCart.StoreId);
        }
        [Fact]
        public void EmptyCartTest()
        {
            IShoppingCart shoppingCart = new ShoppingCart();

            shoppingCart.AddToCart(1, 1, 5);

            shoppingCart.EmptyCart();

            Assert.True(shoppingCart.GetCart().Count == 0);
            Assert.Equal(0, shoppingCart.StoreId);
        }

        [Fact]
        public void StoreAddToInventoryTest()
        {
            Dictionary<int, int> products = new Dictionary<int, int>()
            {
                {1,10 },
                {2,20 },
                {3,30 }
            };

            Store store = new Store(1, "Store", products);

            store.AddToInventory(4, 40);

            Assert.True(store.StoreInventory.Count == 4);
            Assert.True(store.StoreInventory.ContainsKey(4));
        }

        [Fact]
        public void CustomerTest()
        {
            Customer testCustomer = new Customer("Andrew", "Cortez", "(323)760-3152");

            Assert.Equal("Andrew", testCustomer.CustomerFirstName);
            Assert.Equal("Cortez", testCustomer.CustomerLastName);
            Assert.Equal("(323)760-3152", testCustomer.PhoneNumber);
        }

        [Fact]
        public void StoreTest()
        {
            Dictionary<int, int> products = new Dictionary<int, int>()
            {
                {1,10 },
                {2,20 },
            };

            Store store = new Store(1, "Store", products);

            Assert.Equal(1, store.StoreId);
            Assert.Equal("Store", store.StoreName);
            Assert.True(store.StoreInventory.ContainsKey(1) && store.StoreInventory.ContainsKey(2));
            Assert.Equal(10, store.StoreInventory[1]);
            Assert.Equal(20, store.StoreInventory[2]);
        }

        [Fact]
        public void OrderTest()
        {
            Dictionary<int, int> products = new Dictionary<int, int>()
            {
                {1,10 },
                {2,20 },
            };

            Order order = new Order(1, 1, DateTime.Now, 10.00M, products);

            Assert.Equal(1, order.GetStoreId);
            Assert.Equal(1, order.GetCustomerId);
            Assert.Equal(10.00M, order.GetTotal);
            Assert.True(order.GetOrderItems.Count == 2);
            Assert.True(order.GetOrderItems.ContainsKey(1));
            Assert.True(order.GetOrderItems.ContainsKey(2));
            Assert.Equal(10, order.GetOrderItems[1]);
            Assert.Equal(20, order.GetOrderItems[2]);

        }

        [Fact]
        public void ProductTest()
        {
            Product product = new Product(1, "Product", 10.00M);

            product.UpdatePrice(20.00M);

            Assert.Equal(1, product.ProductId);
            Assert.Equal("Product", product.ProductName);
            Assert.Equal(20.00M, product.Price);
        }
    }
}

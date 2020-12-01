using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    [Serializable]
    public class Product : IProduct
    {
        private readonly int _productId = 0;
        private readonly string _productName = "";
        private decimal _price = 0.0M;


        public Product(int id, string name, decimal price)
        {
            _productId = id;
            _productName = name;
            _price = price;
        }

        public int ProductId => _productId;

        public string ProductName => _productName;


        public decimal Price => _price;

        public void UpdatePrice(int newPrice)
        {
            _price = newPrice;
        }
    }
}

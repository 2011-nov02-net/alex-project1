using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Library.Interfaces;

namespace StoreApp.Library
{
    public class ShoppingCart : IShoppingCart
    {
        private Dictionary<int, int> products = new Dictionary<int, int>();
        private int _storeID = 0;
        private int _customerID = 0;


        public int CustomerID => _customerID;
        public int StoreId => _storeID;
        public bool AddToCart(int storeId, int productId, int amount)
        {
            if(_storeID == 0)
            {
                _storeID = storeId;
            }

            if(_storeID != 0 && storeId != _storeID)
            {
                return false;
            }

            if (products.ContainsKey(productId))
            {
                products[productId] += amount;
            }
            else
            {
                products.Add(productId, amount);
            }
            return true;
        }

        public Dictionary<int, int> GetCart()
        {
            return products;
        }

        public void EmptyCart()
        {
            _storeID = 0;
            products.Clear();
        }
    }
}

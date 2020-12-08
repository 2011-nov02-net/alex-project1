using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Library.Interfaces;

namespace StoreApp.Library
{
    
    public class Store
    {
        private readonly int _storeId = 0;
        private readonly string _storeName = "";
        private Dictionary<int, int> _storeInventory = new Dictionary<int, int>();

        public Store(int id, string name, Dictionary<int,int> items)
        {
            _storeId = id;
            _storeName = name;
            _storeInventory = items;
        }

        public int StoreId => _storeId;

        public string StoreName => _storeName;

        public Dictionary<int, int> StoreInventory => _storeInventory;

        public bool AddToInventory(int productId, int amount)
        {
            if (!_storeInventory.ContainsKey(productId))
            {
                if(amount >= 0)
                {
                    _storeInventory.Add(productId, amount);
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}

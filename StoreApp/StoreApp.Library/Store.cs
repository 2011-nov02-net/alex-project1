using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    [Serializable]
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

        public bool AddToInventory(IProduct product, int amount)
        {
            if (!_storeInventory.ContainsKey(product.ProductId))
            {
                if(amount >= 0)
                {
                    _storeInventory.Add(product.ProductId, amount);
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Interfaces
{
    public interface IStoreRepo
    {
        public List<Store> GetAllStores();

        public Store GetStoreByName(string name);

        public Store GetStoreById(int id);

        public bool PlaceOrder(int storeId, Dictionary<int, int> items);
    }
}

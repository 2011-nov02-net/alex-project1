using System;
using System.Collections.Generic;
using StoreApp.DataModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace StoreApp.Library
{
    public class StoreRepository
    {

        /// <summary>
        /// Database context options to create database context
        /// </summary>
        private readonly DbContextOptions<StoreAppContext> _contextOptions;
        
        /// <summary>
        /// Constructor for store repository
        /// </summary>
        /// <param name="contextOptions">Context options to initialize database context</param>
        public StoreRepository(DbContextOptions<StoreAppContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        /// <summary>
        /// Retrives all stores available in databases
        /// </summary>
        /// <returns>List of library store objects available in database or empty list if none</returns>
        public List<Store> GetAllStores()
        {
            using var context = new StoreAppContext(_contextOptions);
            List<Store> matchingStores = new List<Store>();

            List<StoreTable> dbStores;
            try
            {
                dbStores = context.StoreTables
                    .Include(s => s.Inventories)
                    .ToList();
            }
            catch(InvalidOperationException)
            {
                return matchingStores;
            }

            foreach (var store in dbStores)
            {
                Dictionary<int, int> inventory = new Dictionary<int, int>();

                foreach(var product in store.Inventories)
                {
                    inventory.Add(product.ProductId, product.Stock);
                }

                matchingStores.Add(new Store(store.Id, store.Name, inventory));
             }

            return matchingStores;
        }

        /// <summary>
        /// Retrieves store by name from database
        /// </summary>
        /// <param name="name">Store name</param>
        /// <returns>Library store object if exists in database or null if non existent</returns>
        public Store GetStoreByName(string name)
        {
            using var context = new StoreAppContext(_contextOptions);

            StoreTable dbStore;
            try
            {
               dbStore = context.StoreTables
                    .Include(s => s.Inventories)
                    .First(s => s.Name == name);
            }
            catch(InvalidOperationException)
            {
                return null;
            }

            Dictionary<int, int> inventory = new Dictionary<int, int>();

            foreach (var product in dbStore.Inventories)
            {
                inventory.Add(product.ProductId, product.Stock);
            }

            return new Store(dbStore.Id, dbStore.Name, inventory);
        }


        /// <summary>
        /// Retrieves store by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Library store object if exists in database or null if non existent</returns>
        public Store GetStoreById(int id)
        {
            using var context = new StoreAppContext(_contextOptions);

            StoreTable dbStore;
            try
            {
                dbStore = context.StoreTables
                     .Include(s => s.Inventories)
                     .First(s => s.Id == id);
            }
            catch (InvalidOperationException)
            {
                return null;
            }

            Dictionary<int, int> inventory = new Dictionary<int, int>();

            foreach (var product in dbStore.Inventories)
            {
                inventory.Add(product.ProductId, product.Stock);
            }

            return new Store(dbStore.Id, dbStore.Name, inventory);
        }
        
        /// <summary>
        /// updates store inventory in database
        /// </summary>
        /// <param name="storeId"> Target store id</param>
        /// <param name="items">Dictionary containing all order items with product id as key and quantity as value</param>
        /// <returns></returns>
        public bool PlaceOrder(int storeId, Dictionary<int, int> items)
        {
            Store store = GetStoreById(storeId);
            if(store != null)
            {
                using var context = new StoreAppContext(_contextOptions);

                
                foreach(var item in items)
                {
                    var dbStore = context.Inventories.First(i => i.LocationId == storeId && i.ProductId == item.Key);
                    dbStore.Stock = dbStore.Stock - item.Value;
                    context.SaveChanges();
                }
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }

    

}

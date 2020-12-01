using System;
using System.Collections.Generic;
using StoreApp.DataModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StoreApp.Library
{
    public class ProductRepository
    {
        /// <summary>
        /// Database context options to create database context
        /// </summary>
        private readonly DbContextOptions<StoreAppContext> _contextOptions;

        /// <summary>
        /// Constructor for product repository
        /// </summary>
        /// <param name="contextOptions">Context options to initialize database context</param>
        public ProductRepository(DbContextOptions<StoreAppContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }
        
        /// <summary>
        /// Retrieve product from database by its name
        /// </summary>
        /// <param name="name">Name of product to be returned </param>
        /// <returns>Library product object if product exists in database null otherwise</returns>
        public IProduct GetProductByName(string name)
        {
            using var context = new StoreAppContext(_contextOptions);
            ProductTable dbProduct;
            try
            {
                dbProduct = context.ProductTables.First(p => p.Name == name);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            return new Product(dbProduct.Id, dbProduct.Name, dbProduct.Price);
        }

        /// <summary>
        /// Retrieve product by its id
        /// </summary>
        /// <param name="id">Id of product to be returned</param>
        /// <returns>Library product object if product exists in database null otherwise</returns>
        public IProduct GetProductById(int id)
        {
            using var context = new StoreAppContext(_contextOptions);
            ProductTable dbProduct;
            try
            {
                dbProduct = context.ProductTables.First(p => p.Id == id);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            return new Product(dbProduct.Id, dbProduct.Name, dbProduct.Price);
        }

    }
}

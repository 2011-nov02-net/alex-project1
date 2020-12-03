using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Library.Interfaces;

namespace StoreApp.Library.Interfaces
{
    public interface IProductRepo
    {
        public IProduct GetProductByName(string name);

        public IProduct GetProductById(int id);
    }
}

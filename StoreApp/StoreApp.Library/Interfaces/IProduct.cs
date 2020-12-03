using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Interfaces
{
    public interface IProduct
    {
        int ProductId { get; }

        string ProductName { get; }

        decimal Price { get; }

        void UpdatePrice(int newPrice);
    }
}

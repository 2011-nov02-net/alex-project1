using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Interfaces
{
    public interface IShoppingCart
    {
        public int CustomerID { get; }
        public int StoreId { get; }
        public bool AddToCart(int storeId, int productId, int amount);

        public Dictionary<int, int> GetCart();

        public void EmptyCart();
    }
}

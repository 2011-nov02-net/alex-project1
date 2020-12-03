using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Interfaces
{
    public interface IOrderRepo
    {
        public Order GetOrderById(int id);

        public List<Order> GetOrdersCustomerId(int id);

        public List<Order> GetOrdersByStoreId(int id);

        public bool AddNewOrder(Order order);
    }
}

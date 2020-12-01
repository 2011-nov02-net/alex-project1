using System;
using System.Collections.Generic;

namespace StoreApp.Library
{
    [Serializable]
    public class Order
    {
        private readonly int _orderId = 0;
        private readonly int _storeId;
        private readonly int _customerId;
        private readonly DateTime _time;
        private readonly decimal _total = 0;

        private readonly Dictionary<int, int> _orderItems;

        public Order(int id, int store, int customer, DateTime time, decimal total, Dictionary<int,int> products)
        {
            _orderId = id;
            _storeId = store;
            _customerId = customer;
            _time = time;
            _total = total;
            _orderItems = products;
        }

        public Order(int store, int customer, DateTime time, decimal total, Dictionary<int, int> products)
        {
            
            _storeId = store;
            _customerId = customer;
            _time = time;
            _total = total;
            _orderItems = products;
        }

        public int OrderId => _orderId;

        public int GetStoreId => _storeId;

        public int GetCustomerId => _customerId;

        public DateTime GetTime => _time;

        public Dictionary<int, int> GetOrderItems => _orderItems;

        public decimal GetTotal => _total;



    }
}

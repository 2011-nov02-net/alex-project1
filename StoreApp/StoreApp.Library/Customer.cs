using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    [Serializable]
    public class Customer
    {
        private readonly int _customerId = 0;
        private readonly string _customerFirstName = "";
        private readonly string _customerLastName = "";
        private string _phoneNumber = "";

        public Customer(int id, string first, string last, string phone)
        {
            _customerId = id;
            _customerFirstName = first;
            _customerLastName = last;
            _phoneNumber = phone;

        }

        public Customer(string first, string last, string phone)
        {
            _customerFirstName = first;
            _customerLastName = last;
            _phoneNumber = phone;

        }

        public int CustomerId => _customerId;

        public string CustomerFullName => _customerFirstName +" " +_customerLastName;
        public string CustomerFirstName => _customerFirstName;
        public string CustomerLastName => _customerLastName;

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                if(value != null)
                {
                    _phoneNumber = value;
                }
            }
        }

    }
}

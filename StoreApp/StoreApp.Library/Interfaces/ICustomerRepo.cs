using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Interfaces
{
     public interface ICustomerRepo
    {
        public List<Customer> GetAllCustomers();

        public Customer GetCustomerById(int id);

        public Customer GetCustomerByFirstAndLastName(string first, string last);

        public bool AddNewCustomer(Customer customer);

    }
}

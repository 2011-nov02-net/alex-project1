using System;
using System.Collections.Generic;
using StoreApp.DataModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace StoreApp.Library
{
    public class CustomerRepository
    {
        /// <summary>
        /// Database context options to create database context
        /// </summary>
        private readonly DbContextOptions<StoreAppContext> _contextOptions;



        //public List<Customer> _allCustomers = new List<Customer>();

        /// <summary>
        /// Constructor for customer repository
        /// </summary>
        /// <param name="contextOptions">Context options to initialize database context</param>
        public CustomerRepository(DbContextOptions<StoreAppContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        /// <summary>
        /// Retrieves all customers from database
        /// </summary>
        /// <returns>List of library customer objects</returns>
        public List<Customer> GetAllCustomers()
        {
            using var context = new StoreAppContext(_contextOptions);

            var dbCustomers = context.CustomerTables.ToList();

            var customers = dbCustomers.Select(c => new Customer(c.Id, c.FirstName, c.LastName, c.Phone)).ToList();

            return customers;
        }
        /// <summary>
        /// Retrives customer by id from database
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Library customer object</returns>
        public Customer GetCustomerById(int id)
        {
            using var context = new StoreAppContext(_contextOptions);

            CustomerTable dbCustomer;
            try
            {
                dbCustomer = context.CustomerTables.First(c => c.Id == id);
            }
            catch(InvalidOperationException)
            {
                return null;
            }
            
            return new Customer(dbCustomer.Id, dbCustomer.FirstName, dbCustomer.LastName, dbCustomer.Phone);
     
        }
        /// <summary>
        /// Retrieves customer my first and last name from database
        /// </summary>
        /// <param name="first">Customer First Name</param>
        /// <param name="last">Customer Last Name</param>
        /// <returns>Library customer object if found null otherwise</returns>
        public Customer GetCustomerByFirstAndLastName(string first, string last)
        {
            using var context = new StoreAppContext(_contextOptions);
            CustomerTable dbCustomer;
            try
            {
                dbCustomer = context.CustomerTables.First(c => c.FirstName == first && c.LastName == last);
            }
            catch(InvalidOperationException)
            {
                return null;
            }
            return new Customer(dbCustomer.Id, dbCustomer.FirstName, dbCustomer.LastName, dbCustomer.Phone);
            
        }
        /// <summary>
        /// Adds new customer to database
        /// </summary>
        /// <param name="customer">Customer object of customer to add to database</param>
        /// <returns>True if customer added succesfully and false if customer already exists in database</returns>
        public bool AddNewCustomer(Customer customer)
        {

            using var context = new StoreAppContext(_contextOptions);

            var dbCustomer = new CustomerTable()
            {
                FirstName = customer.CustomerFirstName,
                LastName = customer.CustomerLastName,
                Phone = customer.PhoneNumber
            };

            if (GetCustomerByFirstAndLastName(customer.CustomerFirstName, customer.CustomerLastName) == null)
            {
                context.CustomerTables.Add(dbCustomer);
                context.SaveChanges();
                
                return true;
            }
            return false;
        }
    }
}

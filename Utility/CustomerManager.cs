using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* namespace MechanicAPP_OOP2.Utility
{
    
    public class CustomerService
    {
        private List<Customer> customers = new List<Customer>();

        public void loadCustomersFromFile()
        {
        string CUSTOMER_TXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\wchri\\source\\repos\\MechanicShopManagementSystem\\Utility\\customers.txt");
        string[] lines = File.ReadAllLines(CUSTOMER_TXT);
        foreach (string line in File.ReadLines(CUSTOMER_TXT))
        {
            string[] customerData = line.Split(',');
            string id = customerData[0];
            string firstName = customerData[1];
            string lastName = customerData[2];
            string contact = customerData[3];
            string email = customerData[4];
            new Customer(id, firstName, lastName, contact, email);
            customers.Add(new Customer(id, firstName, lastName, contact, email));
            
        }
        }
        public void saveCustomersToFile()
        {
            string CUSTOMER_TXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\wchri\\source\\repos\\MechanicShopManagementSystem\\Utility\\customers.txt");
            using (StreamWriter writer = new StreamWriter(CUSTOMER_TXT))
            {
                foreach (Customer customer in customers)
                {
                    writer.WriteLine($"{customer.Id},{customer.FirstName},{customer.LastName},{customer.ContactNumber},{customer.Email}");
                }
            }
        }
        public List<Customer> GetCustomers()
        {
            return customers;
        }
        public void AddNewCustomer(Customer customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer.Id) || string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName) || string.IsNullOrEmpty(customer.ContactNumber) || string.IsNullOrEmpty(customer.Email))
                {
                    throw new Exception("All fields are required");
                }

                Customer newCustomer = new Customer(customer.Id, customer.FirstName, customer.LastName, customer.ContactNumber, customer.Email);
                customers.Add(newCustomer);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ModifyCustomer(Customer customer)
        {
            var existingCustomer = customers.FirstOrDefault(c => c.Id == customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.ContactNumber = customer.ContactNumber;
                existingCustomer.Email = customer.Email;
            }
            else
            {
                throw new Exception("Customer not found");
            }
        }
        public void DeleteCustomer(Customer customer)
        {
            var customerToDelete = customers.FirstOrDefault(c => c.Id == customer.Id);
            if (customerToDelete != null)
            {
                customers.Remove(customerToDelete);
            }
            else
            {
                throw new Exception("Customer not found");
            }
        }

        public List<Customer> GetAllCustomers()
        {
            return customers;
        }

       
    }
} */

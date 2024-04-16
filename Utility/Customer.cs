using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicAPP_OOP2.Utility
{
    
    public class Customer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public Customer(string id, string firstName, string lastName, string contactNumber, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            ContactNumber = contactNumber;
            Email = email;
        }
        public override string ToString()
        {
            return $"Customer ID: {Id}, First Name: {FirstName}, Last Name: {LastName}, Contact Number: {ContactNumber}, Email: {Email}";
        }
    }


}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MechanicAPP_OOP2.Model
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }


        public Customer Clone() => MemberwiseClone() as Customer;


        public (bool IsValid, string? ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                return (false, $"{nameof(FirstName)} is required.");
            }

            else if (string.IsNullOrWhiteSpace(LastName))
            {
                return (false, $"{nameof(LastName)} is required.");
            }

            else if (string.IsNullOrWhiteSpace(ContactNumber))
            {
                return (false, $"{nameof(ContactNumber)} is required.");
            }

            else if (string.IsNullOrWhiteSpace(Email))
            {
                return (false, $"{nameof(Email)} is required.");
            }

            return (true, null);
        }
    }
}

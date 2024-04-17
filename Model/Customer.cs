using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MechanicAPP_OOP2.Model
{
    [Table("customer")]
    public class Customer
    {
        [PrimaryKey, AutoIncrement]

        [Column("id")]
        public int Id { get; set; }

        [Column("customer_name")]
        public string FirstName { get; set; }

        [Column("customer_lastname")]
        public string LastName { get; set; }

        [Column("mobilephone")]
        public string ContactNumber { get; set; }

        [Column("email")]
        public string Email { get; set; }


        public Customer Clone() => MemberwiseClone() as Customer;


        public (bool IsValid, string? ErrorMessage) Validate()
        {
            if (Id == 0)
            {
                return (false, $"{nameof(Id)} is required.");
            }
            else if (string.IsNullOrWhiteSpace(FirstName))
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

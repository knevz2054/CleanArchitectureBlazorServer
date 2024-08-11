using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Common.Models
{
    public class AccountHolder : Person
    {
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public List<Account> Accounts { get; set; }

        //for update
        public AccountHolder Update(string firstName, string lastName, DateTime dateOfBirth, string contactNumber, string email)
        {
            if (firstName is not null && FirstName?.Equals(firstName, StringComparison.CurrentCultureIgnoreCase) is not true) FirstName = firstName;
            if (lastName is not null && LastName?.Equals(lastName, StringComparison.CurrentCultureIgnoreCase) is not true) LastName = lastName;
            DateOfBirth = dateOfBirth;
            if (contactNumber is not null && ContactNumber?.Equals(contactNumber, StringComparison.CurrentCultureIgnoreCase) is not true) ContactNumber = contactNumber;
            if (email is not null && Email?.Equals(email, StringComparison.CurrentCultureIgnoreCase) is not true) Email = email;
            return this;
        }
    }
}

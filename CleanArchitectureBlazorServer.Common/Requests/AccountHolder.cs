using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Common.Requests
{
    public class CreateAccountHolder
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }
}

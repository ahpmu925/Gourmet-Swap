using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSwap.Models.Domain
{
   public class ContactMessage
    {
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
       
        public string Email { get; set; }
       
        public string PhoneNo { get; set; }
       
        public string City { get; set; }
       
        public string Address { get; set; }
       
        public string Message { get; set; }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int Status { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

    }
}

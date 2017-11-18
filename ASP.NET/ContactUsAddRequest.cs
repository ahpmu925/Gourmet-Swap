using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSwap.Models.Requests
{
    public class ContactUsAddRequest
    {
        [Required]
        [MinLength(2), MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$")]
        public string PhoneNo { get; set;  }
        [Required]
        [MinLength(2), MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Address { get; set; }
        [Required]
        [MinLength(5), MaxLength(128)]
        public string Message { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime DateModified { get; set; }

    }
}

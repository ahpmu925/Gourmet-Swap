using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSwap.Models.Requests
{
   public class ContactUsStatusRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Status { get; set; }
    }
}

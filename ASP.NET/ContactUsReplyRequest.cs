using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSwap.Models.Requests
{
    public class ContactUsReplyRequest
    {
        
        [Required]
        public string To { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Subject { get; set; }

    }
}
   

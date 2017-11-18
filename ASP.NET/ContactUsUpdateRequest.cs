using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSwap.Models.Requests
{
    public class ContactUsUpdateRequest: ContactUsAddRequest
    {
        [Required]
        public int Id { get; set; }

    }
}

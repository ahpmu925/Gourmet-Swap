using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSwap.Models.Requests.Blog
{
  public  class BlogUpdateRequest: BlogAddRequest
    {
        [Required]
        public int Id { get; set; }
    }
}

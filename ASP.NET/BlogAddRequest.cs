using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSwap.Models.Requests.Blog
{
    public class BlogAddRequest
    {

        [Required]
        [MinLength(2), MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MinLength(2), MaxLength(128)]
        public string Body { get; set; }
        [Required]
        public bool IsPublished { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public List<int> Ids { get; set; }

    }
}

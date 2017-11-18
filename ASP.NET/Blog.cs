using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSwap.Models.Domain
{
    public class Blog
    {

        public string Title { get; set; }

        public string Body { get; set; }

        public bool IsPublished { get; set; }

        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

        public List<int> Ids { get; set; }

      
    }
}

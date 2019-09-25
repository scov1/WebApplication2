using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class BookView
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public string Title { get; set; }
        public int? Pages { get; set; }
        public int? Price { get; set; }
        public int GenreId { get; set; }
    }
}
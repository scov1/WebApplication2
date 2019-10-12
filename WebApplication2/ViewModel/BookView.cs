using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class BookView
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }

        //[RegularExpression(@"[a-zA-Z]@")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки не менее двух символов")]
        public string Title { get; set; }

        public int? Pages { get; set; }
        public int? Price { get; set; }
        public int GenreId { get; set; }
    }
}
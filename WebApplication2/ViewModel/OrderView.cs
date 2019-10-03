using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class OrderView
    {
        public int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int BookId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Period { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
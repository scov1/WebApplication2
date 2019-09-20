using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Entities
{
    public class Orders
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Books")]
        public int BookId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }

        public string AuthorName { get; set; }

        public string BookName { get; set; }

        public Users Users { get; set; }
        public Books Books { get; set; }
    }
}
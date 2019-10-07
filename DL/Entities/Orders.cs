using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DL.Entities
{
    public class Orders
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Books")]
        public int BookId { get; set; }

        public Books Books { get; set; }
        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public Users Users { get; set; }

        public string AuthorName { get; set; }

        public string BookName { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Period { get; set; }


    }
}
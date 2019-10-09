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

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? ReturnDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Period { get; set; }


    }
}
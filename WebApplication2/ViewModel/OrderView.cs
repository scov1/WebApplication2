using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class OrderView
    {
        public int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int BookId { get; set; }

        [DataType(DataType.DateTime)]

        public DateTime CreationDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Period { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnDate { get; set; }
    }
}
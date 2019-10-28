using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class GenreView
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно к заполнению")]
        public string Name { get; set; }
    }
}
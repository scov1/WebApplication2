using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class AuthorView
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно поле")]
        public string FirstName { get; set; }

        [RegularExpression(@"[A-Za-z]@")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Фамилия должна быть более двух букв")]
        public string LastName { get; set; }
    }
}
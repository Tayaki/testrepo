using RazorPages.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Models
{
    public class Cat
    {
        [Hidden]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Sex")]
        [SelectorFromEnum(typeof(Enums.Sex))]
        public string Sex { get; set; }

        [Readonly]
        [Display(Name = "Age")]
        public int Age => DateTime.Now.Year - this.Birth.Year;

        [Required]
        [Display(Name = "Birth date")]
        public DateTime Birth { get; set; }
    }
}

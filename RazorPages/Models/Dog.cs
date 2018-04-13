using RazorPages.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPages.Models
{
    public class Dog
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

        [Display(Name = "Height")]
        [Placeholder("0")]
        [Minimum(6)]
        [Maximum(120)]
        [Step(0.5)]
        public int? Height { get; set; }

        [Display(Name = "Weight")]
        [Placeholder("0")]
        [Minimum(2)]
        [Maximum(80)]
        [Step(0.2)]
        public int? Weight { get; set; }

        [Display(Name = "Cat")]
        [SelectorFromDatabase(typeof(Cat), new string[] { "Name" })]
        public int? CatId { get; set; }

        public virtual Cat Cat { get; set; }
    }
}

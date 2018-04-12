using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPages.Models;
using RazorPages.Generic;

namespace RazorPages.Pages.Dogs
{
    public class CreateModel : GenericCreatePageModel<Dog, DogContext>
    {
        public CreateModel(DogContext context) : base(context)
        {
        }
    }
}
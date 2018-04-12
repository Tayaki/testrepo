using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPages.Models;
using RazorPages.Generic;

namespace RazorPages.Pages.Dogs
{
    public class EditModel : GenericEditPageModel<Dog, DogContext>
    {
        public EditModel(DogContext context) : base(context)
        {
        }
    }
}

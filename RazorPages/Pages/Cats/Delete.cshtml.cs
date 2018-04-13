﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.Models;
using RazorPages.Generic;

namespace RazorPages.Pages.Cats
{
    public class DeleteModel : GenericDeletePageModel<Cat, DogContext>
    {
        public DeleteModel(DogContext context) : base(context)
        {
        }
    }
}

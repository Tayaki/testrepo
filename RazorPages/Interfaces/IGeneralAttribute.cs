using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RazorPages.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Interfaces
{
    interface IGeneralAttribute
    {
        void GenerateInput<TContext>(InputModel model, TContext context = null) where TContext : DbContext;
    }
}

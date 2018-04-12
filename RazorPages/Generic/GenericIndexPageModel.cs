using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using RazorPages.Helpers;
using RazorPages.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.Generic
{
    public class GenericIndexPageModel<T, TContext> : PageModel where T : class where TContext : DbContext
    {
        private readonly TContext _context;

        public GenericIndexPageModel(TContext context)
        {
            _context = context;
        }

        public IList<T> ItemList { get; set; }

        public async Task OnGetAsync()
        {
            ItemList = await _context.Set<T>().ToListAsync();
        }

        #region HelperFunctions
        public bool PropertyShowable( PropertyInfo property)
        {
            return (!property.PropertyAttributeExists<HiddenAttribute>() &&
                    !property.PropertyAttributeExists<ReadonlyAttribute>() &&
                    !property.PropertyAttributeExists<CreateOnlyAttribute>() &&
                    !property.PropertyAttributeExists<DetailsOnlyAttribute>() &&
                    !property.PropertyAttributeExists<EditOnlyAttribute>());
        }

        public string GetPropertyName( PropertyInfo property)
        {
            return (property.GetPropertyAttribute<DisplayAttribute>() as DisplayAttribute)?.Name ?? "";
        }
        #endregion
    }
}

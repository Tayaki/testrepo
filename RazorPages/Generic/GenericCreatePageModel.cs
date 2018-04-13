using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using RazorPages.Helpers;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.Generic
{
    public class GenericCreatePageModel<T, TContext> : GenericPageModel where T : class where TContext : DbContext
    {
        private readonly TContext _context;

        public GenericCreatePageModel(TContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public T Item { get; set; }

        public Type Type { get { return typeof(T); } }

        public virtual bool Validate()
        {
            return ModelState.IsValid;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!Validate())
            {
                return Page();
            }

            _context.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        
        public bool PropertyShowable(PropertyInfo property)
        {
            return (!property.PropertyAttributeExists<ReadonlyAttribute>() &&
                    !property.PropertyAttributeExists<DetailsOnlyAttribute>() &&
                    !property.PropertyAttributeExists<EditOnlyAttribute>() &&
                    !property.GetGetMethod().IsVirtual);
        }
    }
}

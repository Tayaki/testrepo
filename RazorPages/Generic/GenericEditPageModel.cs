using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using RazorPages.Helpers;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.Generic
{
    public class GenericEditPageModel<T, TContext> : GenericPageModel where T : class where TContext : DbContext
    {
        private readonly TContext _context;

        public GenericEditPageModel(TContext context)
        {
            _context = context;
        }

        [BindProperty]
        public T Item { get; set; }

        public Type Type { get { return typeof(T); } }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Set<T>().FindAsync(id);

            if (Item == null)
            {
                return NotFound();
            }
            return Page();
        }

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

            _context.Attach(Item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

        public bool PropertyShowable(PropertyInfo property)
        {
            return (!property.PropertyAttributeExists<DetailsOnlyAttribute>() &&
                    !property.PropertyAttributeExists<CreateOnlyAttribute>() &&
                    !property.GetGetMethod().IsVirtual);
        }
    }
}

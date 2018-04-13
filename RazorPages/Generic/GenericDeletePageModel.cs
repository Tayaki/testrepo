using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using RazorPages.Helpers;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.Generic
{
    public class GenericDeletePageModel<T, TContext> : GenericPageModel where T : class where TContext : DbContext
    {
        private readonly TContext _context;

        public GenericDeletePageModel(TContext context)
        {
            _context = context;
        }

        [BindProperty]
        public T Item { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Set<T>().FindAsync(id);

            if (Item != null)
            {
                _context.Remove(Item);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public bool PropertyShowable(PropertyInfo property)
        {
            return (!property.PropertyAttributeExists<HiddenAttribute>() &&
                    !property.PropertyAttributeExists<CreateOnlyAttribute>() &&
                    !property.PropertyAttributeExists<EditOnlyAttribute>() &&
                    !property.GetGetMethod().IsVirtual);
        }

        public string GetPropertyValue(PropertyInfo property)
        {
            return property.GetPropertyDatabaseValue<SelectorFromDatabaseAttribute, TContext>(Item, _context);
        }
    }
}

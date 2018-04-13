using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using RazorPages.Helpers;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.Generic
{
    public class GenericDetailsPageModel<T, TContext> : GenericPageModel where T : class where TContext : DbContext
    {
        private readonly TContext _context;

        public GenericDetailsPageModel(TContext context)
        {
            _context = context;
        }

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

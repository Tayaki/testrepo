using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using RazorPages.Helpers;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.Generic
{
    public class GenericIndexPageModel<T, TContext> : GenericPageModel where T : class where TContext : DbContext
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
        public bool PropertyShowable(PropertyInfo property)
        {
            return (!property.PropertyAttributeExists<HiddenAttribute>() &&
                    !property.PropertyAttributeExists<ReadonlyAttribute>() &&
                    !property.PropertyAttributeExists<CreateOnlyAttribute>() &&
                    !property.PropertyAttributeExists<DetailsOnlyAttribute>() &&
                    !property.PropertyAttributeExists<EditOnlyAttribute>() &&
                    !property.GetGetMethod().IsVirtual);
        }

        public string GetPropertyValue(PropertyInfo property, T item)
        {
            return property.GetPropertyDatabaseValue<SelectorFromDatabaseAttribute, TContext>(item, _context);
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using RazorPages.Helpers;
using RazorPages.Interfaces;
using RazorPages.Layout;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.Generic
{
    public class GenericDeletePageModel<T, TContext> : PageModel where T : class where TContext : DbContext
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

        #region HelperFunctions
        public bool PropertyShowable(PropertyInfo property)
        {
            return (!property.PropertyAttributeExists<HiddenAttribute>() &&
                    !property.PropertyAttributeExists<CreateOnlyAttribute>() &&
                    !property.PropertyAttributeExists<EditOnlyAttribute>() &&
                    !property.GetGetMethod().IsVirtual);
        }

        public bool CustomLayoutExists(PropertyInfo property)
        {
            return property.PropertyAttributeExists<CustomLayout>();
        }

        public virtual HtmlString GetCustomLayout(PropertyInfo property)
        {
            object customLayoutAttribute = property.GetPropertyAttribute<CustomLayout>();
            Type customLayout = ((CustomLayout)customLayoutAttribute).Layout;
            IGeneralLayout item = (IGeneralLayout)Activator.CreateInstance(customLayout);
            return item.GenerateDelete();
        }

        public string GetPropertyCss(PropertyInfo property)
        {
            var cssAttribute = property.GetPropertyAttribute<CssClassAttribute>();
            if (cssAttribute != null)
            {
                string[] cssArray = ((CssClassAttribute)cssAttribute).Classes;
                return String.Join(" ", cssArray);
            }
            return "";
        }

        public string GetPropertyName(PropertyInfo property)
        {
            return property.GetPropertyName();
        }

        public string GetPropertyValue(PropertyInfo property)
        {
            return property.GetPropertyDatabaseValue<SelectorFromDatabaseAttribute, TContext>(Item, _context);
        }
        #endregion
    }
}

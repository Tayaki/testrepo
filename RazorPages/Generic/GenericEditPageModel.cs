using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using RazorPages.Helpers;
using RazorPages.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.Generic
{
    public class GenericEditPageModel<T, TContext> : PageModel where T : class where TContext : DbContext
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

        #region HelperFunctions
        public bool PropertyShowable(PropertyInfo property)
        {
            return (!property.PropertyAttributeExists<DetailsOnlyAttribute>() &&
                    !property.PropertyAttributeExists<CreateOnlyAttribute>());
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
            return item.GenerateEdit();
        }

        public string GetPropertyCss(PropertyInfo property)
        {
            object cssAttribute = property.GetPropertyAttribute<CssClassAttribute>();
            if (cssAttribute != null)
            {
                string[] cssArray = ((CssClassAttribute)cssAttribute).Classes;
                return String.Join(" ", cssArray);
            }
            return "";
        }

        public string GetModelFieldError(PropertyInfo property)
        {
            ModelStateEntry propertyState;
            if (ModelState.TryGetValue(property.Name, out propertyState))
            {
                return String.Join(" ", propertyState.Errors.Select(e => e.ErrorMessage).ToArray());
            }
            return "";
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using RazorPages.Helpers;
using RazorPages.Interfaces;
using RazorPages.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.Generic
{
    public class GenericCreatePageModel<T, TContext> : PageModel where T : class where TContext : DbContext
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

        #region HelperFunctions
        public bool PropertyShowable(PropertyInfo property)
        {
            return (!property.PropertyAttributeExists<ReadonlyAttribute>() &&
                    !property.PropertyAttributeExists<DetailsOnlyAttribute>() &&
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
            return item.GenerateCreate();
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

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Attributes;
using RazorPages.Helpers;
using RazorPages.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace RazorPages.Generic
{
    public class GenericPageModel : PageModel
    {
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

        public string GetPropertyName(PropertyInfo property)
        {
            return property.GetPropertyName();
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
    }
}

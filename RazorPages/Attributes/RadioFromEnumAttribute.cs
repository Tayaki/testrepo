using RazorPages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RazorPages.HelperModels;
using System.Text;
using System.Reflection;
using RazorPages.Helpers;

namespace RazorPages.Attributes
{
    public class RadioFromEnumAttribute : Attribute, IGeneralAttribute
    {
        public Type Domain { get; set; }

        public RadioFromEnumAttribute(Type _Domain)
        {
            Domain = _Domain;
        }

        public void GenerateInput<TContext>(InputModel model, TContext context = null) where TContext : DbContext
        {
            model.Output.TagName = "div";
            model.Output.TagMode = TagMode.StartTagAndEndTag;
            StringBuilder sb = new StringBuilder();
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(model.BaseType, model.PropertyName);
            object pool = requestedProperty.GetPropertyAttribute<RadioFromEnumAttribute>();
            Type poolType = ((RadioFromEnumAttribute)pool).Domain;
            foreach (var enumValue in poolType.GetEnumValues())
            {
                string _checked = "";
                if (Convert.ToString(enumValue) == model.Value)
                {
                    _checked = "checked";
                }
                sb.Append($"<input type='radio' name='{model.PropertyName}' value='{enumValue}' {_checked} /> {enumValue}");
            }
            model.Output.Content.AppendHtml(sb.ToString());
        }
    }
}

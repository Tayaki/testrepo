using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RazorPages.HelperModels;
using RazorPages.Helpers;
using RazorPages.Interfaces;
using System;
using System.Reflection;
using System.Text;

namespace RazorPages.Attributes
{
    public class SelectorFromEnumAttribute : Attribute, IGeneralAttribute
    {
        public Type Domain { get; set; }

        public SelectorFromEnumAttribute(Type _Domain)
        {
            Domain = _Domain;
        }

        public void GenerateInput<TContext>(InputModel model, TContext context = null) where TContext : DbContext
        {
            model.Output.TagName = "select";
            model.Output.TagMode = TagMode.StartTagAndEndTag;
            model.Output.Attributes.SetAttribute("name", model.PropertyName);
            model.Output.Content.SetContent(model.PropertyName);
            StringBuilder sb = new StringBuilder();
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(model.BaseType, model.PropertyName);
            object pool = requestedProperty.GetPropertyAttribute<SelectorFromEnumAttribute>();
            Type poolType = ((SelectorFromEnumAttribute)pool).Domain;
            if (model.Value == null || requestedProperty.IsNullable())
            {
                string placeholder = model.PlaceholderHelper("Choose from...");
                StringBuilder optionAttributes = new StringBuilder();
                if (!requestedProperty.IsNullable())
                {
                    optionAttributes.Append("disabled hidden ");
                }
                if (model.Value == null)
                {
                    optionAttributes.Append("selected ");
                }
                sb.Append($"<option value='' {optionAttributes}>{placeholder}</option>");
            }
            foreach (var enumValue in poolType.GetEnumValues())
            {
                string selected = "";
                if (Convert.ToString(enumValue) == model.Value)
                {
                    selected = "selected";
                }
                sb.Append($"<option value='{enumValue}' {selected}>{enumValue}</option>");
            }
            model.Output.Content.AppendHtml(sb.ToString());
        }
    }
}

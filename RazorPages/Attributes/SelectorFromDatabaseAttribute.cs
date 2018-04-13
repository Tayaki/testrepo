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
    public class SelectorFromDatabaseAttribute : Attribute, IGeneralAttribute
    {
        public Type Domain { get; set; }
        public string[] DisplayName { get; set; }

        public SelectorFromDatabaseAttribute(Type _Domain, string[] _DisplayName)
        {
            Domain = _Domain;
            DisplayName = _DisplayName;
        }

        public void GenerateInput<TContext>(InputModel model, TContext context) where TContext : DbContext
        {
            model.Output.TagName = "select";
            model.Output.TagMode = TagMode.StartTagAndEndTag;
            model.Output.Attributes.SetAttribute("name", model.PropertyName);
            model.Output.Content.SetContent(model.PropertyName);
            StringBuilder sb = new StringBuilder();
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(model.BaseType, model.PropertyName);
            object pool = requestedProperty.GetPropertyAttribute<SelectorFromDatabaseAttribute>();
            Type poolType = ((SelectorFromDatabaseAttribute)pool).Domain;
            string[] poolDisplay = ((SelectorFromDatabaseAttribute)pool).DisplayName;
            var list = context.Set(poolType);
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
            foreach (var item in list)
            {
                string selected = "";
                if (Convert.ToString(item.GetValue("Id")) == model.Value)
                {
                    selected = "selected";
                }
                StringBuilder optionValue = new StringBuilder();
                foreach (var displayProperty in poolDisplay)
                {
                    optionValue.Append(item.GetValue(displayProperty) + " ");
                }
                sb.Append($"<option value='{item.GetValue("Id")}' {selected}>{optionValue}</option>");
            }
            model.Output.Content.AppendHtml(sb.ToString());
        }
    }
}

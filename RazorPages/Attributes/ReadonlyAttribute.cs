using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RazorPages.HelperModels;
using RazorPages.Helpers;
using RazorPages.Interfaces;
using System;

namespace RazorPages.Attributes
{
    public class ReadonlyAttribute : Attribute, IGeneralAttribute
    {
        public void GenerateInput<TContext>(InputModel model, TContext context = null) where TContext : DbContext
        {
            model.Output.TagName = "span";
            model.Output.TagMode = TagMode.StartTagAndEndTag;
            model.Output.Attributes.SetAttribute("name", model.PropertyName);
            string placeholder = model.PlaceholderHelper(model.PropertyName);
            model.Output.Attributes.SetAttribute("placeholder", placeholder);
            model.Output.Content.SetHtmlContent(model.Value);
        }
    }
}

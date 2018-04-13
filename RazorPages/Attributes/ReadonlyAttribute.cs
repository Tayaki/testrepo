using RazorPages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorPages.HelperModels;
using RazorPages.Helpers;
using Microsoft.EntityFrameworkCore;

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

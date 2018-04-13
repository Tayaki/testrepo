using RazorPages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RazorPages.HelperModels;
using RazorPages.Helpers;

namespace RazorPages.Attributes
{
    public class TextAreaAttribute : Attribute, IGeneralAttribute
    {
        public void GenerateInput<TContext>(InputModel model, TContext context = null) where TContext : DbContext
        {
            model.Output.TagName = "textarea";
            model.Output.TagMode = TagMode.StartTagAndEndTag;
            model.Output.Attributes.SetAttribute("name", model.PropertyName);
            string placeholder = model.PlaceholderHelper(model.PropertyName);
            model.Output.Attributes.SetAttribute("placeholder", placeholder);
            model.Output.PreContent.SetHtmlContent(model.Value);
        }
    }
}

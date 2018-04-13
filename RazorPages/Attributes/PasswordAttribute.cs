using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RazorPages.HelperModels;
using RazorPages.Helpers;
using RazorPages.Interfaces;
using System;

namespace RazorPages.Attributes
{
    public class PasswordAttribute : Attribute, IGeneralAttribute
    {
        public void GenerateInput<TContext>(InputModel model, TContext context = null) where TContext : DbContext
        {
            model.Output.TagName = "input";
            model.Output.TagMode = TagMode.SelfClosing;
            model.Output.Attributes.SetAttribute("name", model.PropertyName);
            string placeholder = model.PlaceholderHelper(model.PropertyName);
            model.Output.Attributes.SetAttribute("placeholder", placeholder);
            model.Output.Attributes.SetAttribute("type", "password");
        }
    }
}

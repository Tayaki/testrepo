using RazorPages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorPages.HelperModels;
using Microsoft.EntityFrameworkCore;

namespace RazorPages.Attributes
{
    public class ColorAttribute : Attribute, IGeneralAttribute
    {
        public void GenerateInput<TContext>(InputModel model, TContext context = null) where TContext : DbContext
        {
            model.Output.TagName = "input";
            model.Output.TagMode = TagMode.SelfClosing;
            model.Output.Attributes.SetAttribute("name", model.PropertyName);
            model.Output.Attributes.SetAttribute("type", "color");
            model.Output.Attributes.SetAttribute("value", model.Value);
        }
    }
}

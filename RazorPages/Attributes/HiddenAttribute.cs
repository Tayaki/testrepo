﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RazorPages.HelperModels;
using RazorPages.Interfaces;
using System;

namespace RazorPages.Attributes
{
    public class HiddenAttribute : Attribute, IGeneralAttribute
    {
        public void GenerateInput<TContext>(InputModel model, TContext context = null) where TContext : DbContext
        {
            model.Output.TagName = "hidden";
            model.Output.TagMode = TagMode.SelfClosing;
            model.Output.Attributes.SetAttribute("name", model.PropertyName);
            model.Output.Attributes.SetAttribute("value", model.Value);
        }
    }
}

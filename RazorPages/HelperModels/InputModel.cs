using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.HelperModels
{
    public class InputModel
    {
        public TagHelperOutput Output { get; set; }
        public string BaseType { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }
}

using System;

namespace RazorPages.Attributes
{
    public class CssClassAttribute : Attribute
    {
        public string[] Classes { get; set; }

        public CssClassAttribute(string[] _Classes)
        {
            Classes = _Classes;
        }
    }
}

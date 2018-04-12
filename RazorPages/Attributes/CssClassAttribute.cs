using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

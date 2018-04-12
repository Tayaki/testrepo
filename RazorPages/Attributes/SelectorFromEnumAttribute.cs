using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Attributes
{
    public class SelectorFromEnumAttribute : Attribute
    {
        public Type Domain { get; set; }

        public SelectorFromEnumAttribute(Type _Domain)
        {
            Domain = _Domain;
        }
    }
}

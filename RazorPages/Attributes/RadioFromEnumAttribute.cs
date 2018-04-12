using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Attributes
{
    public class RadioFromEnumAttribute : Attribute
    {
        public Type Domain { get; set; }

        public RadioFromEnumAttribute(Type _Domain)
        {
            Domain = _Domain;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Attributes
{
    public class MaximumLengthAttribute : Attribute
    {
        public double Maximum { get; set; }

        public MaximumLengthAttribute(double _Maximum)
        {
            Maximum = _Maximum;
        }
    }
}

using System;

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

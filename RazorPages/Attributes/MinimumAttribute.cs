using System;

namespace RazorPages.Attributes
{
    public class MinimumAttribute : Attribute
    {
        public double Minimum { get; set; }

        public MinimumAttribute(double _Minimum)
        {
            Minimum = _Minimum;
        }
    }
}

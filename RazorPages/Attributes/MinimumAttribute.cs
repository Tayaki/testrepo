using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

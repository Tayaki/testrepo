using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Attributes
{
    public class StepAttribute : Attribute
    {
        public double Step { get; set; }

        public StepAttribute(double _Step)
        {
            Step = _Step;
        }
    }
}

﻿using System;

namespace RazorPages.Attributes
{
    public class MaximumAttribute : Attribute
    {
        public double Maximum { get; set; }

        public MaximumAttribute(double _Maximum)
        {
            Maximum = _Maximum;
        }
    }
}

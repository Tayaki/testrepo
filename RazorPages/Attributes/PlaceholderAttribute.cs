using System;

namespace RazorPages.Attributes
{
    public class PlaceholderAttribute : Attribute
    {
        public string Holder { get; set; }

        public PlaceholderAttribute(string _Holder)
        {
            Holder = _Holder;
        }
    }
}

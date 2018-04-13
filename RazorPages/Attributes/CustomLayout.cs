using System;

namespace RazorPages.Attributes
{
    public class CustomLayout : Attribute
    {
        public Type Layout { get; set; }

        public CustomLayout(Type _Layout)
        {
            Layout = _Layout;
        }
    }
}

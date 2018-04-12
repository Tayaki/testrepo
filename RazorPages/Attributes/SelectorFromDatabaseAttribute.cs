using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Attributes
{
    public class SelectorFromDatabaseAttribute : Attribute
    {
        public Type Domain { get; set; }
        public string[] DisplayName { get; set; }

        public SelectorFromDatabaseAttribute(Type _Domain, string[] _DisplayName)
        {
            Domain = _Domain;
            DisplayName = _DisplayName;
        }
    }
}

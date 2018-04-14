using RazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.TagHelpers
{
    public class RazorInputTagHelper : GenericInputTagHelper<DogContext>
    {
        public RazorInputTagHelper(DogContext context): base(context) { }
    }
}

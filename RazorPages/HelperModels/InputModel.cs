using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorPages.HelperModels
{
    public class InputModel
    {
        public TagHelperOutput Output { get; set; }
        public string BaseType { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
    }
}

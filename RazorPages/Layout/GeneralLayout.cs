using Microsoft.AspNetCore.Html;
using RazorPages.Interfaces;

namespace RazorPages.Layout
{
    public abstract class GeneralLayout : IGeneralLayout
    {
        public abstract HtmlString GenerateCreate(object[] options = null);
        public abstract HtmlString GenerateDetails(object[] options = null);
        public abstract HtmlString GenerateEdit(object[] options = null);
        public abstract HtmlString GenerateDelete(object[] options = null);
    }
}

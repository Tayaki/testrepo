using Microsoft.AspNetCore.Html;
using RazorPages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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

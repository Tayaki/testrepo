using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Interfaces
{
    public interface IGeneralLayout
    {
        HtmlString GenerateCreate(object[] options = null);
        HtmlString GenerateDetails(object[] options = null);
        HtmlString GenerateEdit(object[] options = null);
        HtmlString GenerateDelete(object[] options = null);
    }
}

﻿using Microsoft.AspNetCore.Html;

namespace RazorPages.Layout
{
    public class NormalLayout : GeneralLayout
    {
        public override HtmlString GenerateCreate(object[] options = null)
        {
            return new HtmlString("<div class='form-group'>N/A</div>");
        }

        public override HtmlString GenerateDetails(object[] options = null)
        {
            return new HtmlString("<dt>N/A</dt><dd>N/A</dd>");
        }

        public override HtmlString GenerateEdit(object[] options = null)
        {
            return new HtmlString("<div class='form-group'>N/A</div>");
        }

        public override HtmlString GenerateDelete(object[] options = null)
        {
            return new HtmlString("<dt>N/A</dt><dd>N/A</dd>");
        }
    }
}

﻿using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorPages.Attributes;
using RazorPages.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.TagHelpers
{
    public class GenericLabelTagHelper : TagHelper
    {
        #region Properties
        private string BaseType { get { return Convert.ToString(Expression.Model.GetValue("DeclaringType.FullName")); } }
        private string PropertyName { get { return Convert.ToString(Expression.Model.GetValue("Name")); } }
        public ModelExpression Expression { get; set; }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object hidden = requestedProperty.GetPropertyAttribute<HiddenAttribute>();
            if (hidden == null && !requestedProperty.GetGetMethod().IsVirtual)
            {
                output.TagName = "label";
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Attributes.SetAttribute("for", PropertyName);
                output.Content.SetHtmlContent(requestedProperty.GetPropertyName());
            }
        }
    }
}

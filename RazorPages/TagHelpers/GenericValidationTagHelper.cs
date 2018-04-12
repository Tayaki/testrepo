﻿using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorPages.Attributes;
using RazorPages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorPages.TagHelpers
{
    public class GenericValidationTagHelper : TagHelper
    {
        #region Properties
        private string BaseType { get { return Convert.ToString(Expression.Model.GetType().GetProperty("DeclaringType").GetValue(Expression.Model).GetType().GetProperty("FullName").GetValue(Expression.Model.GetType().GetProperty("DeclaringType").GetValue(Expression.Model))); } }
        private string PropertyName { get { return Convert.ToString(Expression.Model.GetType().GetProperty("Name").GetValue(Expression.Model)); } }
        public string Value { get; set; }
        public ModelExpression Expression { get; set; }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object hidden = ReflectionHelpers.GetPropertyAttribute<HiddenAttribute>(requestedProperty);
            if (hidden == null)
            {
                output.TagName = "span";
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Attributes.SetAttribute("class", "text-danger field-validation-valid");
                output.Attributes.SetAttribute("data-valmsg-for", PropertyName);
                output.Attributes.SetAttribute("data-valmsg-replace", true);
                output.PreContent.SetHtmlContent(Value);
            }
        }
    }
}
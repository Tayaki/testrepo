using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using RazorPages.Enums;
using RazorPages.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RazorPages.TagHelpers
{
    public class GenericInputTagHelper : TagHelper
    {
        #region Properties
        private string BaseType { get { return Convert.ToString(Expression.Model.GetType().GetProperty("DeclaringType").GetValue(Expression.Model).GetType().GetProperty("FullName").GetValue(Expression.Model.GetType().GetProperty("DeclaringType").GetValue(Expression.Model))); } }
        private string PropertyName { get { return Convert.ToString(Expression.Model.GetType().GetProperty("Name").GetValue(Expression.Model)); } }
        public string Value { get; set; }
        public ModelExpression Expression { get; set; }
        #endregion

        #region InputTypes
        private void SelectorEnumInput(TagHelperOutput output)//, [FromServices] DbContext context)
        {
            output.TagName = "select";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("name", PropertyName);
            output.Content.SetContent(PropertyName);
            StringBuilder sb = new StringBuilder();
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object pool = ReflectionHelpers.GetPropertyAttribute<SelectorFromEnumAttribute>(requestedProperty);
            Type poolType = ((SelectorFromEnumAttribute)pool).Domain;
            if (Value == null)
            {
                string placeholder = PlaceholderHelper("Choose from...");
                sb.Append($"<option value='' diabled selected hidden>{placeholder}</option>");
            }
            foreach (var enumValue in poolType.GetEnumValues())
            {
                string selected = "";
                if (Convert.ToString(enumValue) == Value)
                {
                    selected = "selected";
                }
                sb.Append($"<option value='{enumValue}' {selected}>{enumValue}</option>");
            }
            output.Content.AppendHtml(sb.ToString());
        }

        //private void SelectorDatabase(TagHelperOutput output)
        //{
        //    output.TagName = "select";
        //    output.TagMode = TagMode.StartTagAndEndTag;
        //    output.Attributes.SetAttribute("name", PropertyName);
        //    output.Content.SetContent(PropertyName);
        //    StringBuilder sb = new StringBuilder();
        //    PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
        //    object pool = ReflectionHelpers.GetPropertyAttribute<SelectorFromDatabaseAttribute>(requestedProperty);
        //    Type poolType = ((Attributes.SelectorFromDatabaseAttribute)pool).Domain;
        //    string[] poolDisplay = ((Attributes.SelectorFromDatabaseAttribute)pool).DisplayName;
        //    if (Value == null)
        //    {
        //        string placeholder = "Choose from...";
        //        object placeholderAttribute = ReflectionHelpers.GetPropertyAttribute<PlaceholderAttribute>(requestedProperty);
        //        if (placeholderAttribute != null)
        //        {
        //            placeholder = ((PlaceholderAttribute)placeholderAttribute).Holder;
        //        }
        //        sb.Append($"<option value='' diabled selected hidden>{placeholder}</option>");
        //    }
        //    foreach (var enumValue in poolType.GetEnumValues())
        //    {
        //        sb.Append($"<option value='{enumValue}'>{enumValue}</option>");
        //    }
        //    output.Content.AppendHtml(sb.ToString());
        //}

        private void RadioEnumInput(TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            StringBuilder sb = new StringBuilder();
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object pool = ReflectionHelpers.GetPropertyAttribute<RadioFromEnumAttribute>(requestedProperty);
            Type poolType = ((RadioFromEnumAttribute)pool).Domain;
            foreach (var enumValue in poolType.GetEnumValues())
            {
                string _checked = "";
                if (Convert.ToString(enumValue) == Value)
                {
                    _checked = "checked";
                }
                sb.Append($"<input type='radio' name='{PropertyName}' value='{enumValue}' {_checked} /> {enumValue}");
            }
            output.Content.AppendHtml(sb.ToString());
        }

        private void CheckEnumInput(TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            StringBuilder sb = new StringBuilder();
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object pool = ReflectionHelpers.GetPropertyAttribute<RadioFromEnumAttribute>(requestedProperty);
            Type poolType = ((RadioFromEnumAttribute)pool).Domain;
            foreach (var enumValue in poolType.GetEnumValues())
            {
                string _checked = "";
                if (Convert.ToString(enumValue) == Value)
                {
                    _checked = "checked";
                }
                sb.Append($"<input type='radio' name='{PropertyName}' value='{enumValue}' {_checked} /> {enumValue}");
            }
            
            output.Content.AppendHtml(sb.ToString());
        }

        private void TextAreaInput(TagHelperOutput output)
        {
            output.TagName = "textarea";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("name", PropertyName);
            string placeholder = PlaceholderHelper(PropertyName);
            output.Attributes.SetAttribute("placeholder", placeholder);
            output.PreContent.SetHtmlContent(Value);
        }

        private void PasswordInput(TagHelperOutput output)
        {
            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("name", PropertyName);
            string placeholder = PlaceholderHelper(PropertyName);
            output.Attributes.SetAttribute("placeholder", placeholder);
            output.Attributes.SetAttribute("type", "password");
        }

        private void RangeInput(TagHelperOutput output)
        {
            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("name", PropertyName);
            output.Attributes.SetAttribute("type", "range");
            NumberInputHelper(output);
            output.Attributes.SetAttribute("value", Value);
        }

        private void EmailInput(TagHelperOutput output)
        {
            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("name", PropertyName);
            output.Attributes.SetAttribute("type", "email");
            output.Attributes.SetAttribute("value", Value);
        }

        private void ColorInput(TagHelperOutput output)
        {
            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("name", PropertyName);
            output.Attributes.SetAttribute("type", "color");
            output.Attributes.SetAttribute("value", Value);
        }

        private void ReadonlyInput(TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("name", PropertyName);
            string placeholder = PlaceholderHelper(PropertyName);
            output.Attributes.SetAttribute("placeholder", placeholder);
            output.Content.SetHtmlContent(Value);
        }

        private void HiddenInput(TagHelperOutput output)
        {
            output.TagName = "hidden";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("name", PropertyName);
        }

        private void NormalInput(TagHelperOutput output)
        {
            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("name", PropertyName);
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            TypeCode requestedPropertyCode = ReflectionHelpers.GetRealTypeCode(requestedProperty);
            string placeholder = PlaceholderHelper(PropertyName);
            switch (requestedPropertyCode)
            {
                case TypeCode.Int32:
                    NormalNumberInputHelper(output);
                    break;
                case TypeCode.Double:
                    NormalNumberInputHelper(output);
                    break;
                case TypeCode.DateTime:
                    NormalDateTimeInputHelper(output);
                    break;
                default:
                    NormalTextInputHelper(output);
                    break;
            }
            output.Attributes.SetAttribute("placeholder", placeholder);
            output.Attributes.SetAttribute("value", Value);
        }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            bool needNormalInput = true;
            foreach (var attribute in typeof(GenericInputAttribute).GetEnumValues())
            {
                string attributeName = Convert.ToString(attribute).Replace("_", ".");
                object _attribute = ReflectionHelpers.GetPropertyAttribute(requestedProperty, Type.GetType(attributeName));
                if (_attribute != null)
                {
                    switch (attribute)
                    {
                        case GenericInputAttribute.RazorPages_Attributes_HiddenAttribute:
                            HiddenInput(output);
                            break;
                        case GenericInputAttribute.RazorPages_Attributes_ReadonlyAttribute:
                            ReadonlyInput(output);
                            break;
                        case GenericInputAttribute.RazorPages_Attributes_TextAreaAttribute:
                            TextAreaInput(output);
                            break;
                        case GenericInputAttribute.RazorPages_Attributes_SelectorFromEnumAttribute:
                            SelectorEnumInput(output);
                            break;
                        case GenericInputAttribute.RazorPages_Attributes_SelectorFromDatabaseAttribute:
                            NormalInput(output); // TODO!!!
                            break;
                        case GenericInputAttribute.RazorPages_Attributes_PasswordAttribute:
                            PasswordInput(output);
                            break;
                        case GenericInputAttribute.RazorPages_Attributes_RadioFromEnumAttribute:
                            RadioEnumInput(output);
                            break;
                        case GenericInputAttribute.RazorPages_Attributes_RangeAttribute:
                            RangeInput(output);
                            break;
                        case GenericInputAttribute.RazorPages_Attributes_ColorAttribute:
                            ColorInput(output);
                            break;
                        case GenericInputAttribute.RazorPages_Attributes_EmailAttribute:
                            EmailInput(output);
                            break;
                    }
                    needNormalInput = false;
                }
            }
            if (needNormalInput)
            {
                NormalInput(output);
            }
        }

        #region Helpers
        private void NormalNumberInputHelper(TagHelperOutput output)
        {
            output.Attributes.SetAttribute("type", "number");
            NumberInputHelper(output);
        }

        private void NormalDateTimeInputHelper(TagHelperOutput output)
        {
            output.Attributes.SetAttribute("type", "date");
        }

        private void NormalTextInputHelper(TagHelperOutput output)
        {
            output.Attributes.SetAttribute("type", "text");
            TextInputHelper(output);
        }

        private void NumberInputHelper(TagHelperOutput output)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object minAttribute = ReflectionHelpers.GetPropertyAttribute<MinimumAttribute>(requestedProperty);
            if (minAttribute != null)
            {
                double minimum = ((MinimumAttribute)minAttribute).Minimum;
                output.Attributes.SetAttribute("min", minimum);
            }
            object maxAttribute = ReflectionHelpers.GetPropertyAttribute<MaximumAttribute>(requestedProperty);
            if (maxAttribute != null)
            {
                double maximum = ((MaximumAttribute)maxAttribute).Maximum;
                output.Attributes.SetAttribute("max", maximum);
            }
            object stepAttribute = ReflectionHelpers.GetPropertyAttribute<StepAttribute>(requestedProperty);
            if (stepAttribute != null)
            {
                double step = ((StepAttribute)stepAttribute).Step;
                output.Attributes.SetAttribute("step", step);
            }
        }

        private void TextInputHelper(TagHelperOutput output)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object maxLengthAttribute = ReflectionHelpers.GetPropertyAttribute<MaximumLengthAttribute>(requestedProperty);
            if (maxLengthAttribute != null)
            {
                double maximum = ((MaximumLengthAttribute)maxLengthAttribute).Maximum;
                output.Attributes.SetAttribute("maxlength", maximum);
            }
        }

        private string PlaceholderHelper(string _default)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object placeholderAttribute = ReflectionHelpers.GetPropertyAttribute<PlaceholderAttribute>(requestedProperty);
            if (placeholderAttribute != null)
            {
                return ((PlaceholderAttribute)placeholderAttribute).Holder;
            }
            return _default;
        }
        #endregion
    }
}

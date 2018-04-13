using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using RazorPages.Enums;
using RazorPages.Helpers;
using RazorPages.Models;
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
        private readonly DogContext _context;
        private string BaseType { get { return Convert.ToString(Expression.Model.GetValue("DeclaringType.FullName")); } }
        private string PropertyName { get { return Convert.ToString(Expression.Model.GetValue("Name")); } }
        public string Value { get; set; }
        public ModelExpression Expression { get; set; }
        #endregion

        #region InputTypes
        private void SelectorEnumInput(TagHelperOutput output)
        {
            output.TagName = "select";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("name", PropertyName);
            output.Content.SetContent(PropertyName);
            StringBuilder sb = new StringBuilder();
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object pool = ReflectionHelpers.GetPropertyAttribute<SelectorFromEnumAttribute>(requestedProperty);
            Type poolType = ((SelectorFromEnumAttribute)pool).Domain;
            if (Value == null || requestedProperty.IsNullable())
            {
                string placeholder = PlaceholderHelper("Choose from...");
                StringBuilder optionAttributes = new StringBuilder();
                if (!requestedProperty.IsNullable())
                {
                    optionAttributes.Append("disabled hidden ");
                }
                if (Value == null)
                {
                    optionAttributes.Append("selected ");
                }
                sb.Append($"<option value='' {optionAttributes}>{placeholder}</option>");
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
        
        private void SelectorDatabase(TagHelperOutput output)
        {
            output.TagName = "select";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("name", PropertyName);
            output.Content.SetContent(PropertyName);
            StringBuilder sb = new StringBuilder();
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object pool = requestedProperty.GetPropertyAttribute<SelectorFromDatabaseAttribute>();
            Type poolType = ((SelectorFromDatabaseAttribute)pool).Domain;
            string[] poolDisplay = ((SelectorFromDatabaseAttribute)pool).DisplayName;
            var list = _context.Set(poolType);
            if (Value == null || requestedProperty.IsNullable())
            {
                string placeholder = PlaceholderHelper("Choose from...");
                StringBuilder optionAttributes = new StringBuilder();
                if (!requestedProperty.IsNullable())
                {
                    optionAttributes.Append("disabled hidden ");
                }
                if (Value == null)
                {
                    optionAttributes.Append("selected ");
                }
                sb.Append($"<option value='' {optionAttributes}>{placeholder}</option>");
            }
            foreach (var item in list)
            {
                string selected = "";
                if (Convert.ToString(item.GetValue("Id")) == Value)
                {
                    selected = "selected";
                }
                StringBuilder optionValue = new StringBuilder();
                foreach (var displayProperty in poolDisplay)
                {
                    optionValue.Append(item.GetValue(displayProperty) + " ");
                }
                sb.Append($"<option value='{item.GetValue("Id")}' {selected}>{optionValue}</option>");
            }
            output.Content.AppendHtml(sb.ToString());
        }

        private void RadioEnumInput(TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            StringBuilder sb = new StringBuilder();
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object pool = requestedProperty.GetPropertyAttribute<RadioFromEnumAttribute>();
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
            object pool = requestedProperty.GetPropertyAttribute<RadioFromEnumAttribute>();
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
            TypeCode requestedPropertyCode = requestedProperty.GetRealTypeCode();
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

        public GenericInputTagHelper(DogContext context)
        {
            _context = context;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            if (!requestedProperty.GetGetMethod().IsVirtual)
            {
                bool needNormalInput = true;
                foreach (var attribute in typeof(GenericInputAttribute).GetEnumValues())
                {
                    string attributeName = Convert.ToString(attribute).Replace("_", ".");
                    object _attribute = requestedProperty.GetPropertyAttribute(Type.GetType(attributeName));
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
                                SelectorDatabase(output);
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
            if (Value != null)
            {
                output.Attributes.SetAttribute("value", Convert.ToDateTime(Value).ToString("yyyy-MM-dd"));
            }
        }

        private void NormalTextInputHelper(TagHelperOutput output)
        {
            output.Attributes.SetAttribute("type", "text");
            TextInputHelper(output);
        }

        private void NumberInputHelper(TagHelperOutput output)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object minAttribute = requestedProperty.GetPropertyAttribute<MinimumAttribute>();
            if (minAttribute != null)
            {
                double minimum = ((MinimumAttribute)minAttribute).Minimum;
                output.Attributes.SetAttribute("min", minimum);
            }
            object maxAttribute = requestedProperty.GetPropertyAttribute<MaximumAttribute>();
            if (maxAttribute != null)
            {
                double maximum = ((MaximumAttribute)maxAttribute).Maximum;
                output.Attributes.SetAttribute("max", maximum);
            }
            object stepAttribute = requestedProperty.GetPropertyAttribute<StepAttribute>();
            if (stepAttribute != null)
            {
                double step = ((StepAttribute)stepAttribute).Step;
                output.Attributes.SetAttribute("step", step);
            }
        }

        private void TextInputHelper(TagHelperOutput output)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object maxLengthAttribute = requestedProperty.GetPropertyAttribute<MaximumLengthAttribute>();
            if (maxLengthAttribute != null)
            {
                double maximum = ((MaximumLengthAttribute)maxLengthAttribute).Maximum;
                output.Attributes.SetAttribute("maxlength", maximum);
            }
        }

        private string PlaceholderHelper(string _default)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            object placeholderAttribute = requestedProperty.GetPropertyAttribute<PlaceholderAttribute>();
            if (placeholderAttribute != null)
            {
                return ((PlaceholderAttribute)placeholderAttribute).Holder;
            }
            return _default;
        }
        #endregion
    }
}

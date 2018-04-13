using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorPages.Attributes;
using RazorPages.HelperModels;
using System;
using System.Reflection;

namespace RazorPages.Helpers
{
    public static class TagHelperHelpers
    {
        public static void NormalNumberInputHelper(this InputModel model)
        {
            model.Output.Attributes.SetAttribute("type", "number");
            model.NumberInputHelper();
        }

        public static void NormalDateTimeInputHelper(this InputModel model)
        {
            model.Output.Attributes.SetAttribute("type", "date");
            if (model.Value != null)
            {
                model.Output.Attributes.SetAttribute("value", Convert.ToDateTime(model.Value).ToString("yyyy-MM-dd"));
            }
        }

        public static void NormalTextInputHelper(this InputModel model)
        {
            model.Output.Attributes.SetAttribute("type", "text");
            model.TextInputHelper();
        }

        public static void NumberInputHelper(this InputModel model)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(model.BaseType, model.PropertyName);
            object minAttribute = requestedProperty.GetPropertyAttribute<MinimumAttribute>();
            if (minAttribute != null)
            {
                double minimum = ((MinimumAttribute)minAttribute).Minimum;
                model.Output.Attributes.SetAttribute("min", minimum);
            }
            object maxAttribute = requestedProperty.GetPropertyAttribute<MaximumAttribute>();
            if (maxAttribute != null)
            {
                double maximum = ((MaximumAttribute)maxAttribute).Maximum;
                model.Output.Attributes.SetAttribute("max", maximum);
            }
            object stepAttribute = requestedProperty.GetPropertyAttribute<StepAttribute>();
            if (stepAttribute != null)
            {
                double step = ((StepAttribute)stepAttribute).Step;
                model.Output.Attributes.SetAttribute("step", step);
            }
        }

        public static void TextInputHelper(this InputModel model)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(model.BaseType, model.PropertyName);
            object maxLengthAttribute = requestedProperty.GetPropertyAttribute<MaximumLengthAttribute>();
            if (maxLengthAttribute != null)
            {
                double maximum = ((MaximumLengthAttribute)maxLengthAttribute).Maximum;
                model.Output.Attributes.SetAttribute("maxlength", maximum);
            }
        }

        public static string PlaceholderHelper(this InputModel model, string _default)
        {
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(model.BaseType, model.PropertyName);
            object placeholderAttribute = requestedProperty.GetPropertyAttribute<PlaceholderAttribute>();
            if (placeholderAttribute != null)
            {
                return ((PlaceholderAttribute)placeholderAttribute).Holder;
            }
            return _default;
        }

        public static void NormalInput(this InputModel model)
        {
            model.Output.TagName = "input";
            model.Output.TagMode = TagMode.SelfClosing;
            model.Output.Attributes.SetAttribute("name", model.PropertyName);
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(model.BaseType, model.PropertyName);
            TypeCode requestedPropertyCode = requestedProperty.GetRealTypeCode();
            string placeholder = model.PlaceholderHelper(model.PropertyName);
            switch (requestedPropertyCode)
            {
                case TypeCode.Int32:
                    model.NormalNumberInputHelper();
                    break;
                case TypeCode.Double:
                    model.NormalNumberInputHelper();
                    break;
                case TypeCode.DateTime:
                    model.NormalDateTimeInputHelper();
                    break;
                default:
                    model.NormalTextInputHelper();
                    break;
            }
            model.Output.Attributes.SetAttribute("placeholder", placeholder);
            model.Output.Attributes.SetAttribute("value", model.Value);
        }
    }
}

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorPages.Enums;
using RazorPages.HelperModels;
using RazorPages.Helpers;
using RazorPages.Interfaces;
using RazorPages.Models;
using System;
using System.Reflection;

namespace RazorPages.TagHelpers
{
    public class GenericInputTagHelper : TagHelper
    {
        private readonly DogContext DatabaseContext;
        private string BaseType { get { return Convert.ToString(Expression.Model.GetValue("DeclaringType.FullName")); } }
        private string PropertyName { get { return Convert.ToString(Expression.Model.GetValue("Name")); } }
        public string Value { get; set; }
        public ModelExpression Expression { get; set; }

        public GenericInputTagHelper(DogContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            InputModel model = new InputModel { Output = output, BaseType = BaseType, PropertyName = PropertyName, Value = Value };
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
                        (_attribute as IGeneralAttribute).GenerateInput(model, DatabaseContext);
                        needNormalInput = false;
                    }
                }
                if (needNormalInput)
                {
                    model.NormalInput();
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RazorPages.Enums;
using RazorPages.HelperModels;
using RazorPages.Helpers;
using RazorPages.Interfaces;
using RazorPages.Models;
using System;
using System.Linq;
using System.Reflection;

namespace RazorPages.TagHelpers
{
    public class GenericInputTagHelper<TContext> : TagHelper where TContext : DbContext
    {
        private readonly TContext DatabaseContext;
        private string BaseType { get { return Convert.ToString(Expression.Model.GetValue("DeclaringType.FullName")); } }
        private string PropertyName { get { return Convert.ToString(Expression.Model.GetValue("Name")); } }
        public string Value { get; set; }
        public ModelExpression Expression { get; set; }

        public GenericInputTagHelper(TContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            InputModel model = new InputModel { Output = output, BaseType = BaseType, PropertyName = PropertyName, Value = Value };
            PropertyInfo requestedProperty = ReflectionHelpers.GetPropertyInfo(BaseType, PropertyName);
            if (!requestedProperty.GetGetMethod().IsVirtual)
            {
                CustomAttributeData attributeData = requestedProperty.GetPropertyLayoutAttributeData();
                if (attributeData != null)
                {
                    object attribute = requestedProperty.GetPropertyAttribute(attributeData.AttributeType);
                    (attribute as IGeneralAttribute).GenerateInput(model, DatabaseContext);
                }
                else
                {
                    model.NormalInput();
                }
            }
        }
    }
}

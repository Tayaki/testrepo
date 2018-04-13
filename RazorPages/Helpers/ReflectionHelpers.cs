using Microsoft.EntityFrameworkCore;
using RazorPages.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RazorPages.Helpers
{
    public static class ReflectionHelpers
    {
        public static PropertyInfo GetPropertyInfo(string baseType, string propertyName)
        {
            Type requestedType = Type.GetType(baseType);
            return requestedType.GetProperty(propertyName);
        }

        public static object GetPropertyAttribute<T>(this PropertyInfo property) where T : class
        {
            return property.GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }

        public static object GetPropertyAttribute(this PropertyInfo property, Type attributeType)
        {
            return property.GetCustomAttributes(attributeType, true).FirstOrDefault();
        }

        public static object GetPropertyValue<T>(this PropertyInfo property, string propertyName) where T : class
        {
            object requestedProperty = property.GetCustomAttributes(typeof(T), true).FirstOrDefault();
            if (requestedProperty != null)
            {
                return (requestedProperty as T).GetType().GetProperty(propertyName).GetValue((requestedProperty as T).GetType());
            }
            return null;
        }

        public static string GetPropertyDatabaseValue<T, TContext>(this PropertyInfo property, object item, TContext _context) where T : class where TContext : DbContext
        {
            object pool = property.GetPropertyAttribute<SelectorFromDatabaseAttribute>();
            if (pool != null)
            {
                var linkedId = property.GetValue(item);
                if (linkedId != null)
                {
                    Type poolType = ((SelectorFromDatabaseAttribute)pool).Domain;
                    var databaseObject = _context.FindInSet(poolType, new object[] { linkedId });
                    string[] poolDisplay = ((SelectorFromDatabaseAttribute)pool).DisplayName;
                    StringBuilder displayValue = new StringBuilder();
                    foreach (var displayProperty in poolDisplay)
                    {
                        displayValue.Append(databaseObject.GetValue(displayProperty) + " ");
                    }
                    return displayValue.ToString();
                }
            }
            return property.GetValue(item)?.ToString();
        }

        public static bool PropertyAttributeExists<T>(this PropertyInfo property) where T : class
        {
            return (property.GetCustomAttributes(typeof(T), true).FirstOrDefault() != null);
        }

        public static TypeCode GetRealTypeCode(this PropertyInfo property)
        {
            TypeCode requestedPropertyCode = Type.GetTypeCode(property.PropertyType);
            
            if (Nullable.GetUnderlyingType(property.PropertyType) != null)
            {
                requestedPropertyCode = Type.GetTypeCode(property.PropertyType.GenericTypeArguments[0]);
            }
            return requestedPropertyCode;
        }

        public static bool IsNullable(this PropertyInfo property)
        {
            return (Nullable.GetUnderlyingType(property.PropertyType) != null);
        }

        public static object GetValue(this object model, string property)
        {
            string[] properties = property.Split(".", StringSplitOptions.RemoveEmptyEntries);
            object result = model;
            foreach (var prop in properties)
            {
                result = result.GetType().GetProperty(prop).GetValue(result);
            }
            return result;
        }

        public static string GetPropertyName(this PropertyInfo property)
        {
            return (property.GetPropertyAttribute<DisplayAttribute>() as DisplayAttribute)?.Name ?? property.Name;
        }
    }
}

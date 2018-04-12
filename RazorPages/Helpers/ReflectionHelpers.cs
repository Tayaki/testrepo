using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
    }
}

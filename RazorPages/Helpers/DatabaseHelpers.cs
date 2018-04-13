using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace RazorPages.Helpers
{
    public static class DatabaseHelpers
    {
        public static IQueryable Set(this DbContext context, Type T)
        {
            MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);
            method = method.MakeGenericMethod(T);
            return method.Invoke(context, null) as IQueryable;
        }

        public static object FindInSet(this DbContext context, Type T, object[] keys)
        {
            MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);
            method = method.MakeGenericMethod(T);
            object dbSet =  method.Invoke(context, null);
            MethodInfo genericMethod = typeof(DbSet<>).MakeGenericType(T).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name == "Find" && m.GetParameters().Count() == 1);
            return genericMethod.Invoke(dbSet, new object[] { keys });
        }
    }
}

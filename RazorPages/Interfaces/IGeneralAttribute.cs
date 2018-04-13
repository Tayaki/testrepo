using Microsoft.EntityFrameworkCore;
using RazorPages.HelperModels;

namespace RazorPages.Interfaces
{
    interface IGeneralAttribute
    {
        void GenerateInput<TContext>(InputModel model, TContext context = null) where TContext : DbContext;
    }
}

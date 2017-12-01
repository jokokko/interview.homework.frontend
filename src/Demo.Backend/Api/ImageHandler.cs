using System.Threading.Tasks;
using Demo.Backend.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Demo.Backend.Api
{
    internal sealed class ImageHandler : Handler
    {
        protected override async Task<(dynamic Model, int StatusCode)> HandleGet(HttpContext context)
        {
            var id = context.IdFrom();

            if (id.HasValue && Catalog.Images.TryGetValue(id.Value, out var value))
            {
                return (value, StatusCodes.Status200OK);
            }

            return (Catalog.Images, StatusCodes.Status200OK);
        }
    }
}
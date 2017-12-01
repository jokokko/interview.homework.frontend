using System.Threading.Tasks;
using Demo.Backend.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Demo.Backend.Api
{
    internal sealed class CartHandler : Handler
    {
        protected override async Task<(dynamic Model, int StatusCode)> HandlePost(HttpContext context)
        {
            var id = context.IdFrom();
            var cart = context.CartFrom();

            if (id.HasValue && Catalog.Prices.TryGetValue(id.Value, out var _))
            {
                Cart.Register(cart, id.Value);
                return (cart, StatusCodes.Status200OK);                
            }

            return ("Provide a valid cart and proudct", StatusCodes.Status500InternalServerError);
        }

        protected override async Task<(dynamic Model, int StatusCode)> HandleDelete(HttpContext context)
        {
            var id = context.IdFrom();
            var cart = context.CartFrom();

            if (id.HasValue && Catalog.Prices.TryGetValue(id.Value, out var _))
            {
                Cart.Remove(cart, id.Value);
                return (cart, StatusCodes.Status200OK);
            }

            return ("Provide a valid cart and proudct", StatusCodes.Status500InternalServerError);
        }
    }    
}
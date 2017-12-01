using System.Threading.Tasks;
using Demo.Backend.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Demo.Backend.Api
{
    internal sealed class PricesHandler : Handler, IInjectFault
    {
        protected override async Task<(dynamic Model, int StatusCode)> HandleGet(HttpContext context)
        {
            var id = context.IdFrom();

            if (id.HasValue && Catalog.Prices.TryGetValue(id.Value, out var value))
            {   
                return (value, StatusCodes.Status200OK);
            }

            return (Catalog.Prices, StatusCodes.Status200OK);
        }

        public int SinceEveryNthRequest { get; } = 10;
        public int WithMillisecondsOfOutage { get; } = 4000;
    }
}
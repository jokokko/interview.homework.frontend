using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Demo.Backend.Infrastructure
{
    internal static class ContextExtensions
    {
        public static int? IdFrom(this HttpContext context)
        {
            var routeData = context.GetRouteData();

            if (int.TryParse((string)routeData.Values["id"], out var n))
            {
                return n;
            }

            return null;
        }

        public static string CartFrom(this HttpContext context)
        {
            var routeData = context.GetRouteData();

            return (string) routeData.Values["cart"];
        }
    }
}
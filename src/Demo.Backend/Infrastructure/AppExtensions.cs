using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Demo.Backend.Infrastructure
{
    internal static class AppExtensions
    {
        private static void RunRoutes(this IApplicationBuilder app, Action<IRouteBuilder> configureRouteBuilder = null)
        {
            var routeBuilder = new RouteBuilder(app);
            configureRouteBuilder?.Invoke(routeBuilder);

            app.UseRouter(routeBuilder.Build());
        }

        public static void RouteToController(this IApplicationBuilder app, string route, Handler handler) =>
            app.RunRoutes(routeBuilder =>
            {
                routeBuilder.MapRoute(route, handler.HandleRequest);
                routeBuilder.MapRoute("*", context =>
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return Task.CompletedTask;
                });
            });
    }
}
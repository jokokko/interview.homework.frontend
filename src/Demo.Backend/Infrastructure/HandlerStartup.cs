using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Demo.Backend.Infrastructure
{
    internal abstract class HandlerStartup<T> where T : Handler, new()
    {
        protected string Route { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory) 
        {
            app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod());
            app.RouteToController(Route ?? "api/{id:int?}", new T());
        }
    }
}
using Demo.Backend.Api;

namespace Demo.Backend.Infrastructure
{
    internal sealed class StartupCart : HandlerStartup<CartHandler>        
    {
        public StartupCart()
        {
            Route = "api/{cart}/{id:int}";
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Demo.Backend.Infrastructure
{
    internal abstract class Handler
    {
        protected Handler()
        {
            handler = Handle;
        }

        protected virtual Task<(dynamic Model, int StatusCode)> HandlePost(HttpContext context)
        {
            throw new InvalidOperationException("Non-supported request method");
        }

        protected virtual Task<(dynamic Model, int StatusCode)> HandleGet(HttpContext context)
        {
            throw new InvalidOperationException("Non-supported request method");
        }

        protected virtual Task<(dynamic Model, int StatusCode)> HandleDelete(HttpContext context)
        {
            throw new InvalidOperationException("Non-supported request method");
        }

        private Func<HttpContext, Task<(dynamic Model, int StatusCode)>> handler;
        private int throttled, requests;

        public async Task HandleRequest(HttpContext context)
        {
            var result = await handler(context);
            context.Response.StatusCode = result.StatusCode;
            context.Response.ContentType = "application/json; charset=utf-8";
            string json = JsonConvert.SerializeObject(result.Model, UseSettings(context));
            await context.Response.WriteAsync(json);
        }
        private async Task<(dynamic Model, int StatusCode)> Handle(HttpContext context)
        {
            async Task<(dynamic Model, int StatusCode)> HandleThrottled(HttpContext ctx, DateTime throttlePoint)
            {
                ctx.Response.Headers.Add("Retry-After", Math.Max(throttlePoint.Subtract(DateTime.Now).Seconds, 0).ToString());
                return (string.Empty, StatusCodes.Status503ServiceUnavailable);
            }

            if (this is IInjectFault fault && Interlocked.Increment(ref requests) % fault.SinceEveryNthRequest == 0 && Interlocked.CompareExchange(ref throttled, 1, 0) == 0)
            {                                                    
                    var throttlePoint = DateTime.Now.AddMilliseconds(fault.WithMillisecondsOfOutage);
                    Task.Delay(fault.WithMillisecondsOfOutage).ContinueWith(_ =>
                    {
                        handler = Handle;
                        Interlocked.CompareExchange(ref throttled, 0, 1);
                    });
                    handler = httpContext => HandleThrottled(context, throttlePoint);                
            }

            (dynamic Model, int StatusCode) result;

            if (context.Request.Method == HttpMethods.Get)
            {
                result = await HandleGet(context);
            }
            else if (context.Request.Method == HttpMethods.Post)
            {
                result = await HandlePost(context);
            }
            else if (context.Request.Method == HttpMethods.Delete)
            {
                result = await HandleDelete(context);
            }
            else
            {
                throw new InvalidOperationException("Non-supported request method");
            }
            return result;
        }

        private static JsonSerializerSettings UseSettings(HttpContext context)
        {
            context.Request.Headers.TryGetValue("Accept-Casing", out var casing);

            switch (casing)
            {
                case "casing/pascal":
                    return new JsonSerializerSettings();

                default:
                    return new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    };
            }
        }
    }
}
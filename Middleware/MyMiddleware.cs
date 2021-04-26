
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace Middleware
{
    public class MyMiddleware
    {
        private RequestDelegate _next;
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var agent = context.Request.Headers["User-Agent"][0].ToLower();

            var MS = agent.Contains("edge")||agent.Contains("edg/")||agent.Contains("trident");
            
            if(!context.Response.HasStarted &&!MS){
                return _next(context);
            }
            else{
                context.Response.ContentType = "text/plain; charset=utf-8";
                return context.Response.WriteAsync("Przeglądarka nie jest obsługiwana");
            }
            
        
        }

    }
}
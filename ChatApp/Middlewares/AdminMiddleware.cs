using Microsoft.AspNetCore.Authentication;

namespace ChatApp.Middlewares
{
    public class AdminMiddleware 
        (RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            string requestPath = context.Request.Path;
            if(requestPath.StartsWith("/admin"))
            {
                if(context.User.HasClaim("Admin", "True"))
                {
                    await _next(context);
                }    
                else
                {
                    context.Response.Redirect("/account/AccessDenied");
                }
            }
            else
            {
                await _next(context);
            }
            
        }

    }
}

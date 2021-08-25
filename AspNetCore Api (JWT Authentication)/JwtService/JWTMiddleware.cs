using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core_API__JWT_Authentication_.JwtService
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate next;

        private readonly IJwtHandler jwtHandler;

        public JWTMiddleware(RequestDelegate _next,IJwtHandler _jwtHandler)
        {
            jwtHandler = _jwtHandler;
            next = _next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies["jwt"];

            if (token != null)
            {
                string username = jwtHandler.ValidateJWTToken(token);

                //use this username to query database if user exits attach account to context

            }

            await next(context);
        }

    }
}

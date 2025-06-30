namespace DigitalService.Middleware.Extensions
{
    public static class UserRoleMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserRoleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserRoleMiddleware>();
        }
    }
}
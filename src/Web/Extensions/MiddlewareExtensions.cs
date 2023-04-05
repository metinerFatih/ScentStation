using Web.Middleware;

namespace Web.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseTransferBasket(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<TransferBasketMiddleware>();
        }
    }
}

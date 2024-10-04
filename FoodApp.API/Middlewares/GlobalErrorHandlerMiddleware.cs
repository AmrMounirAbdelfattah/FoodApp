using FoodApp.Application.Common.Exceptions;
using FoodApp.Application.Common.ViewModels;
using FoodApp.Domain.Enums;

namespace FoodApp.API.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string message = "Error Occured";
                ErrorCode errorCode = ErrorCode.UnKnown;

                if (ex is BusinessException businessException)
                {
                    message = businessException.Message;
                    errorCode = businessException.ErrorCode;
                }
                var result = ResultViewModel<bool>.Faliure(errorCode, message);

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}

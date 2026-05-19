using System.ComponentModel.DataAnnotations;
using System.Net;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace TecPay.Catalog.API.Middleware
{
    public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                await Write(context, HttpStatusCode.BadRequest, "Error de validación", ex.Errors.Select(e => e.ErrorMessage));
            }
            catch (InvalidOperationException ex)
            {
                await Write(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error no encontrado");
                await Write(context, HttpStatusCode.InternalServerError, "Error inesperado del servidor");
            }
        }

        static Task Write(HttpContext c, HttpStatusCode code, string message, object? details = null)
        {
            c.Response.StatusCode = (int)code;
            return c.Response.WriteAsJsonAsync(
                new
                {
                    status = (int)code,
                    message,
                    details
                }
            );
        }
    }

}

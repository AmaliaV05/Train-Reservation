using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using Train_Reservation_Application.Exceptions;
using Train_Reservation_Application.Extensions;

namespace Train_Reservation_Application.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (IdNotFoundException infe)
            {
                await HandleExceptionAsync(httpContext, infe);
            }
            catch (NoMatchException nme)
            {
                await HandleExceptionAsync(httpContext, nme);
            }
            catch (DbUpdateConcurrencyException duce)
            {
                await HandleExceptionAsync(httpContext, duce);
            }
            catch (InvalidOperationException ioe)
            {
                await HandleExceptionAsync(httpContext, ioe);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var message = exception switch
            {
                IdNotFoundException => exception.Message,
                NoMatchException => exception.Message,
                DbUpdateConcurrencyException => exception.Message,
                InvalidOperationException => exception.Message,
                _ => exception.Message
            };

            var stack = exception switch
            {
                IdNotFoundException => exception.StackTrace,
                NoMatchException => exception.StackTrace,
                DbUpdateConcurrencyException => exception.StackTrace,
                InvalidOperationException => exception.StackTrace,
                _ => exception.StackTrace
            };

            await httpContext.Response.AddErrorMessage(httpContext.Response.StatusCode, message, stack);
        }
    }
}

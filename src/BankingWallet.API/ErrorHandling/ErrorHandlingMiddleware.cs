using BankingWallet.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BankingWallet.API.ErrorHandling;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Call the next middleware / endpoint
            await _next(context);
        }
        catch (InsufficientFundsException ex)
        {
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (CurrencyMismatchException ex)
        {
            context.Response.StatusCode = 404; // Not Found
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (InvalidAmountException ex)
        {
            context.Response.StatusCode = 404; // Not Found
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (InvalidTransferException ex)
        {
            context.Response.StatusCode = 404; // Not Found
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (DomainException ex)
        {
            context.Response.StatusCode = 404; // Not Found
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (Exception ex) // Any other unhandled exception
        {
            _logger.LogError(ex, "Unexpected error occurred.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                error = "An unexpected error occurred."
            });
        }
    }
}

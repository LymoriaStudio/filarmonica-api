using System.Net;
using System.Text.Json;
using FilarmonicaMetais.Application.Common.Exceptions;

namespace FilarmonicaMetais.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleAsync(context, ex);
        }
    }

    private async Task HandleAsync(HttpContext context, Exception ex)
    {
        var (statusCode, title) = ex switch
        {
            NotFoundException => (HttpStatusCode.NotFound, "Não encontrado"),
            ValidationException => (HttpStatusCode.BadRequest, "Requisição inválida"),
            ConflictException => (HttpStatusCode.Conflict, "Conflito"),
            UnauthorizedAppException => (HttpStatusCode.Unauthorized, "Não autorizado"),
            _ => (HttpStatusCode.InternalServerError, "Erro interno"),
        };

        if (statusCode == HttpStatusCode.InternalServerError)
            _logger.LogError(ex, "Erro não tratado ao processar {Path}", context.Request.Path);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var body = JsonSerializer.Serialize(new
        {
            title,
            status = (int)statusCode,
            detail = ex.Message,
        });

        await context.Response.WriteAsync(body);
    }
}

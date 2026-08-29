namespace FilarmonicaMetais.Application.Common.Exceptions;

// Nome próprio (não UnauthorizedAccessException) para não colidir com a exceção do BCL
// e para o ExceptionHandlingMiddleware distinguir "erro de auth de negócio" de bug interno.
public class UnauthorizedAppException : Exception
{
    public UnauthorizedAppException(string message) : base(message) { }
}

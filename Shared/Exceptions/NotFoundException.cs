namespace Shared.Exceptions;
public class NotFoundException(string ErrorMessage) : Exception(ErrorMessage) { };
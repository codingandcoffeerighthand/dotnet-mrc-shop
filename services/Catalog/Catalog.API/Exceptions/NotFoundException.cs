namespace Catalog.API.Exceptions;

public class NotFoundException(string ErrorMessage) : Exception(ErrorMessage) { };
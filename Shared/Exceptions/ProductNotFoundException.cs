namespace Shared.Exceptions;

public sealed class ProductNotFoundException() : NotFoundException("product not found") { };

namespace Catalog.API.Exceptions;

public sealed class ProductNotFoundException() : NotFoundException("product not found") { };

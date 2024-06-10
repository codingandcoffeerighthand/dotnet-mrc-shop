namespace Shared.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string typeofException, object key) : base("not found" + typeofException + " " + key) { }
    public NotFoundException(string ExceptionMessage) : base(ExceptionMessage) { }
};
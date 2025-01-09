// This is a custom exception, made for internal logging when an argument is not valid.
namespace DigimonAPI.exceptions;
public class InvalidObjectException : Exception
{
	public InvalidObjectException() : base("Object validation failed: missing required properties.") { }

	public InvalidObjectException(string message) : base(message) { }

	public InvalidObjectException(string message, Exception innerException)
		: base(message, innerException) { }
}
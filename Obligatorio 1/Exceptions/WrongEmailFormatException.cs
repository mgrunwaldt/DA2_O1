namespace Exceptions
{
    public class WrongEmailFormatException: System.Exception
    {
        public WrongEmailFormatException()
        {
        }

        public WrongEmailFormatException(string message) : base(message)
        {
        }
    }
}
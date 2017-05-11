namespace Exceptions
{
    public class WrongNumberFormatException: System.Exception
    {
        public WrongNumberFormatException()
        {
        }

        public WrongNumberFormatException(string message) : base(message)
        {
        }
    }
}
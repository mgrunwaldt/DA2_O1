namespace Exceptions
{
    public class MissingUserDataException: System.Exception
    {
        public MissingUserDataException()
        {
        }

        public MissingUserDataException(string message) : base(message)
        {
        }
    }
}
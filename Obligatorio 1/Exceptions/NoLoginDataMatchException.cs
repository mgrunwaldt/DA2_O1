namespace Exceptions
{
    public class NoLoginDataMatchException : System.Exception
    {
        public NoLoginDataMatchException()
        {
        }

        public NoLoginDataMatchException(string message) : base(message)
        {
        }
    }
}

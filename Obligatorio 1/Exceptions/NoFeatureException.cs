namespace Exceptions
{
    public class NoFeatureException : System.Exception
    {
        public NoFeatureException()
        {
        }

        public NoFeatureException(string message) : base(message)
        {
        }
    }
}
namespace Exceptions
{

    public class ProductFeatureDuplicateFeature : System.Exception
    {
        public ProductFeatureDuplicateFeature()
        {
        }

        public ProductFeatureDuplicateFeature(string message) : base(message)
        {
        }
    }
}
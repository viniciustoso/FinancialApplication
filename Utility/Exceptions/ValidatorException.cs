namespace FinancialApplication.Utility.Exceptions
{
    public class ValidatorException : Exception
    {
        public ValidatorException(string message)
            : base(message)
        {
        }

        public ValidatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

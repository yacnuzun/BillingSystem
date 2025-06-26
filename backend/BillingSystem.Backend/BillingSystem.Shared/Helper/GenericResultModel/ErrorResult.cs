namespace BillingSystem.Shared.Helper.GenericResultModel
{
    public class ErrorResult : Result, IResult
    {
        public ErrorResult(string message) : base(false, message)
        {

        }
        public ErrorResult() : base(false)
        {

        }
    }
}

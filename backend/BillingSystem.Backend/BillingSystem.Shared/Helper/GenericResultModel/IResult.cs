namespace BillingSystem.Shared.Helper.GenericResultModel
{
    public interface IResult
    {
        public bool Success { get; }
        public string Message { get; }
    }
}

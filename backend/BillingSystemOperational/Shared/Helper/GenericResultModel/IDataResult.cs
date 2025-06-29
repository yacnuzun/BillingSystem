namespace Shared.Helper.GenericResultModel
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}

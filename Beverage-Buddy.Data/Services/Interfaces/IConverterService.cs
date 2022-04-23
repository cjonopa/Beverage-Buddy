namespace Beverage_Buddy.Data.Services.Interfaces
{
    public interface IConverterService<T, R>
    {
        T ConvertResult(T item, R itemResult);
    }
}

namespace Sales.Core.Interfaces.Services
{
    public interface IHttpProxy
    {
        Task<TOut> GetAsync<TOut>(string url);

        Task<TOut> PostAsync<TIn, TOut>(TIn input, string url);

        Task<TOut> PostAsync<TOut>(string url);
    }
}

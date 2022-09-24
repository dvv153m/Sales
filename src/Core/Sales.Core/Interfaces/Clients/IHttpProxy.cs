
using System.Threading.Tasks;

namespace Sales.Core.Interfaces.Clients
{
    public interface IHttpProxy
    {
        Task<TOut> GetAsync<TOut>(string url);

        Task PostAsync<TIn>(TIn input, string url);

        Task<TOut> PostAsync<TIn, TOut>(TIn input, string url);

        Task<TOut> PostAsync<TOut>(string url);

        Task DeleteAsync<TIn>(TIn input, string url);
    }
}

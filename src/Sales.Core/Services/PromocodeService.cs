using Sales.Contracts.Entity;
using Sales.Core.Helper;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;

namespace Sales.Core.Services
{
    public class PromocodeService : IPromocodeService
    {
        private readonly IPromocodeRepository _promocodeRepository;
        private readonly int _promocodeLenght;

        public PromocodeService(IPromocodeRepository promocodeRepository, int promocodeLenght)
        {
            promocodeRepository = promocodeRepository ?? throw new ArgumentNullException(nameof(promocodeRepository));            

            _promocodeRepository = promocodeRepository;
            _promocodeLenght = promocodeLenght;
        }

        public async Task AddPromocodeAsync()
        {
            string newPromocode = PromocodeGenerator.Build(_promocodeLenght);
            await _promocodeRepository.AddAsync(new PromocodeEntity { Value = newPromocode });
        }

        public async Task<bool> ExistsAsync(string promocode)
        {
            PromocodeEntity promocodeEntity = await _promocodeRepository.GetByPromocodeAsync(promocode);
            return promocodeEntity != null;
        }
    }
}

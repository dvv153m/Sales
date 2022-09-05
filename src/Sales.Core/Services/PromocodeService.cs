using Sales.Contracts.Entity;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;

namespace Sales.Core.Services
{
    public class PromocodeService : IPromocodeService
    {
        private readonly IPromocodeRepository _promocodeRepository;
        private readonly IPromocodeGenerator _promocodeGenerator;
        private readonly int _promocodeLenght;

        public PromocodeService(IPromocodeRepository promocodeRepository,
                                IPromocodeGenerator promocodeGenerator,
                                int promocodeLenght)
        {
            _promocodeRepository = promocodeRepository ?? throw new ArgumentNullException(nameof(promocodeRepository));
            _promocodeGenerator = promocodeGenerator ?? throw new ArgumentNullException(nameof(promocodeGenerator));

            _promocodeLenght = promocodeLenght;
        }

        public async Task<string> AddPromocodeAsync()
        {
            string newPromocode = _promocodeGenerator.Build(_promocodeLenght);
            await _promocodeRepository.AddAsync(new PromocodeEntity { Value = newPromocode, Role="User" });
            return newPromocode;
        }

        public async Task<bool> ExistsAsync(string promocode)
        {
            PromocodeEntity promocodeEntity = await _promocodeRepository.GetByPromocodeAsync(promocode);
            return promocodeEntity != null;
        }
    }
}

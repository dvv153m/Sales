using Sales.Core.Entity;
using Sales.Core.Domain;
using Sales.Core.Interfaces.Authentication;
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

        public async Task<Promocode> GetByPromocodeAsync(string promocode)
        {
            PromocodeEntity promocodeEntity = await _promocodeRepository.GetByPromocodeAsync(promocode);
            return promocodeEntity != null ? new Promocode { Value = promocodeEntity.Value } : null;
        }

        public async Task<Promocode> AddPromocodeAsync()
        {
            string newPromocode = _promocodeGenerator.Build(_promocodeLenght);
            await _promocodeRepository.AddAsync(new PromocodeEntity { Value = newPromocode, Role="User" });
            return new Promocode { Value = newPromocode };
        }        
    }
}

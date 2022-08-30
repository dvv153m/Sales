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

        public async Task<bool> AddPromocodeAsync()
        {
            try
            {
                byte maxNumberAttemps = 10;
                byte numberAttemps = 0;
                PromocodeEntity promocodeEntity;
                string newPromocode;
                do
                {
                    //генерируем новый промокод и проверяем, что такого еще нет в бд
                    newPromocode = PromocodeGenerator.Build(_promocodeLenght);
                    promocodeEntity = await _promocodeRepository.GetByPromocodeAsync(newPromocode);
                    numberAttemps++;
                }
                while (promocodeEntity != null && numberAttemps < maxNumberAttemps);

                if (promocodeEntity == null)
                {
                    await _promocodeRepository.AddAsync(new PromocodeEntity { Value = newPromocode });
                }
                else
                { 
                    return false;
                }
            }
            catch (Exception ex)
            {
                //loging
                return false;
            }
            return true;
        }

        public async Task<bool> GetByPromocodeAsync(string promocode)
        {
            try
            {
                PromocodeEntity promocodeEntity = await _promocodeRepository.GetByPromocodeAsync(promocode);
                if (promocodeEntity == null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            { 
                //loging
            }
            return true;
        }        
    }
}

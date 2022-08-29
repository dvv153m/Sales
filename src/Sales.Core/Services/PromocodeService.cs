using Sales.Contracts.Entity;
using Sales.Core.Interfaces.Repositories;
using Sales.Core.Interfaces.Services;


namespace Sales.Core.Services
{
    public class PromocodeService : IPromocodeService
    {
        private readonly IPromocodeRepository _promocodeRepository;

        private readonly IPromocodeGenerator _promocodeGenerator;

        public PromocodeService(IPromocodeRepository promocodeRepository,
                                IPromocodeGenerator promocodeGenerator)
        {
            if (promocodeRepository == null)
                throw new ArgumentNullException(nameof(promocodeRepository));

            if (promocodeGenerator == null)
                throw new ArgumentNullException(nameof(promocodeGenerator));

            _promocodeRepository = promocodeRepository; 
            _promocodeGenerator = promocodeGenerator;
        }

        public bool AddPromocode()
        {
            try
            {
                byte maxNumberAttemps = 10;
                byte numberAttemps = 0;
                PromocodeEntity promocodeEntity;
                string newPromocode;
                do
                {
                    //генерируем новый промокод и проверяем что такого еще нет в бд
                    newPromocode = _promocodeGenerator.Build();
                    promocodeEntity = _promocodeRepository.GetByPromocode(newPromocode);
                    numberAttemps++;
                }
                while (promocodeEntity != null || numberAttemps < maxNumberAttemps);

                if (promocodeEntity == null)
                {
                    _promocodeRepository.Add(new PromocodeEntity { Value = newPromocode, CreatedDate = DateTime.Now });
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

        public bool LoginByPromocode(string promocode)
        {
            try
            {
                PromocodeEntity promocodeEntity = _promocodeRepository.GetByPromocode(promocode);
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

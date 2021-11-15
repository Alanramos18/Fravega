using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Entity;
using Fravega.Business.Extensions;
using Fravega.Business.Services.Interfaces;
using Fravega.Data.Repositories.Interfaces;
using Fravega.Dto;
using Microsoft.Extensions.Logging;

namespace Fravega.Business.Services
{
    public class PromocionesServices : IPromocionesServices
    {
        private readonly IPromocionesRepository _repository;
        private readonly ILogger<PromocionesServices> _logger;

        public PromocionesServices(IPromocionesRepository repository, ILogger<PromocionesServices> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public Task<List<Promocion>> GetListAsync(CancellationToken cancellationToken)
        {
            return _repository.GetAllAsync(cancellationToken);
        }

        /// <inheritdoc />
        public Task<Promocion> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<List<Promocion>> GetActivePromotionsAsync(DateTime? date, CancellationToken cancellationToken)
        {
            return (await _repository.GetActiveAsync(date, cancellationToken)).ToList();
        }

        /// <inheritdoc />
        public Task<IEnumerable<Promocion>> GetActiveForSalePromotionsAsync(GetSalePromotionDto promotionDto, CancellationToken cancellationToken)
        {
            return _repository.GetActiveForSaleAsync(promotionDto, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Promocion> CreateAsync(PromotionDto promotionDto, CancellationToken cancellationToken)
        {
            try
            {
                var prom = promotionDto.Convert(null);
                await _repository.CreateAsync(prom, cancellationToken);

                return prom;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw e;
            }
        }

        /// <inheritdoc />
        public async Task<Promocion> UpdateAsync(Guid id, PromotionDto promotionDto, CancellationToken cancellationToken)
        {
            try
            {
                var prom = promotionDto.Convert(id);
                //prom.FechaModificacion = DateTime.Now;
                await _repository.UpdateAsync(id, prom, cancellationToken);

                return prom;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw e;
            }
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteAsync(id, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw e;
            }
        }

        /// <inheritdoc />
        public async Task<Promocion> UpdateValidityAsync(Guid id, DateTime? start, DateTime? end, CancellationToken cancellationToken)
        {
            try
            {
                var prom = await _repository.GetByIdAsync(id, cancellationToken);

                prom.UpdateDate(start, end);

                await _repository.UpdateAsync(id, prom, cancellationToken);

                return prom;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw e;
            }
        }
    }
}

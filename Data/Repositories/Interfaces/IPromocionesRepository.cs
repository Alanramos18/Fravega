using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.Entity;
using Fravega.Dto;

namespace Fravega.Data.Repositories.Interfaces
{
    public interface IPromocionesRepository
    {
        Task<List<Promocion>> GetAllAsync(CancellationToken cancellationToken);
        Task<Promocion> GetByIdAsync(Guid guid, CancellationToken cancellationToken);
        Task<IList<Promocion>> GetActiveAsync(DateTime? date, CancellationToken cancellationToken);
        Task<IEnumerable<Promocion>> GetActiveForSaleAsync(GetSalePromotionDto dto, CancellationToken cancellationToken);
        Task CreateAsync(Promocion prom, CancellationToken cancellationToken);
        Task UpdateAsync(Guid id, Promocion prom, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

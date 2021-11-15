using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Entity;
using Fravega.Data.Repositories.Interfaces;
using Fravega.Dto;
using MongoDB.Driver;

namespace Fravega.Data.Repositories
{
    public class PromocionesRepository : IPromocionesRepository
    {
        private readonly IFravegaContext _context;

        public PromocionesRepository(IFravegaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<List<Promocion>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _context.DbSet.Find(x => true).ToListAsync(cancellationToken);
        }

        public Task<Promocion> GetByIdAsync(Guid guid, CancellationToken cancellationToken)
        {
            return _context.DbSet.Find(x => x.Id == guid).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IList<Promocion>> GetActiveAsync(DateTime? date, CancellationToken cancellationToken)
        {
            FilterDefinition<Promocion> filter;
            FilterDefinition<Promocion> dateFilter;

            filter = Builders<Promocion>.Filter.Eq(x => x.Activo, true);

            if (date != null)
            {
                dateFilter = Builders<Promocion>.Filter.Lt(x => x.FechaFin, date);
                filter = filter & dateFilter;
            }

            return (await _context.DbSet.FindAsync(filter, cancellationToken: cancellationToken)).ToList();
        }

        public async Task<IEnumerable<Promocion>> GetActiveForSaleAsync(GetSalePromotionDto dto, CancellationToken cancellationToken)
        {
            var result = await _context.DbSet.FindAsync(
                x => x.MediosDePago.Any(mp => mp.Contains(dto.PaymentMethod)) &&
                x.Bancos.Any(b => b.Contains(dto.Bank)) &&
                x.CategoriasProductos == dto.Categories, cancellationToken: cancellationToken);

            return result.ToEnumerable<Promocion>();
        }

        public Task CreateAsync(Promocion prom, CancellationToken cancellationToken)
        {
            return _context.DbSet.InsertOneAsync(prom, cancellationToken: cancellationToken);
        }

        public Task UpdateAsync(Guid id, Promocion prom, CancellationToken cancellationToken)
        {
            return _context.DbSet.FindOneAndReplaceAsync(x => x.Id == id, prom, cancellationToken: cancellationToken);
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.DbSet.DeleteOneAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }
    }
}

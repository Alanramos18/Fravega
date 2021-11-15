using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.Entity;
using Fravega.Dto;

namespace Fravega.Business.Services.Interfaces
{
    public interface IPromocionesServices
    {
        /// <summary>
        ///     Get a list of all promotions
        /// </summary>
        /// <param name="cancellationToken">Transaction Cancellation Token</param>
        /// <returns></returns>
        Task<List<Promocion>> GetListAsync(CancellationToken cancellationToken);

        /// <summary>
        ///     Get a promotion by its ID
        /// </summary>
        /// <param name="id">Id of the promotion</param>
        /// <param name="cancellationToken">Transaction Cancellation Token</param>
        /// <returns></returns>
        Task<Promocion> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        ///     Get all active promotions available by date.
        ///     If no date is input then all active promotions will be returned.
        /// </summary>
        /// <param name="date">Date for filtering the promotions</param>
        /// <param name="cancellationToken">Transaction Cancellation Token</param>
        /// <returns></returns>
        Task<List<Promocion>> GetActivePromotionsAsync(DateTime? date, CancellationToken cancellationToken);

        /// <summary>
        ///     Get an active promotion for sale
        /// </summary>
        /// <param name="promotionDto"></param>
        /// <param name="cancellationToken">Transaction Cancellation Token</param>
        /// <returns></returns>
        Task<IEnumerable<Promocion>> GetActiveForSalePromotionsAsync(GetSalePromotionDto promotionDto, CancellationToken cancellationToken);

        /// <summary>
        ///     Create a promotion
        /// </summary>
        /// <param name="promotionDto">Dto of promotion</param>
        /// <param name="cancellationToken">Transaction Cancellation Token</param>
        /// <returns></returns>
        Task<Promocion> CreateAsync(PromotionDto promotionDto, CancellationToken cancellationToken);

        /// <summary>
        ///     Update a promotion
        /// </summary>
        /// <param name="id">Id of the promotion</param>
        /// <param name="promotion">Dto of promotion</param>
        /// <param name="cancellationToken">Transaction Cancellation Token</param>
        /// <returns></returns>
        Task<Promocion> UpdateAsync(Guid id, PromotionDto promotion, CancellationToken cancellationToken);

        /// <summary>
        ///     Delete a promotion by its id
        /// </summary>
        /// <param name="id">Id of the promotion</param>
        /// <param name="cancellationToken">Transaction Cancellation Token</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        ///     Update the dates of promotions
        /// </summary>
        /// <param name="id">Id of the promotion</param>
        /// <param name="start">Start Date</param>
        /// <param name="end">End date</param>
        /// <param name="cancellationToken">Transaction Cancellation Token</param>
        /// <returns></returns>
        Task<Promocion> UpdateValidityAsync(Guid id, DateTime? start, DateTime? end, CancellationToken cancellationToken);
    }
}

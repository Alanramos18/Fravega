using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Entity;
using Fravega.Business.Services.Interfaces;
using Fravega.Dto;
using Fravega.Web.Exceptions;
using Fravega.Web.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fravega.Web.Controllers
{
    [ApiController]
    [Route("api/promociones")]
    public class PromocionesController
    {
        private readonly IPromocionesServices _promocionesService;

        public PromocionesController(IPromocionesServices promocionesServices)
        {
            _promocionesService = promocionesServices ?? throw new ArgumentNullException(nameof(promocionesServices));
        }

        /// <summary>
        ///     Get all the promotions available
        /// </summary>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns>A list of promotions</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not Found.</response>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Promocion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> GetListAsync(CancellationToken cancellationToken = default)
        {
            var result = await _promocionesService.GetListAsync(cancellationToken);

            if (result == null || !result.Any())
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }

        /// <summary>
        ///     Get a promotions by GUID
        /// </summary>
        /// <param name="id">Id of the promotion</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns>A list of promotions</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not Found.</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Promocion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _promocionesService.GetByIdAsync(id, cancellationToken);

            if (result == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }

        /// <summary>
        ///     Get all active promotions available by date.
        ///     If no date is input then all active promotions will be returned.
        /// </summary>
        /// <param name="date">Date for active promotions</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns>A list of promotions</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not Found.</response>
        [HttpPost]
        [Route("active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Promocion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> GetActivePromotionsAsync([FromBody]DateTime? date, CancellationToken cancellationToken = default)
        {
            var result = await _promocionesService.GetActivePromotionsAsync(date, cancellationToken);

            if (result == null || !result.Any())
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }

        /// <summary>
        ///     Get all the promotions for a specific sale
        /// </summary>
        /// <param name="promotionDto"></param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns>A list of promotions</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not Found.</response>
        [HttpPost]
        [Route("promotions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Promocion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> GetSalePromotionAsync([FromBody]GetSalePromotionDto promotionDto, CancellationToken cancellationToken = default)
        {
            var result = await _promocionesService.GetActiveForSalePromotionsAsync(promotionDto, cancellationToken);

            if (result == null || result.Count() == 0)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }

        /// <summary>
        ///     Create a promotion
        /// </summary>
        /// <param name="promotion">Dto for creating a promotion</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns>A list of promotions</returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        public async Task<ActionResult> CreateAsync([FromBody]PromotionDto promotion, CancellationToken cancellationToken = default)
        {
            try
            {
                if (promotion == null)
                    return new BadRequestObjectResult("Error al cargar los datos");

                PromotionValidation.ValidateData(promotion);

                var result = await _promocionesService.CreateAsync(promotion, cancellationToken);

                if (result == null)
                {
                    return new BadRequestResult();
                }

                return new OkObjectResult(result.Id);
            }
            catch (PromotionException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        ///     Update a promotion filter by guid
        /// </summary>
        /// <param name="id">Id of promotion to update</param>
        /// <param name="promotion">Dto of updated promotion</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns>A list of promotions</returns>
        /// <response code="200">Ok.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not Found.</response>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Promocion))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> UpdateAsync([FromQuery] Guid id, [FromBody]PromotionDto promotion, CancellationToken cancellationToken = default)
        {
            try
            {
                if (promotion == null)
                    return new BadRequestObjectResult("Error al cargar los datos");

                PromotionValidation.ValidateData(promotion);

                var result = await _promocionesService.UpdateAsync(id, promotion, cancellationToken);

                if (result == null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(result);
            }
            catch (PromotionException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all the promotions available
        /// </summary>
        /// <param name="id">Id of promotion</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns>A list of promotions</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not Found.</response>
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> DeleteAsync([FromQuery] Guid id, CancellationToken cancellationToken = default)
        {
            await _promocionesService.DeleteAsync(id, cancellationToken);

            return new OkResult();
        }

        /// <summary>
        ///     Update a promotion active dates
        /// </summary>
        /// <param name="id">Id of promotion</param>
        /// <param name="start">Start date of promotion</param>
        /// <param name="end">End date of promotion</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns>A list of promotions</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not Found.</response>
        [HttpPost]
        [Route("/updateValidity")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Promocion))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> UpdateValidityAsync([FromQuery] Guid id, [FromRoute]DateTime? start, [FromRoute] DateTime? end, CancellationToken cancellationToken = default)
        {
            var result = await _promocionesService.UpdateValidityAsync(id, start, end, cancellationToken);

            if (result == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }
    }
}

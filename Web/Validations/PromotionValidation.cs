using System;
using System.Collections.Generic;
using System.Linq;
using Fravega.Business.Utilities;
using Fravega.Dto;
using Fravega.Web.Exceptions;

namespace Fravega.Web.Validations
{
    public static class PromotionValidation
    {
        public static void ValidateData(PromotionDto promotionDto)
        {
            ValidatePayments(promotionDto.PaymentMethods);
            ValidateBank(promotionDto.Banks);
            ValidateCategory(promotionDto.Categories);

            var discount = promotionDto.DiscountPorcentage;
            var payNum = promotionDto.PaymentsNumber;

            if (discount == null && payNum == null)
                throw new PromotionException("La promocion debe tener o un descuento o un numero de cuotas");

            if (discount < 5 || discount > 80)
                throw new PromotionException("El descuento debe tener un rango de 5 a 80");

            if (payNum == null && promotionDto.PaymentInterestPorcentage != null)
                throw new PromotionException("No puede tener un valor el interes de cuotas si no hay cuotas");

            if ((discount != null && discount != 0) && (payNum != null && payNum != 0))
                throw new PromotionException("La promocion no pude tener descuento y cuotas a la vez");

            if ((promotionDto.EndDate - promotionDto.StartDate).Value.TotalDays < 0)
                throw new PromotionException("La fecha de fin no puede ser mayor que la fecha de inicio");
        }

        /// <summary>
        ///     Validate the payment data
        /// </summary>
        /// <param name="payments"></param>
        private static void ValidatePayments(IEnumerable<string> payments)
        {
            if (payments == null || payments.Count() == 0)
                return;

            foreach (var pay in payments)
            {
                if (!MediosDePago.ListaDePagos().Contains(pay))
                    throw new PromotionException("Uno de los metodos de pago no es valido");
            }
        }

        /// <summary>
        ///     Validate the bank data
        /// </summary>
        /// <param name="banks"></param>
        private static void ValidateBank(IEnumerable<string> banks)
        {
            if (banks == null || banks.Count() == 0)
                return;

            foreach (var bank in banks)
            {
                if (!Bancos.ListaDeBancos().Contains(bank))
                    throw new PromotionException("Uno de los bancos no es valido");
            }
        }

        /// <summary>
        ///     Validate the categories data
        /// </summary>
        /// <param name="categories"></param>
        private static void ValidateCategory(IEnumerable<string> categories)
        {
            if (categories == null || categories.Count() == 0)
                return;

            foreach (var category in categories)
            {
                if (!CategoriasProductos.ListaDeCategorias().Contains(category))
                    throw new PromotionException("Una de las categorias no es valida");
            }
        }
    }
}

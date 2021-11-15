using System;
using System.Collections.Generic;

namespace Fravega.Dto
{
    public class PromotionDto
    {
        public IEnumerable<string> PaymentMethods { get; set; }
        public IEnumerable<string> Banks { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public int? PaymentsNumber { get; set; }
        public decimal? PaymentInterestPorcentage { get; set; }
        public decimal? DiscountPorcentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

using System.Collections.Generic;

namespace Fravega.Dto
{
    public class GetSalePromotionDto
    {
        public string PaymentMethod { get; set; }
        public string Bank { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}

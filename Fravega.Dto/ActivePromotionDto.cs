namespace Fravega.Dto
{
    public class ActivePromotionDto
    {
        public string Id { get; set; }
        public string PaymentMethod { get; set; }
        public string Bank { get; set; }
        public string Category { get; set; }
        public int? PaymentsNumber { get; set; }
        public decimal? PaymentInterestPorcentage { get; set; }
        public decimal? DiscountPorcentage { get; set; }
    }
}

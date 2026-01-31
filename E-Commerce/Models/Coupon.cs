namespace E_Commerce.Models
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; }
        public string Type { get; set; }  
        public decimal Value { get; set; }
        public decimal MinimumOrderAmount { get; set; }
        public decimal MaximumDiscountAmount { get; set; }
        public int UsageLimit { get; set; }
        public int UsedCount { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }

   
}

namespace E_Commerce.Models
{
    public class Address : BaseEntity
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Apartment { get; set; }
        public int PostalCode { get; set; }

        public bool IsDefaultShipping { get; set; }
        public bool IsDefaultBilling { get; set; }
    }
}

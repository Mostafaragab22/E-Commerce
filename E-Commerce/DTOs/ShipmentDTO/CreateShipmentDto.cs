namespace E_Commerce.DTOs.ShipmentDTO
{
    public class CreateShipmentDto
    {
        public long OrderId { get; set; }          
        public string ShippingCompany { get; set; } 
        public string TrackingNumber { get; set; }
        public string Address { get; set; }
}
}

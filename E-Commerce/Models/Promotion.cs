using E_Commerce.Models;

public class Promotion : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string BannerImage { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }
}
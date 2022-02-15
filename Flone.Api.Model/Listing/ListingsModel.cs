using Flone.Data.Models.Constant;

namespace Flone.Api.Models.Listing
{
    public class ListingsModel
    {
        public string? UserId { get; set; }
        public Guid BrandId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid MarketId { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsFreeShiping { get; set; } = false;
        public WeightUnits WeightUnit { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}

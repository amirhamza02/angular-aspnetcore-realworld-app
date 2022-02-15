namespace Flone.Api.Models.Listing
{
    public class MarketsModel
    {
        public MarketsModel()
        {
            Categories = new List<CategoryModel>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}

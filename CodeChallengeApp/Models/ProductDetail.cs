namespace CodeChallengeApp.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
		public string ImageFileName { get; set; }
        //public string ImagePath { get; set; } 

    }
}

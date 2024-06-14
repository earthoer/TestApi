namespace TestApi.Models
{
    public class BaseModel
    {
        public List<Product> Products { get; set; }

    }
    public class Product
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}

namespace TestApi.Models
{
    public class ResponseModel
    {
        public Boolean status { get; set; } = false;
        public String message { get; set; }
        public int total { get; set; } = 0;
        public object data { get; set; }
    }
}

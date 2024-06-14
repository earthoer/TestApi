using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;
using TestApi.Models;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpPost]
        public async Task<ResponseModel> CreateData(Product product)
        {
            ResponseModel res = new ResponseModel();
            if (string.IsNullOrEmpty(product.name))
            {
                res.message = "need prod name";
                return res;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "products.json");

            if (!System.IO.File.Exists(filePath))
            {
                using (var fileStream = System.IO.File.Create(filePath))
                {

                }
            }
            string existingData = await System.IO.File.ReadAllTextAsync(filePath);
            var existingProducts = string.IsNullOrWhiteSpace(existingData)
                                ? new List<Product>()
                                : JsonSerializer.Deserialize<List<Product>>(existingData);
            product.value = product.value * 10;
            existingProducts.Add(product);
            var jsonData = JsonSerializer.Serialize(existingProducts, new JsonSerializerOptions { WriteIndented = true });

            await System.IO.File.WriteAllTextAsync(filePath, jsonData);

            res.status = true;
            res.message = "success";
            res.data = product;
            return res;
        }
        [HttpGet]
        public async Task<ResponseModel> GetData()
        {
            ResponseModel res = new ResponseModel();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "products.json");
            if (!System.IO.File.Exists(filePath))
            {
                res.message = "don't found any data";
                return res;
            }
            string existingData = await System.IO.File.ReadAllTextAsync(filePath);
            var existingProducts = string.IsNullOrWhiteSpace(existingData)
                                ? new List<Product>()
                                : JsonSerializer.Deserialize<List<Product>>(existingData);



            res.status = true;
            res.message = "success";
            res.data = existingProducts;
            return res;
        }
        [HttpGet("value")]
        public async Task<ResponseModel> GetValueByName(int value)
        {
            ResponseModel res = new ResponseModel();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "products.json");
            if (!System.IO.File.Exists(filePath))
            {
                res.message = "don't found any data";
                return res;
            }
            string existingData = await System.IO.File.ReadAllTextAsync(filePath);
            var existingProducts = string.IsNullOrWhiteSpace(existingData)
                                ? new List<Product>()
                                : JsonSerializer.Deserialize<List<Product>>(existingData);
            List<Product> prod = existingProducts.Where(x => x.value>value).ToList();


            res.status = true;
            res.message = "success";
            res.data = prod;
            return res;
        }
    }
}

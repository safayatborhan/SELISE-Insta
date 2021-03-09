using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Newtonsoft.Json.Converters;

namespace InstaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private InMemoryRepository _inMemoryRepository;

        public ProductsController(InMemoryRepository inMemoryRepository)
        {
            _inMemoryRepository = inMemoryRepository;
        }

        [HttpGet]
        public ActionResult<Product> GetProduct()
        {
            var product = _inMemoryRepository.GetProduct();
            return Ok(product);
        }

        [HttpPost]
        public IActionResult SaveProduct([FromBody] Product product)
        {
            product.CustomData = product.CustomData.ToString();
            _inMemoryRepository.SaveProduct(product);
            return Ok();
        }
    }

    public class InMemoryRepository
    {
        private string _productData;
        public void SaveProduct(Product product)
        {
            _productData = JsonConvert.SerializeObject(product);
        }
        public Product GetProduct()
        {
            var product = JsonConvert.DeserializeObject<Product>(_productData);

            product.CustomData = JsonConvert.DeserializeObject<ExpandoObject>
                (product.CustomData?.ToString() ?? string.Empty, new ExpandoObjectConverter());

            return product;
        }
    }

    public class Product
    {
        public string Name { get; set; }

        public object CustomData { get; set; }
    }

    public class CustomData
    {
        public List<Language> Languages { get; set; }
    }

    public class Language
    {
        public string Locale { get; set; }
        public string Value { get; set; }
    }
}

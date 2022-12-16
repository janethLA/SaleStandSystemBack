using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MySalesStandSystem.Interfaces;
using MySalesStandSystem.Models;
using static System.Net.Mime.MediaTypeNames;

namespace MySalesStandSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        [ActionName(nameof(GetProductsAsync))]
        public IEnumerable<Product> GetProductsAsync()
        {
            return _productRepository.GetProducts();
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetProductById))]
        public ActionResult<Product> GetProductById(int id)
        {
            var productByID = _productRepository.GetProductById(id);
            if (productByID == null)
            {
                return NotFound();
            }
            return productByID;
        }
        //[System.Web.Http.Route(**"api/imagenes/GetImagenLink" * *)]
        //[HttpPost, Route("cargar-archivo")]
        [HttpPost]
        [ActionName(nameof(CreateProductAsync))]
        public async Task<ActionResult<Product>> CreateProductAsync([FromForm] Product product, [FromForm] IFormFile image)
        { 
             using (var ms = new MemoryStream())
             {
                image.CopyTo(ms);
                byte[] fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                product.image = fileBytes;
                // act on the Base64 data
             }
           
            /* try
                 {
                    // file.CopyTo(ms);
                     byte[] fileBytes = image.
                     string s = Convert.ToBase64String(fileBytes);
                     product.image = fileBytes;
                 }
                 catch (Exception e)
                 {
                     // TODO: handle exception
                 }*/


            await _productRepository.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.id }, product);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(UpdateProduct))]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            var c = _productRepository.GetProductById(id);
            if (c == null)
            {
                return NotFound();
            }
            if (c.productName.IsNullOrEmpty())
            {
                Console.WriteLine("El campo esta vacio");
            }
            c.productName = product.productName;
            c.description= product.description;
            c.price = product.price;
            c.measurement = product.measurement;
            c.quantity = product.quantity;

            await _productRepository.UpdateProductAsync(c);
            return NoContent();

        }

        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteProduct))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteProductAsync(product);

            return NoContent();
        }
    }
}

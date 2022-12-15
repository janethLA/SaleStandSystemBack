using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySalesStandSystem.Models;
using MySalesStandSystem.Output;
using MySalesStandSystem.Repository;
using System.Data;

namespace MySalesStandSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesStandController : ControllerBase
    {
        private ISalesStandRepository _salesStandRepository;
        public SalesStandController(ISalesStandRepository salesStandRepository)
        {
            _salesStandRepository = salesStandRepository;
        }


        [HttpGet]
        [ActionName(nameof(GetSalesStandsAsync))]
        public IEnumerable<SalesStand> GetSalesStandsAsync()
        {
            return _salesStandRepository.GetSalesStands();
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetSalesStandById))]
        public ActionResult<SalesStand> GetSalesStandById(int id)
        {
            var salesStandByID = _salesStandRepository.GetSalesStandById(id);
            if (salesStandByID == null)
            {
                return NotFound();
            }
            return salesStandByID;
        }

        [HttpPost]
        [ActionName(nameof(CreateSalesStandAsync))]
        public async Task<ActionResult<SalesStand>> CreateSalesStandAsync(SalesStand salesStand)
        {
            await _salesStandRepository.CreateSalesStandAsync(salesStand);
            return CreatedAtAction(nameof(GetSalesStandById), new { id = salesStand.id }, salesStand);
        }

        [HttpPut("{id}")]
        [ActionName(nameof(UpdateSalesStand))]
        //[Authorize(Roles = ("seller"))]
        public async Task<ActionResult> UpdateSalesStand(int id, [FromForm] SalesStand salesStand, [FromForm] IFormFile? image)
        {
            await _salesStandRepository.UpdateSalesStandAsync(id,salesStand, image);
            return NoContent();

        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = ("seller"))]
        [ActionName(nameof(DeleteSalesStand))]
        public async Task<IActionResult> DeleteSalesStand(int id)
        {
            var salesStand = _salesStandRepository.GetSalesStandById(id);
            if (salesStand == null)
            {
                return NotFound();
            }

            await _salesStandRepository.DeleteSalesStandAsync(salesStand);

            return NoContent();
        }

        [HttpGet("/api/allSalesStands")]
        [ActionName(nameof(GetSalesStands))]
        public List<SalesStandOutput> GetSalesStands()
        {
            return _salesStandRepository.GetAllSalesStands();
        }


        [HttpPost("/api/createSale")]
        //[Authorize(Roles = ("seller"))]
        [ActionName(nameof(CreateSalesStand2))]
        public async Task<ActionResult<Product>> CreateSalesStand2([FromForm] SalesStand salesStand, [FromForm] IFormFile image)
        {
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                byte[] fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
                salesStand.image = fileBytes;
            }
            await _salesStandRepository.CreateSalesStandAsync(salesStand);
            return CreatedAtAction(nameof(GetSalesStandById), new { id = salesStand.id }, salesStand);
        }

        
       
    }
}

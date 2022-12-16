using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySalesStandSystem.Input;
using MySalesStandSystem.Interfaces;
using MySalesStandSystem.Models;
using MySalesStandSystem.Output;

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
       
        [HttpPut("{id}")]
        [ActionName(nameof(UpdateSalesStand))]
        public async Task<ActionResult> UpdateSalesStand(int id, [FromForm] SalesStand salesStand, [FromForm] IFormFile? image)
        {
            await _salesStandRepository.UpdateSalesStandAsync(id, salesStand, image);
            return NoContent();

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ("seller"))]
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
        [Authorize(Roles = ("seller"))]
        [ActionName(nameof(CreateSalesStand2))]
        public async Task<ActionResult<Product>> CreateSalesStand2([FromForm] SalesStand salesStand, [FromForm] IFormFile image)
        {
           
            await _salesStandRepository.CreateSalesStandAsync(salesStand,image);
            return CreatedAtAction(nameof(GetSalesStandById), new { id = salesStand.id }, salesStand);
        }

        
       
    }
}

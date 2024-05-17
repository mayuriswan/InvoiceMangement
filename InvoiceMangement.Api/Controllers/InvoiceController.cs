using InvoiceMangement.Api.Models;
using InvoiceMangement.Api.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceMangement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _repository;

        public InvoiceController(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Invoice>> Get()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> Get(int id)
        {
            var invoice = await _repository.GetByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return invoice;
        }

        [HttpPost]
        public async Task<ActionResult<Invoice>> Post(Invoice invoice)
        {
            await _repository.AddAsync(invoice);
            return CreatedAtAction(nameof(Get), new { id = invoice.InvoiceID }, invoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Invoice invoice)
        {
            if (id != invoice.InvoiceID)
            {
                return BadRequest();
            }
            await _repository.UpdateAsync(invoice);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
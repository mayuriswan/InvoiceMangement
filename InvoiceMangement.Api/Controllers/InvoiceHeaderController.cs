using InvoiceMangement.Api.Models;
using InvoiceMangement.Api.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceMangement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceHeaderController : ControllerBase
    {
        private readonly IInvoiceHeaderRepository _repository;

        public InvoiceHeaderController(IInvoiceHeaderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<InvoiceHeader>> Get()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceHeader>> Get(int id)
        {
            var invoiceHeader = await _repository.GetByIdAsync(id);
            if (invoiceHeader == null)
            {
                return NotFound();
            }
            return invoiceHeader;
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceHeader>> Post(InvoiceHeader invoiceHeader)
        {
            if (invoiceHeader.InvoiceLineItems == null)
            {
                invoiceHeader.InvoiceLineItems = new List<InvoiceLineItem>();
            }
            await _repository.AddAsync(invoiceHeader);
            return CreatedAtAction(nameof(Get), new { id = invoiceHeader.InvoiceHeaderId }, invoiceHeader);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, InvoiceHeader invoiceHeader)
        {
            if (id != invoiceHeader.InvoiceHeaderId)
            {
                return BadRequest();
            }
            await _repository.UpdateAsync(invoiceHeader);
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
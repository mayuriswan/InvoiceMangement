using InvoiceMangement.Api.Models;
using InvoiceMangement.Api.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceMangement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceLineItemController : ControllerBase
    {
        private readonly IInvoiceLineItemRepository _repository;

        public InvoiceLineItemController(IInvoiceLineItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("by-header/{invoiceHeaderId}")]
        public async Task<IEnumerable<InvoiceLineItem>> GetByInvoiceHeaderId(int invoiceHeaderId)
        {
            return await _repository.GetByInvoiceHeaderIdAsync(invoiceHeaderId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceLineItem>> Get(int id)
        {
            var invoiceLineItem = await _repository.GetByIdAsync(id);
            if (invoiceLineItem == null)
            {
                return NotFound();
            }
            return invoiceLineItem;
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceLineItem>> Post(InvoiceLineItem invoiceLineItem)
        {
            await _repository.AddAsync(invoiceLineItem);
            return CreatedAtAction(nameof(Get), new { id = invoiceLineItem.InvoiceLineItemId }, invoiceLineItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, InvoiceLineItem invoiceLineItem)
        {
            if (id != invoiceLineItem.InvoiceLineItemId)
            {
                return BadRequest();
            }
            await _repository.UpdateAsync(invoiceLineItem);
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
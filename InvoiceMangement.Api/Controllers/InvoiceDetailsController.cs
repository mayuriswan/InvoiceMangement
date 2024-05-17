using InvoiceMangement.Api.Models;
using InvoiceMangement.Api.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceMangement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailsController : ControllerBase
    {
        private readonly IInvoiceDetailsRepository _repository;

        public InvoiceDetailsController(IInvoiceDetailsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("by-invoice/{invoiceId}")]
        public async Task<IEnumerable<InvoiceDetails>> GetByInvoiceId(int invoiceId)
        {
            return await _repository.GetByInvoiceIdAsync(invoiceId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDetails>> Get(int id)
        {
            var invoiceDetails = await _repository.GetByIdAsync(id);
            if (invoiceDetails == null)
            {
                return NotFound();
            }
            return invoiceDetails;
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceDetails>> Post(InvoiceDetails invoiceDetails)
        {
            await _repository.AddAsync(invoiceDetails);
            return CreatedAtAction(nameof(Get), new { id = invoiceDetails.DetailID }, invoiceDetails);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, InvoiceDetails invoiceDetails)
        {
            if (id != invoiceDetails.DetailID)
            {
                return BadRequest();
            }
            await _repository.UpdateAsync(invoiceDetails);
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
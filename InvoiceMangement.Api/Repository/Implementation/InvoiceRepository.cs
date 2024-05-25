namespace InvoiceMangement.Api.Repository.Implementation
{
    using InvoiceMangement.Api.Data;
    using InvoiceMangement.Api.Models;
    using InvoiceMangement.Api.Repository.Interface;
    using Microsoft.Data.SqlClient;
    // Repositories/InvoiceHeaderRepository.cs
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Data;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using System.Text.Json.Serialization;

    using System.Threading.Tasks;

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            var invoices = await _context.Invoices
                .Include(i=>i.Category).Include(i => i.InvoiceDetails).ToListAsync();
            return invoices; 
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _context.Invoices.Include(i=>i.Category).Include(i => i.InvoiceDetails).FirstOrDefaultAsync(i => i.InvoiceID == id);
        }

        public async Task AddAsync(Invoice invoice)
        {
            invoice.Category = null;
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }
        public async Task<Invoice> GetInvoiceAsync(string invoiceNumber, DateTime invoiceDate)
        {
            var parameterInvoiceNumber = new SqlParameter("@InvoiceNumber", SqlDbType.NVarChar, 50) { Value = invoiceNumber ?? (object)DBNull.Value };
            var parameterInvoiceDate = new SqlParameter("@InvoiceDate", SqlDbType.Date) { Value = invoiceDate == default ? (object)DBNull.Value : invoiceDate };

            var commandText = "EXEC [dbo].[GetInvoice] @InvoiceNumber, @InvoiceDate";

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = commandText;
                command.Parameters.Add(parameterInvoiceNumber);
                command.Parameters.Add(parameterInvoiceDate);

                _context.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    if (await result.ReadAsync())
                    {
                        var jsonResult = result.GetString(0);

                        // Print the JSON result for inspection
                        Console.WriteLine(jsonResult);

                        // Parse the JSON result using JsonNode for manual processing
                        var jsonObject = JsonNode.Parse(jsonResult);
                        var invoiceArray = jsonObject?["Invoice"]?.AsArray();
                        if (invoiceArray == null || !invoiceArray.Any())
                        {
                            return null;
                        }

                        // Deserialize the first element of the array to Invoice
                        var invoiceJson = invoiceArray[0].ToJsonString();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var invoice = JsonSerializer.Deserialize<Invoice>(invoiceJson, options);
                        return invoice;
                    }
                }
            }

            return null;
        }


        public async Task UpdateAsync(Invoice invoice)
        {
            var existingInvoice = await _context.Invoices
                                                 .Include(i => i.Category)
                                                 .Include(i => i.InvoiceDetails)
                                                 .FirstOrDefaultAsync(i => i.InvoiceID == invoice.InvoiceID);

            if (existingInvoice == null)
            {
                throw new Exception("Invoice not found");
            }

            // Update the invoice fields
            _context.Entry(existingInvoice).CurrentValues.SetValues(invoice);

            // Update the category if it has changed
            if (existingInvoice.CategoryID != invoice.CategoryID)
            {
                existingInvoice.CategoryID = invoice.CategoryID;
                var newCategory = await _context.Categories.FindAsync(invoice.CategoryID);
                if (newCategory != null)
                {
                    existingInvoice.Category = newCategory;
                }
            }

            // Handle line items
            foreach (var existingDetail in existingInvoice.InvoiceDetails.ToList())
            {
                if (!invoice.InvoiceDetails.Any(d => d.DetailID == existingDetail.DetailID))
                {
                    _context.InvoiceDetails.Remove(existingDetail);
                }
            }

            foreach (var detail in invoice.InvoiceDetails)
            {
                var existingDetail = existingInvoice.InvoiceDetails
                                                    .FirstOrDefault(d => d.DetailID == detail.DetailID);

                if (existingDetail != null)
                {
                    _context.Entry(existingDetail).CurrentValues.SetValues(detail);
                }
                else
                {
                    existingInvoice.InvoiceDetails.Add(detail);
                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task SaveInvoiceAsync(Invoice invoice)
        {
            var invoiceJson = JsonSerializer.Serialize(new { Invoices = invoice });

            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SaveInvoice";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@InvoiceJson", SqlDbType.NVarChar) { Value = invoiceJson });

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }
    }
}

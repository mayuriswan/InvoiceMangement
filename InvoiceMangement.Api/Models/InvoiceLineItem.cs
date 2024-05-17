namespace InvoiceMangement.Api.Models
{
    public class InvoiceLineItem
    {
        public int InvoiceLineItemId { get; set; }
        public int InvoiceHeaderId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation property
        public InvoiceHeader InvoiceHeader { get; set; }
    }
}

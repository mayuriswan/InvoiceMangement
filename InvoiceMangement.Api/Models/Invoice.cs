namespace InvoiceMangement.Api.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation property
        public ICollection<InvoiceDetails> InvoiceDetails { get; set; } = new List<InvoiceDetails>();
    }
}

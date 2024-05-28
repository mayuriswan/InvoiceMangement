namespace InvoiceMangement.Shared.Models{
    public class InvoiceDetails
    {
        public int DetailID { get; set; }
        public int InvoiceID { get; set; }
        public string ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }

   
    }
}

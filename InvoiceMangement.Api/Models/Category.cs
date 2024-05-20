using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InvoiceMangement.Api.Models
{
    [Table("Category", Schema = "dbo")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        [Column("CategoryCode", TypeName = "nchar(10)")]
        public string CategoryCode { get; set; }

        [Column("CategoryDescription", TypeName = "nvarchar(50)")]
        public string CategoryDescription { get; set; }
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    }

}

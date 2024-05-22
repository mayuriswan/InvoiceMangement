using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace InvoiceMangement.Api.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        
        public string CategoryCode { get; set; }

        public string CategoryDescription { get; set; }

    }

}

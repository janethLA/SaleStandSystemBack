using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySalesStandSystem.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Product")]
    public class Product
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string productName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string description { get; set; }

        public double price { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string measurement { get; set; }

        public double quantity { get; set; }

        public byte[]? image { get; set; }
        //public String image { get; set; }

        public int SalesStandId { get; set; }

        public SalesStand? SalesStand { get; set; }
    }
}

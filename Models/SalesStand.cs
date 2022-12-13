using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySalesStandSystem.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("SalesStand")]
    public class SalesStand
    {
        [Key]
        public int id { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string salesStandName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string address { get; set; }
        [Column(TypeName = "varchar(50)")] 
        public string longitude { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string latitude { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string description { get; set; }
        public byte[]? image { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public IEnumerable<Product>? products { get; set; }
    }
}

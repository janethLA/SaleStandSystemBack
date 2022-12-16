using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySalesStandSystem.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("User")]
    public class User
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string username { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string password { get; set; }

        [Column(TypeName = "varchar(100)")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage = "Incorrect format")]
        public string email { get; set; }

        public DateTime registrationDate { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string rol { get; set; }

        public bool active { get; set; }

        public IEnumerable<SalesStand>? salesStands { get; set; }
    }
}

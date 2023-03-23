using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string LoginName { get; set; }
      
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public int Gender { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}

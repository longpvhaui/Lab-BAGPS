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
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string LoginName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}

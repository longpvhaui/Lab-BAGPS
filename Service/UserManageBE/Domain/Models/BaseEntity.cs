using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseEntity
    {

        public DateTime CreatedDate { get; set; }

        public Boolean IsDelete { get; set; } = false;

        public DateTime? DeletedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ResponseUser
    {
        public List<User> Users { get; set; }
        public int TotalItems { get; set; }
        public double TotalPages { get; set; }
    }
}

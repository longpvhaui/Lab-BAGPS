using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SearchModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? SearchText { get; set; }
        public string? Gender { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}

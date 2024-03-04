using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreClass
{
    public class Cases
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedOnUTC { get; set; }
    }
}

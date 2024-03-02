using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreClass
{
    public class CaseLawyerMapping
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int LawyerId { get; set; }
        public int TotalPayment { get; set; }
    }
}

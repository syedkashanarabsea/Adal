using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreClass
{
    public class MediaGallery
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedOnUTC { get; set; }
    }
}

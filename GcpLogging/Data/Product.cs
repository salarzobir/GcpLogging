using System;

#nullable disable

namespace GcpLogging.Data
{
    public partial class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Category Category { get; set; }
    }
}

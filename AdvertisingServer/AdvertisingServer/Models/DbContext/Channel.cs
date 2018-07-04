using System;

namespace AdvertisingServer.Models.DbContext
{
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal Price { get; set; }
        public bool IsValid { get; set; }
    }
}

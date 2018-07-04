using System;

namespace AdvertisingServer.Models.DbContext
{
    public class Advertising
    {
        public int AdvertisingId { get; set; }
        public string Token { get; set; }
        public string Text { get; set; }
        public byte[] Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

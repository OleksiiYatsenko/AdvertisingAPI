using System;

namespace AdvertisingServer.Models.DbContext
{
    public class Publishing
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int AdvertisingId { get; set; }
        public int ChannelId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Amount { get; set; }
    }
}

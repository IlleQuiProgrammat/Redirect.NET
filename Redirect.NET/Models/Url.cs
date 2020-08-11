using System;

namespace Redirect.NET.Models
{
    public class Url
    {
        public string Id { get; set; }
        public string RedirectUrl { get; set; }
        public int UsageCount { get; set; }
        public DateTime LastUsed { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
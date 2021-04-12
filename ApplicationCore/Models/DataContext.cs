using ApplicationCore.Entities;
using System;

namespace ApplicationCore.Models
{
    public class DataContext
    {
        public User User { get; set; }
        public DateTime EntryTime { get; set; }
        public string IpAddress { get; set; }
    }
}

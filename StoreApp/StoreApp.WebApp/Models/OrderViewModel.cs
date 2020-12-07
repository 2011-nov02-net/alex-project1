using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StoreApp.Library;

namespace StoreApp.WebApp.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Store { get; set; }
        public string Customer { get; set; }
        public DateTime Time { get; set; }
        public decimal Total { get; set; }
        public Dictionary<string, int> Products { get; set; }
        public Dictionary<int, int> ProductIds { get; set; }
    }
}

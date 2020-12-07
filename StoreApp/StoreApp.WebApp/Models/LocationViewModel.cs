using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class LocationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<int, int> Inventory { get; set; }

    }
}

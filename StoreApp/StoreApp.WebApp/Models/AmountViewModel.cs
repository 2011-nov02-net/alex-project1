using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.WebApp.Models
{
    public class AmountViewModel
    {

        public int StoreId { get; set; }
        public int ProducId { get; set; }

        [Range(1,int.MaxValue, ErrorMessage ="Please use only positive Integers")]
        public int Amount { get; set; }
    }
}

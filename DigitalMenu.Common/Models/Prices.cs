using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalMenu.Common.Models
{
   public class Prices
    {
        [Required]
        public string Size { get; set; }
        [Required]
        public int Price { get; set; }
    }
}

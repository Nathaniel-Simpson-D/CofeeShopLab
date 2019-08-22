using System;
using System.Collections.Generic;

namespace CoffeeShopLab2.Models
{
    public partial class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int? Quanity { get; set; }
    }
}

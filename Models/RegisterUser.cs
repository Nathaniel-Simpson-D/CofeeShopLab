using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopLab2.Models
{
    public partial class RegisterUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        //[PasswordValadation]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Range(typeof(string),"true","true")]
        public string IsNotBot { get; set; }
        [Range(18,80)]
        public int? Age { get; set; }
        
        public float? Funds { get; set; }
    }
}

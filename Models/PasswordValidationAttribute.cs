using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopLab2.Models
{
    public class PasswordValadationAttribute : ValidationAttribute
    {

        public class PasswordValidationAttribute : ValidationAttribute
        {

            public override bool IsValid(object value)
            {
                //O(2n) meaning  (2 = number of loops not imbeded in another loop)*(n = length of incomming string)
                string password = "";
                try
                {
                    //IF value cannot become a string (bc IsNull) catches exeption and returns false with an error message
                    password = value.ToString();
                }
                catch
                {
                    this.ErrorMessage = "Password Must be 8 or More Charecters";
                    return false;
                }
                // checks to make sure it has at LEAST 8 Char, if not returns false
                if (password.Length < 8)
                {
                    this.ErrorMessage = "Password Must be 8 or More Charecters";
                    return false;
                }
                //checks if it has 1+ Upeer case letters IF it is 8+ char
                bool isValid = false;
                foreach (char letter in password)
                {
                    if (char.IsUpper(letter))
                    {
                        isValid = true;
                        break;
                    }
                    this.ErrorMessage = "Password Must Have 1 or More Upper Case Charecter(s)";
                }
                //IF it is 8+ char and has 1+ UC char THEN checks if it has a number, if so it returns true
                if (isValid)
                {
                    this.ErrorMessage = "Password Must Have 1 or More Numeric Charecter(s)";
                    foreach (char letter in password)
                    {
                        if (char.IsNumber(letter))
                        {
                            return true;
                        }
                    }
                }
                //IF is 8+ Char but does not have UC char AND a number THEN returns false
                return false;
            }
        }
    }
}

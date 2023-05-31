using AutoMapper;
using DataLayer.DataServiceInterfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace WebServer.Services
{
    public class Authentication
    {         

        public string ValidatePlayername(string playername)
        {      
            /*
             * Validation conditions:
             * [a-zA-Z] => first character must be a letter
             * [a-zA-Z0-9] => playername can contain letter and number, 
             * and be both upper and lowercase
             * {7,11} => the length is between 6 to 12
             */

            string message = "Playername must start with a letter, and be 6-20 characters in lenght";

            string pattern = @"^[a-zA-Z][a-zA-Z0-9]{5,19}$";
            
            
            Regex regex = new Regex(pattern);
            var result = regex.IsMatch(playername);

            if (result)
            {
                return null!;
            }
            else
            {
                return message;
            }         
                  
        }


        public string ValidatePassword(string password)
        {
            /*
             * Validation conditions:
             * Min 8 char and max 14 char
             * One upper case
             * One special char
             * One lower case
             * One numerical
             * No white space
             */

            string message = null!;

            //Min 8 char and max 14 char            
            if (password.Length < 8 || password.Length > 14)
            {
                message = "Password must have a lenght of 8-14 characters.";
                return message;
            }
                

            //One upper case            
            if (!password.Any(char.IsUpper))
            {
                message = "Password must contain one uppercase letter";
                return message;
            }       

            //One lower case            
            if (!password.Any(char.IsLower))
            {
                message = "Password must contain one lowercase letter";
                return message;
            }

            //One special char
            bool IsValid = false;
            string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialChArray = specialCh.ToCharArray();
            foreach (char ch in specialChArray)
            {
                if (password.Contains(ch)) { IsValid = true; }
            }

            if (!IsValid)
            {
                message = "Password must contain minimum one special character";
                return message;
            }

            //one numerical
            if (!password.Any(char.IsNumber))
            {
                message = "Password must contain one number";
                return message;
            }          


            //No white space            
            if (password.Contains(" "))
            {
                message = "Password must not contain spaces";
                return message;
            }          
                

            return message;

        }
    }
}

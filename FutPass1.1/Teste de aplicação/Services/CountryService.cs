using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teste_de_aplicação.Models;

namespace Teste_de_aplicação.Services
{
    public class CountryService
    {
        public static string GetFlag(Country country) {
            
            
                if (country.Flag == null)
                {
                    return null;
                }
            
                var base64 = Convert.ToBase64String(country.Flag);
                return String.Format("data:image/gif;base64,{0}", base64);
            }
        
        } 


    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teste_de_aplicação.Models;

namespace Teste_de_aplicação.Services
{
    public class ManagersService
    {
        public static int GetAge(Manager manager)
        {
            DateTime birth;

            try
            {
                DateTime.TryParse(manager.DateOfBirth, out birth);
            }
            catch (Exception)
            {
                return 0;
            }

            return DateTime.Now.Year - birth.Year;
        }
        
        public static string GetPhoto(Manager manager)
        {
            if (manager.Photo == null)
            {
                return null;
            }
            var base64 = Convert.ToBase64String(manager.Photo);
            return String.Format("data:image/gif;base64,{0}", base64);
        }
            
    }
}
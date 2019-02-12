using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teste_de_aplicação.Models;

namespace Teste_de_aplicação.Services
{
    public class PlayerService
    {
        public static int GetAge(Player player)
        {
            DateTime birth;

            try
            {
                DateTime.TryParse(player.DateOfBirth, out birth);
            }
            catch (Exception)
            {
                return 0;
            }

            return DateTime.Now.Year - birth.Year;
        }

        public static string GetPhoto(Player player)
        {
            if (player.Photo == null)
            {
                return null;
            }
            var base64 = Convert.ToBase64String(player.Photo);
            return String.Format("data:image/gif;base64,{0}", base64);
        }
    }
}
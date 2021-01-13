using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp
{
   public class Administrator
    {
        public string UserName { get; set; }

        public int Pin { get; set; }

        public Administrator(string userName, int pin)
        {
            UserName = userName;
            Pin = pin;
        }

    }
}

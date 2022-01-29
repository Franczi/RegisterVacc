using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.Services
{
    public class VaccStorage
    {
        private static int VaccAmount = 30;


        public static int getVacAmount()
        {
            return VaccAmount;
        }
        
        public static void reduceVaccAmount()
        {
            if(VaccAmount>0)
                VaccAmount--;
        }

    }
}

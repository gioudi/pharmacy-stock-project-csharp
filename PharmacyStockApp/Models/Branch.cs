using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacyStockApp.Models
{
    public enum Branch
    {
        Primaria, 
        Secuandaria
    }
    public static class BranchExtensions
    {
        public static string GetAddress(this Branch branch)
        {
            return branch switch
            {
                Branch.Primaria => "Calle de la Rosa n.28",
                Branch.Secuandaria => "Calle Alcazabilla n. 3",
                _ => "Dirección no encontraron"
            };
        }
    }
}

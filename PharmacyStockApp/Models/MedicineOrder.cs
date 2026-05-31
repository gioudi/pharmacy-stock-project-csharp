using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacyStockApp.Models
{
    public class MedicineOrder
    {
        public string MedicineName { get; set; }
        public MedicineType Type { get; set; }
        public int Quantity { get; set; }
        public Provider ProviderCompany { get; set; }
        public Branch  TargetBranch { get; set; }



        public MedicineOrder(string medicineName, MedicineType type, int quantity, Provider providerCompany, Branch targetBranch) { 
        
            MedicineName = medicineName;
            Type = type;
            Quantity = quantity;
            ProviderCompany = providerCompany;
            TargetBranch = targetBranch;
        }

    }
}

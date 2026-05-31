using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacyStockApp.Models
{

    public enum MedicineType
    {
        Analgesico,
        Analeptico,
        Anestesico,
        Antiacido,
        Antidepresivo,
        Antibiotico
    }

    public static class MedicineTypeExtensions
    {
        public static string ToDisplayName(this MedicineType type)
        {
            return type switch
            {
                MedicineType.Analgesico => "Analgésico",
                MedicineType.Analeptico => "Analéptico",
                MedicineType.Anestesico => "Anestésico",
                MedicineType.Antiacido => "Antiácido",
                MedicineType.Antidepresivo => "Antidepresivo",
                MedicineType.Antibiotico => "Antibiótico",
                _ => type.ToString()
            };
        }
    }
}
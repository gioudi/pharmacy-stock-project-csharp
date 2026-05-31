using PharmacyStockApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacyStockApp.Views
{
    public interface IMainFormView
    {
        string MedicineName { get; set; }
        MedicineType SelectedType { get; set; }
        string QuantityInput { get; set; }
        Provider? SelectedProvider { get; set; }
        bool IsPrimaryBranchChecked { get; set; }
        bool IsSecondaryBranchChecked { get; set; }

        event EventHandler ConfirmClicked;
        event EventHandler DeleteClicked;

        void ClearFields();
        void ShowWarning(string message);
        void HideView();
        void ShowView();
    }
}

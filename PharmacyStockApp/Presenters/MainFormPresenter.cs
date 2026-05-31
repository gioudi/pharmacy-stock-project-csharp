using System;
using System.Text;
using System.Text.RegularExpressions;
using PharmacyStockApp.Models;
using PharmacyStockApp.Views;

namespace PharmacyStockApp.Presenters
{
    public class MainFormPresenter
    {
        
        private readonly IMainFormView _view;

        public MainFormPresenter(IMainFormView view)
        {
            _view = view;

       
            _view.ConfirmClicked += OnConfirmClicked;
            _view.DeleteClicked += OnDeleteClicked;
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
           
            _view.ClearFields();
        }

        
        private void OnConfirmClicked(object sender, EventArgs e)
        {
            StringBuilder errorBuilder = new StringBuilder();

            // 1. Medicine Name Validation (Alphanumeric verification)
            if (string.IsNullOrWhiteSpace(_view.MedicineName))
            {
                errorBuilder.AppendLine("- Medicine Name cannot be empty.");
            }
            else if (!Regex.IsMatch(_view.MedicineName, @"^[a-zA-Z0-9 ]+$"))
            {
                errorBuilder.AppendLine("- Medicine Name must be alphanumeric (letters and numbers only).");
            }

            // 2. Quantity Validation (Parsing to positive integer)
            int parsedQuantity = -1;
            if (string.IsNullOrWhiteSpace(_view.QuantityInput))
            {
                errorBuilder.AppendLine("- Quantity / Amount cannot be empty.");
            }
            else if (!int.TryParse(_view.QuantityInput, out parsedQuantity) || parsedQuantity <= 0)
            {
                errorBuilder.AppendLine("- Quantity must be a valid positive integer greater than zero.");
            }

            // 3. Provider Validation (Null evaluation)
            if (_view.SelectedProvider == null)
            {
                errorBuilder.AppendLine("- You must select a provider company (radio button).");
            }

            // 4. Branch Validation (Exclusive single-selection processing)
            Branch? targetBranch = null;
            if (!_view.IsPrimaryBranchChecked && !_view.IsSecondaryBranchChecked)
            {
                errorBuilder.AppendLine("- You must choose at least one branch.");
            }
            else if (_view.IsPrimaryBranchChecked && _view.IsSecondaryBranchChecked)
            {
                errorBuilder.AppendLine("- Please choose ONLY one branch per order.");
            }
            else
            {
                targetBranch = _view.IsPrimaryBranchChecked ? Branch.Primaria : Branch.Secundaria;
            }

            // --- EVALUATING ACCUMULATED RUNTIME ERRORS ---
            if (errorBuilder.Length > 0)
            {
                
                _view.ShowWarning(errorBuilder.ToString());
            }
            else
            {
                
                MedicineOrder confirmedOrder = new MedicineOrder(
                    _view.MedicineName,
                    _view.SelectedType,
                    parsedQuantity,
                    _view.SelectedProvider.Value,
                    targetBranch.Value
                );

                ProcessValidOrder(confirmedOrder);
            }
        }

        private void ProcessValidOrder(MedicineOrder order)
        {
         
            _view.HideView();

            SummaryForm summaryView = new SummaryForm(order);

            
            summaryView.CancelClicked += (sender, e) =>
            {

                summaryView.CloseView();

                _view.ShowView();
            };

          
            summaryView.SendClicked += (sender, e) =>
            {
               
                System.Windows.Forms.MessageBox.Show(
                    "Pedido enviado.",
                    "Order Dispatched",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);


                summaryView.CloseView();

                System.Windows.Forms.Application.Exit();
            };

            summaryView.Show();
        }
    }
}

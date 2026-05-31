using System;
using System.Drawing;
using System.Windows.Forms;
using PharmacyStockApp.Models;

namespace PharmacyStockApp.Views
{
    public partial class SummaryForm : Form
    {

        private Label lblHeader;
        private Label lblOrderDetails;
        private Label lblAddress;
        private Button btnCancel;
        private Button btnSend;

        
        public event EventHandler CancelClicked;
        public event EventHandler SendClicked;

    
        public SummaryForm(MedicineOrder order)
        {
            InitializeSummaryLayout();
            PopulateOrderData(order);
        }

        private void InitializeSummaryLayout()
        {
            this.Text = "Order Summary";
            this.Size = new Size(520, 320);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

        
            FlowLayoutPanel containerPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(20),
                WrapContents = false
            };

            // 1. Header Label: Displays the selected company provider D
            lblHeader = new Label
            {
                Width = 460,
                Height = 35,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkSlateGray,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };

            // 2. Order Details Label: Displays dynamic information in specific required string format
            lblOrderDetails = new Label
            {
                Width = 460,
                Height = 45,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };

            // 3. Address Label: Displays the calculated physical address based on selected branch
            lblAddress = new Label
            {
                Width = 460,
                Height = 35,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.RoyalBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };

            // 4. CTA Buttons Container Panel
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Width = 460,
                Height = 60,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 15, 0, 0)
            };

            btnCancel = new Button { Text = "Cancel Order", Width = 140, Height = 35, Margin = new Padding(50, 0, 20, 0) };
            btnSend = new Button { Text = "Send Order", Width = 140, Height = 35 };

           
            btnCancel.Click += (sender, e) => CancelClicked?.Invoke(this, EventArgs.Empty);
            btnSend.Click += (sender, e) => SendClicked?.Invoke(this, EventArgs.Empty);

         
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnSend);

            containerPanel.Controls.Add(lblHeader);
            containerPanel.Controls.Add(lblOrderDetails);
            containerPanel.Controls.Add(lblAddress);
            containerPanel.Controls.Add(buttonPanel);

            this.Controls.Add(containerPanel);
        }

        private void PopulateOrderData(MedicineOrder order)
        {
         
            lblHeader.Text = $"Summary of the 'shopping cart' Order to provider {order.ProviderCompany}";

            
            lblOrderDetails.Text = $"Text of medicine requested: {order.Quantity} units of {order.Type.ToDisplayName()} ({order.MedicineName})";


            lblAddress.Text = $"Delivery Address: {order.TargetBranch.GetAddress()}";
        }

        public void CloseView() => this.Dispose();
    }
}

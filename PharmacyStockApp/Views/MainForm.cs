using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using PharmacyStockApp.Models;

namespace PharmacyStockApp.Views
{
    public partial class MainForm : Form, IMainFormView
    {
       
        private TextBox txtMedicineName;
        private ComboBox cmbMedicineType;
        private TextBox txtQuantity;
        private RadioButton rdoCofara;
        private RadioButton rdoEmpsephar;
        private RadioButton rdoCemefar;
        private CheckBox chkPrimaria;
        private CheckBox chkSecundaria;
        private Button btnDelete;
        private Button btnConfirm;

 
        public event EventHandler ConfirmClicked;
        public event EventHandler DeleteClicked;

        public MainForm()
        {
            InitializeFormLayout();
            PopulateMedicineTypes();
        }

        private void InitializeFormLayout()
        {

            this.Text = "Pharmacy Stock - Order Request";
            this.Size = new Size(480, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

          
            TableLayoutPanel layoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(20),
            };
            layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

            // Row 0: Medicine Name
            layoutPanel.Controls.Add(new Label { Text = "Medicine Name:", Anchor = AnchorStyles.Left }, 0, 0);
            txtMedicineName = new TextBox { Anchor = AnchorStyles.Left, Width = 220 };
            layoutPanel.Controls.Add(txtMedicineName, 1, 0);

            // Row 1: Medicine Type
            layoutPanel.Controls.Add(new Label { Text = "Type of Medicine:", Anchor = AnchorStyles.Left }, 0, 1);
            cmbMedicineType = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
            layoutPanel.Controls.Add(cmbMedicineType, 1, 1);

            // Row 2: Quantity
            layoutPanel.Controls.Add(new Label { Text = "Quantity / Amount:", Anchor = AnchorStyles.Left }, 0, 2);
            txtQuantity = new TextBox { Anchor = AnchorStyles.Left, Width = 220 };
            layoutPanel.Controls.Add(txtQuantity, 1, 2);

            // Row 3: Provider Radio Buttons 
            layoutPanel.Controls.Add(new Label { Text = "Provider Company:", Anchor = AnchorStyles.Left }, 0, 3);
            FlowLayoutPanel providerPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };
            rdoCofara = new RadioButton { Text = "Cofara", AutoSize = true };
            rdoEmpsephar = new RadioButton { Text = "Empsephar", AutoSize = true };
            rdoCemefar = new RadioButton { Text = "Cemefar", AutoSize = true };
            providerPanel.Controls.AddRange(new Control[] { rdoCofara, rdoEmpsephar, rdoCemefar });
            layoutPanel.Controls.Add(providerPanel, 1, 3);

            // Row 4: Branch Checkboxes
            layoutPanel.Controls.Add(new Label { Text = "Branch:", Anchor = AnchorStyles.Left }, 0, 4);
            FlowLayoutPanel branchPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };
            chkPrimaria = new CheckBox { Text = "Primaria", AutoSize = true };
            chkSecundaria = new CheckBox { Text = "Secundaria", AutoSize = true };
            branchPanel.Controls.AddRange(new Control[] { chkPrimaria, chkSecundaria });
            layoutPanel.Controls.Add(branchPanel, 1, 4);

            // Row 5: Action CTA Buttons
            btnDelete = new Button { Text = "Delete", Width = 100, Height = 35 };
            btnConfirm = new Button { Text = "Confirm", Width = 100, Height = 35 };

            // Wire up internal control events to bubble up to our Interface Event handlers
            btnConfirm.Click += (sender, e) => ConfirmClicked?.Invoke(this, EventArgs.Empty);
            btnDelete.Click += (sender, e) => DeleteClicked?.Invoke(this, EventArgs.Empty);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
            buttonPanel.Controls.AddRange(new Control[] { btnConfirm, btnDelete });
            layoutPanel.Controls.Add(buttonPanel, 1, 5);

            this.Controls.Add(layoutPanel);
        }

        private void PopulateMedicineTypes()
        {
            // Binding values from our Step 2 MedicineType Enum
            cmbMedicineType.DataSource = Enum.GetValues(typeof(MedicineType));
            cmbMedicineType.FormattingEnabled = true;

            // Format display strings using our Extension Method safely
            cmbMedicineType.Format += (sender, e) =>
            {
                if (e.Value is MedicineType type)
                {
                    e.Value = type.ToDisplayName();
                }
            };
        }

        // --- IMainFormView Property Implementations ---

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MedicineName
        {
            get => txtMedicineName.Text.Trim();
            set => txtMedicineName.Text = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MedicineType SelectedType
        {
            get => (MedicineType)cmbMedicineType.SelectedItem;
            set => cmbMedicineType.SelectedItem = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string QuantityInput
        {
            get => txtQuantity.Text.Trim();
            set => txtQuantity.Text = value;
        }


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Provider? SelectedProvider
        {
            get
            {
                if (rdoCofara.Checked) return Provider.Cofara;
                if (rdoEmpsephar.Checked) return Provider.Empsephar;
                if (rdoCemefar.Checked) return Provider.Cemefar;
                return null;
            }
            set
            {
                if (value == null)
                {
                    rdoCofara.Checked = false;
                    rdoEmpsephar.Checked = false;
                    rdoCemefar.Checked = false;
                }
                else
                {
                    switch (value)
                    {
                        case Provider.Cofara: rdoCofara.Checked = true; break;
                        case Provider.Empsephar: rdoEmpsephar.Checked = true; break;
                        case Provider.Cemefar: rdoCemefar.Checked = true; break;
                    }
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPrimaryBranchChecked { get => chkPrimaria.Checked; set => chkPrimaria.Checked = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSecondaryBranchChecked { get => chkSecundaria.Checked; set => chkSecundaria.Checked = value; }

        // --- IMainFormView Method Implementations ---
        public void ClearFields()
        {
            MedicineName = string.Empty;
            cmbMedicineType.SelectedIndex = 0;
            QuantityInput = string.Empty;
            SelectedProvider = null;
            IsPrimaryBranchChecked = false;
            IsSecondaryBranchChecked = false;
        }

        public void ShowWarning(string message)
        {
            MessageBox.Show(message, "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void HideView() => this.Hide();
        public void ShowView() => this.Show();
    }
}

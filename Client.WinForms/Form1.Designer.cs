namespace Client.WinForms
{
    partial class Form1
    {
        /// <summary> Required designer variable. </summary>
        private System.ComponentModel.IContainer components = null;

        // Controls referenced by code-behind (names must match)
        private System.Windows.Forms.DataGridView gridItems;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.NumericUpDown numUnitPrice;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Button btnIssue;

        // Columns (so we can bind by property name safely)
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;

        /// <summary> Clean up resources. </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.Text = "Inventory (Client.WinForms)";
            this.Width = 1100;
            this.Height = 700;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // Search
            txtSearch = new System.Windows.Forms.TextBox
            {
                Left = 12,
                Top = 12,
                Width = 320,
                PlaceholderText = "Search by name/brand..."
            };
            txtSearch.TextChanged += txtSearch_TextChanged;

            btnRefresh = new System.Windows.Forms.Button
            {
                Left = txtSearch.Right + 8,
                Top = 10,
                Width = 90,
                Height = 28,
                Text = "Refresh"
            };
            btnRefresh.Click += btnRefresh_Click;

            // Grid
            gridItems = new System.Windows.Forms.DataGridView
            {
                Left = 12,
                Top = 48,
                Width = 1050,
                Height = 360,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            };
            gridItems.SelectionChanged += gridItems_SelectionChanged;

            // Define columns
            colId = new System.Windows.Forms.DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Models.Item.Id),
                Name = nameof(Models.Item.Id),
                Visible = false
            };
            colCode = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Code", Name = "Code", HeaderText = "Code" };
            colBrand = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Brand", Name = "Brand", HeaderText = "Brand" };
            colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Category", Name = "Category", HeaderText = "Category" };
            colSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Supplier", Name = "Supplier", HeaderText = "Supplier" };
            colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", Name = "UnitPrice", HeaderText = "Unit Price" };
            colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Quantity", Name = "Quantity", HeaderText = "Qty" };

            gridItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
            {
                colId, colCode, colBrand, colCategory, colSupplier, colUnitPrice, colQuantity
            });

            // Form fields panel (simple layout)
            int left = 12, top = gridItems.Bottom + 16, labelW = 90, fieldW = 220, rowH = 28, gap = 8;

            // Code (maps to Item.Name)
            var lblCode = new System.Windows.Forms.Label { Left = left, Top = top + 4, Width = labelW, Text = "Code:" };
            txtCode = new System.Windows.Forms.TextBox { Left = lblCode.Right + 6, Top = top, Width = fieldW };

            // Name
            var lblName = new System.Windows.Forms.Label { Left = txtCode.Right + 24, Top = top + 4, Width = labelW, Text = "Name:" };
            txtName = new System.Windows.Forms.TextBox { Left = lblName.Right + 6, Top = top, Width = fieldW };

            // Brand
            top += rowH + gap;
            var lblBrand = new System.Windows.Forms.Label { Left = left, Top = top + 4, Width = labelW, Text = "Brand:" };
            txtBrand = new System.Windows.Forms.TextBox { Left = lblBrand.Right + 6, Top = top, Width = fieldW };

            // Unit price
            var lblPrice = new System.Windows.Forms.Label { Left = txtBrand.Right + 24, Top = top + 4, Width = labelW, Text = "Unit Price:" };
            numUnitPrice = new System.Windows.Forms.NumericUpDown
            {
                Left = lblPrice.Right + 6,
                Top = top,
                Width = fieldW,
                DecimalPlaces = 2,
                Maximum = 1000000,
                Minimum = 0
            };

            // Category
            top += rowH + gap;
            var lblCat = new System.Windows.Forms.Label { Left = left, Top = top + 4, Width = labelW, Text = "Category:" };
            cboCategory = new System.Windows.Forms.ComboBox { Left = lblCat.Right + 6, Top = top, Width = fieldW, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };

            // Supplier
            var lblSup = new System.Windows.Forms.Label { Left = cboCategory.Right + 24, Top = top + 4, Width = labelW, Text = "Supplier:" };
            cboSupplier = new System.Windows.Forms.ComboBox { Left = lblSup.Right + 6, Top = top, Width = fieldW, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };

            // Buttons
            top += rowH + 14;
            btnAdd = new System.Windows.Forms.Button { Left = left, Top = top, Width = 90, Height = 30, Text = "Add" };
            btnUpdate = new System.Windows.Forms.Button { Left = btnAdd.Right + 8, Top = top, Width = 90, Height = 30, Text = "Update" };
            btnDelete = new System.Windows.Forms.Button { Left = btnUpdate.Right + 8, Top = top, Width = 90, Height = 30, Text = "Delete" };
            btnReceive = new System.Windows.Forms.Button { Left = btnDelete.Right + 24, Top = top, Width = 110, Height = 30, Text = "Receive (In)" };
            btnIssue = new System.Windows.Forms.Button { Left = btnReceive.Right + 8, Top = top, Width = 110, Height = 30, Text = "Issue (Out)" };

            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnReceive.Click += btnReceive_Click;
            btnIssue.Click += btnIssue_Click;

            // Add to form
            this.Controls.Add(txtSearch);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(gridItems);
            this.Controls.Add(lblCode);
            this.Controls.Add(txtCode);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblBrand);
            this.Controls.Add(txtBrand);
            this.Controls.Add(lblPrice);
            this.Controls.Add(numUnitPrice);
            this.Controls.Add(lblCat);
            this.Controls.Add(cboCategory);
            this.Controls.Add(lblSup);
            this.Controls.Add(cboSupplier);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnReceive);
            this.Controls.Add(btnIssue);

            this.Load += Form1_Load;
        }

        #endregion
    }
}

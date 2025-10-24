using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.WinForms.Models;
using Client.WinForms.Services;

namespace Client.WinForms
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _http = new HttpClient();
        private ItemApi _itemApi;
        private CategoryApi _categoryApi;
        private SupplierApi _supplierApi;
        private StockMovementApi _movementApi;

        private List<Item> _items = new();
        private List<Category> _categories = new();
        private List<Supplier> _suppliers = new();

        private TabControl tabs = null!;
        private TabPage tabItems = null!, tabCategories = null!, tabSuppliers = null!, tabStock = null!;

        private TextBox txtName = null!, txtCode = null!, txtBrand = null!, txtSearch = null!;
        private NumericUpDown numUnitPrice = null!, numQty = null!;
        private ComboBox cmbCategory = null!, cmbSupplier = null!;
        private Button btnAdd = null!, btnUpdate = null!, btnDelete = null!;
        private DataGridView gridItems = null!;

        private TextBox txtCatName = null!;
        private Button btnCatAdd = null!, btnCatUpdate = null!, btnCatDelete = null!;
        private DataGridView gridCategories = null!;

        private TextBox txtSupName = null!, txtSupContact = null!;
        private Button btnSupAdd = null!, btnSupUpdate = null!, btnSupDelete = null!;
        private DataGridView gridSuppliers = null!;

        private ComboBox cmbStkItem = null!, cmbStkType = null!;
        private NumericUpDown numStkQty = null!;
        private TextBox txtStkRemarks = null!;
        private Button btnStkApply = null!;
        private DataGridView gridMovements = null!;

        public Form1()
        {
            InitializeComponent();
            _itemApi = new ItemApi(_http);
            _categoryApi = new CategoryApi(_http);
            _supplierApi = new SupplierApi(_http);
            _movementApi = new StockMovementApi(_http);

            BuildLayout();
            StyleUi();
            WireEvents();
        }

        private static int? Val(ComboBox cb) => cb.SelectedValue is int v ? v : (int?)null;

        private void BuildLayout()
        {
            tabs = new TabControl { Dock = DockStyle.Fill };
            tabItems = new TabPage("Items");
            tabCategories = new TabPage("Categories");
            tabSuppliers = new TabPage("Suppliers");
            tabStock = new TabPage("Stock");
            tabs.TabPages.AddRange(new[] { tabItems, tabCategories, tabSuppliers, tabStock });
            Controls.Add(tabs);

            BuildItemsTab();
            BuildCategoriesTab();
            BuildSuppliersTab();
            BuildStockTab();
        }

        private void BuildItemsTab()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, AutoSize = true };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var rowInputs = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 8 };
            for (int i = 0; i < 8; i++) rowInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));

            var lblName = new Label { Text = "Name" };
            var lblCode = new Label { Text = "Code" };
            var lblBrand = new Label { Text = "Brand" };
            var lblPrice = new Label { Text = "Unit Price" };
            var lblQty = new Label { Text = "Qty" };
            var lblCat = new Label { Text = "Category" };
            var lblSup = new Label { Text = "Supplier" };

            txtName = new TextBox(); txtCode = new TextBox(); txtBrand = new TextBox();
            numUnitPrice = new NumericUpDown { DecimalPlaces = 2, Maximum = 100000000, Minimum = 0 };
            numQty = new NumericUpDown { Maximum = 100000000, Minimum = 0 };
            cmbCategory = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cmbSupplier = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };

            rowInputs.Controls.Add(lblName, 0, 0);
            rowInputs.Controls.Add(txtName, 1, 0);
            rowInputs.Controls.Add(lblCode, 2, 0);
            rowInputs.Controls.Add(txtCode, 3, 0);
            rowInputs.Controls.Add(lblBrand, 4, 0);
            rowInputs.Controls.Add(txtBrand, 5, 0);
            rowInputs.Controls.Add(lblPrice, 0, 1);
            rowInputs.Controls.Add(numUnitPrice, 1, 1);
            rowInputs.Controls.Add(lblQty, 2, 1);
            rowInputs.Controls.Add(numQty, 3, 1);
            rowInputs.Controls.Add(lblCat, 4, 1);
            rowInputs.Controls.Add(cmbCategory, 5, 1);
            rowInputs.Controls.Add(lblSup, 6, 1);
            rowInputs.Controls.Add(cmbSupplier, 7, 1);

            var rowButtons = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
            btnAdd = new Button { Text = "Add" };
            btnUpdate = new Button { Text = "Update" };
            btnDelete = new Button { Text = "Delete" };
            rowButtons.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete });

            var rowSearch = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 2 };
            rowSearch.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            rowSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            var lblSearch = new Label { Text = "Search" };
            txtSearch = new TextBox();
            rowSearch.Controls.Add(lblSearch, 0, 0);
            rowSearch.Controls.Add(txtSearch, 1, 0);

            gridItems = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            root.Controls.Add(rowInputs, 0, 0);
            root.Controls.Add(rowButtons, 0, 1);
            root.Controls.Add(rowSearch, 0, 2);
            root.Controls.Add(gridItems, 0, 3);
            tabItems.Controls.Add(root);
        }

        private void BuildCategoriesTab()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, AutoSize = true };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var rowInputs = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 2 };
            rowInputs.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            rowInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            var lbl = new Label { Text = "Name" };
            txtCatName = new TextBox();
            rowInputs.Controls.Add(lbl, 0, 0);
            rowInputs.Controls.Add(txtCatName, 1, 0);

            var rowButtons = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
            btnCatAdd = new Button { Text = "Add" };
            btnCatUpdate = new Button { Text = "Update" };
            btnCatDelete = new Button { Text = "Delete" };
            rowButtons.Controls.AddRange(new Control[] { btnCatAdd, btnCatUpdate, btnCatDelete });

            gridCategories = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            root.Controls.Add(rowInputs, 0, 0);
            root.Controls.Add(rowButtons, 0, 1);
            root.Controls.Add(gridCategories, 0, 2);
            tabCategories.Controls.Add(root);
        }

        private void BuildSuppliersTab()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, AutoSize = true };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var rowInputs = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 4 };
            rowInputs.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            rowInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            rowInputs.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            rowInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

            var lblName = new Label { Text = "Name" };
            var lblContact = new Label { Text = "Contact" };
            txtSupName = new TextBox();
            txtSupContact = new TextBox();
            rowInputs.Controls.Add(lblName, 0, 0);
            rowInputs.Controls.Add(txtSupName, 1, 0);
            rowInputs.Controls.Add(lblContact, 2, 0);
            rowInputs.Controls.Add(txtSupContact, 3, 0);

            var rowButtons = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
            btnSupAdd = new Button { Text = "Add" };
            btnSupUpdate = new Button { Text = "Update" };
            btnSupDelete = new Button { Text = "Delete" };
            rowButtons.Controls.AddRange(new Control[] { btnSupAdd, btnSupUpdate, btnSupDelete });

            gridSuppliers = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            root.Controls.Add(rowInputs, 0, 0);
            root.Controls.Add(rowButtons, 0, 1);
            root.Controls.Add(gridSuppliers, 0, 2);
            tabSuppliers.Controls.Add(root);
        }

        private void BuildStockTab()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, AutoSize = true };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var rowInputs = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 8 };
            for (int i = 0; i < 8; i++) rowInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));

            var lblItem = new Label { Text = "Item" };
            var lblType = new Label { Text = "Type" };
            var lblQty = new Label { Text = "Qty" };
            var lblRemarks = new Label { Text = "Remarks" };

            cmbStkItem = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStkType = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, DataSource = Enum.GetValues(typeof(MovementType)) };
            numStkQty = new NumericUpDown { Maximum = 100000000, Minimum = 1 };
            txtStkRemarks = new TextBox();
            btnStkApply = new Button { Text = "Apply" };

            rowInputs.Controls.Add(lblItem, 0, 0);
            rowInputs.Controls.Add(cmbStkItem, 1, 0);
            rowInputs.Controls.Add(lblType, 2, 0);
            rowInputs.Controls.Add(cmbStkType, 3, 0);
            rowInputs.Controls.Add(lblQty, 4, 0);
            rowInputs.Controls.Add(numStkQty, 5, 0);
            rowInputs.Controls.Add(lblRemarks, 0, 1);
            rowInputs.Controls.Add(txtStkRemarks, 1, 1);
            rowInputs.Controls.Add(btnStkApply, 7, 1);

            gridMovements = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            root.Controls.Add(rowInputs, 0, 0);
            root.Controls.Add(gridMovements, 0, 2);
            tabStock.Controls.Add(root);
        }

        private void StyleUi()
        {
            Font = new Font("Segoe UI", 10);
        }

        private void WireEvents()
        {
            Load += async (_, __) => await ReloadAllAsync();

            btnAdd.Click += async (_, __) => await AddItemAsync();
            btnUpdate.Click += async (_, __) => await UpdateItemAsync();
            btnDelete.Click += async (_, __) => await DeleteItemAsync();
            gridItems.SelectionChanged += (_, __) => BindItemSelection();
            txtSearch.TextChanged += (_, __) => ApplyItemFilter();

            btnCatAdd.Click += async (_, __) => await AddCategoryAsync();
            btnCatUpdate.Click += async (_, __) => await UpdateCategoryAsync();
            btnCatDelete.Click += async (_, __) => await DeleteCategoryAsync();
            gridCategories.SelectionChanged += (_, __) => BindCategorySelection();

            btnSupAdd.Click += async (_, __) => await AddSupplierAsync();
            btnSupUpdate.Click += async (_, __) => await UpdateSupplierAsync();
            btnSupDelete.Click += async (_, __) => await DeleteSupplierAsync();
            gridSuppliers.SelectionChanged += (_, __) => BindSupplierSelection();

            btnStkApply.Click += async (_, __) => await ApplyMovementAsync();
        }

        private async Task ReloadAllAsync()
        {
            _categories = await _categoryApi.GetAllAsync() ?? new List<Category>();
            _suppliers = await _supplierApi.GetAllAsync() ?? new List<Supplier>();
            _items = await _itemApi.GetAllAsync() ?? new List<Item>();

            cmbCategory.DataSource = _categories.ToList();
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";

            cmbSupplier.DataSource = _suppliers.ToList();
            cmbSupplier.DisplayMember = "Name";
            cmbSupplier.ValueMember = "Id";

            cmbStkItem.DataSource = _items.ToList();
            cmbStkItem.DisplayMember = "Name";
            cmbStkItem.ValueMember = "Id";

            BindItemsGrid(_items);
            BindCategoriesGrid(_categories);
            BindSuppliersGrid(_suppliers);

            var movements = await _movementApi.GetAllAsync() ?? new List<StockMovement>();
            BindMovementsGrid(movements);
        }

        private void BindItemsGrid(IEnumerable<Item> data)
        {
            gridItems.DataSource = data.Select(x => new
            {
                x.Id,
                x.Name,
                x.Code,
                x.Brand,
                x.UnitPrice,
                x.Quantity,
                Category = _categories.FirstOrDefault(c => c.Id == x.CategoryId)?.Name ?? "",
                Supplier = _suppliers.FirstOrDefault(s => s.Id == x.SupplierId)?.Name ?? ""
            }).ToList();
        }

        private void BindCategoriesGrid(IEnumerable<Category> data)
        {
            gridCategories.DataSource = data.Select(c => new { c.Id, c.Name }).ToList();
        }

        private void BindSuppliersGrid(IEnumerable<Supplier> data)
        {
            gridSuppliers.DataSource = data.Select(s => new { s.Id, s.Name, s.Contact }).ToList();
        }

        private void BindMovementsGrid(IEnumerable<StockMovement> data)
        {
            gridMovements.DataSource = data.Select(m => new
            {
                m.Id,
                Item = _items.FirstOrDefault(i => i.Id == m.ItemId)?.Name ?? "",
                m.Type,
                m.Quantity,
                m.Date,
                m.Remarks
            }).ToList();
        }

        private void BindItemSelection()
        {
            if (gridItems.CurrentRow?.Cells["Id"]?.Value is int id)
            {
                var it = _items.FirstOrDefault(i => i.Id == id);
                if (it == null) return;

                txtName.Text = it.Name;
                txtCode.Text = it.Code;
                txtBrand.Text = it.Brand ?? "";
                numUnitPrice.Value = it.UnitPrice;
                numQty.Value = it.Quantity;
                cmbCategory.SelectedValue = it.CategoryId ?? -1;
                cmbSupplier.SelectedValue = it.SupplierId ?? -1;
            }
        }

        private void BindCategorySelection()
        {
            if (gridCategories.CurrentRow?.Cells["Id"]?.Value is int id)
            {
                var c = _categories.FirstOrDefault(x => x.Id == id);
                if (c == null) return;
                txtCatName.Text = c.Name;
            }
        }

        private void BindSupplierSelection()
        {
            if (gridSuppliers.CurrentRow?.Cells["Id"]?.Value is int id)
            {
                var s = _suppliers.FirstOrDefault(x => x.Id == id);
                if (s == null) return;
                txtSupName.Text = s.Name;
                txtSupContact.Text = s.Contact ?? "";
            }
        }

        private void ApplyItemFilter()
        {
            var s = (txtSearch.Text ?? "").Trim().ToLowerInvariant();
            var q = _items.Where(i =>
                   (i.Name ?? "").ToLowerInvariant().Contains(s)
                || (i.Code ?? "").ToLowerInvariant().Contains(s)
                || (i.Brand ?? "").ToLowerInvariant().Contains(s));
            BindItemsGrid(q);
        }

        private async Task AddItemAsync()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Name and Code are required."); return;
            }

            var e = new Item
            {
                Name = txtName.Text.Trim(),
                Code = txtCode.Text.Trim(),
                Brand = string.IsNullOrWhiteSpace(txtBrand.Text) ? null : txtBrand.Text.Trim(),
                UnitPrice = numUnitPrice.Value,
                Quantity = (int)numQty.Value,
                CategoryId = Val(cmbCategory),
                SupplierId = Val(cmbSupplier)
            };

            try
            {
                await _itemApi.CreateAsync(e);
                await ReloadAllAsync();
                ClearItemInputs();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task UpdateItemAsync()
        {
            if (gridItems.CurrentRow?.Cells["Id"]?.Value is not int id) return;

            var e = new Item
            {
                Name = txtName.Text.Trim(),
                Code = txtCode.Text.Trim(),
                Brand = string.IsNullOrWhiteSpace(txtBrand.Text) ? null : txtBrand.Text.Trim(),
                UnitPrice = numUnitPrice.Value,
                Quantity = (int)numQty.Value,
                CategoryId = Val(cmbCategory),
                SupplierId = Val(cmbSupplier)
            };

            try
            {
                await _itemApi.UpdateAsync(id, e);
                await ReloadAllAsync();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task DeleteItemAsync()
        {
            if (gridItems.CurrentRow?.Cells["Id"]?.Value is not int id) return;
            if (MessageBox.Show("Delete selected item?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                await _itemApi.DeleteAsync(id);
                await ReloadAllAsync();
                ClearItemInputs();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ClearItemInputs()
        {
            txtName.Text = txtCode.Text = txtBrand.Text = "";
            numUnitPrice.Value = 0;
            numQty.Value = 0;
            cmbCategory.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;
        }

        private async Task AddCategoryAsync()
        {
            if (string.IsNullOrWhiteSpace(txtCatName.Text)) { MessageBox.Show("Name is required."); return; }
            try
            {
                await _categoryApi.CreateAsync(new Category { Name = txtCatName.Text.Trim() });
                await ReloadAllAsync();
                txtCatName.Text = "";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task UpdateCategoryAsync()
        {
            if (gridCategories.CurrentRow?.Cells["Id"]?.Value is not int id) return;
            if (string.IsNullOrWhiteSpace(txtCatName.Text)) { MessageBox.Show("Name is required."); return; }
            try
            {
                await _categoryApi.UpdateAsync(id, new Category { Name = txtCatName.Text.Trim() });
                await ReloadAllAsync();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task DeleteCategoryAsync()
        {
            if (gridCategories.CurrentRow?.Cells["Id"]?.Value is not int id) return;
            if (MessageBox.Show("Delete selected category?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                await _categoryApi.DeleteAsync(id);
                await ReloadAllAsync();
                txtCatName.Text = "";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task AddSupplierAsync()
        {
            if (string.IsNullOrWhiteSpace(txtSupName.Text)) { MessageBox.Show("Name is required."); return; }
            try
            {
                await _supplierApi.CreateAsync(new Supplier
                {
                    Name = txtSupName.Text.Trim(),
                    Contact = string.IsNullOrWhiteSpace(txtSupContact.Text) ? null : txtSupContact.Text.Trim()
                });
                await ReloadAllAsync();
                txtSupName.Text = ""; txtSupContact.Text = "";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task UpdateSupplierAsync()
        {
            if (gridSuppliers.CurrentRow?.Cells["Id"]?.Value is not int id) return;
            if (string.IsNullOrWhiteSpace(txtSupName.Text)) { MessageBox.Show("Name is required."); return; }
            try
            {
                await _supplierApi.UpdateAsync(id, new Supplier
                {
                    Name = txtSupName.Text.Trim(),
                    Contact = string.IsNullOrWhiteSpace(txtSupContact.Text) ? null : txtSupContact.Text.Trim()
                });
                await ReloadAllAsync();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task DeleteSupplierAsync()
        {
            if (gridSuppliers.CurrentRow?.Cells["Id"]?.Value is not int id) return;
            if (MessageBox.Show("Delete selected supplier?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                await _supplierApi.DeleteAsync(id);
                await ReloadAllAsync();
                txtSupName.Text = ""; txtSupContact.Text = "";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task ApplyMovementAsync()
        {
            if (cmbStkItem.SelectedValue is not int itemId) { MessageBox.Show("Select Item."); return; }
            if (numStkQty.Value <= 0) { MessageBox.Show("Qty must be > 0."); return; }

            var m = new StockMovement
            {
                ItemId = itemId,
                Type = (MovementType)cmbStkType.SelectedItem!,
                Quantity = (int)numStkQty.Value,
                Remarks = string.IsNullOrWhiteSpace(txtStkRemarks.Text) ? null : txtStkRemarks.Text.Trim()
            };

            try
            {
                await _movementApi.CreateAsync(m);
                await ReloadAllAsync();
                txtStkRemarks.Text = ""; numStkQty.Value = 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}

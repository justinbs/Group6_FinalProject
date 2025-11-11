using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.WinForms.Models;
using Client.WinForms.Services;

namespace Client.WinForms
{
    public partial class Form1 : Form
    {
        private readonly CategoryApi _catApi = new();
        private readonly SupplierApi _supApi = new();
        private readonly ItemApi _itemApi = new();
        private readonly StockApi _stockApi = new();

        private List<Category> _categories = new();
        private List<Supplier> _suppliers = new();
        private List<Item> _items = new();

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            await ApiBase.WaitForApiAsync();
            await LoadLookupsAsync();
            await LoadItemsAsync();
            BuildGridColumns();
        }

        private async Task LoadLookupsAsync()
        {
            _categories = (await _catApi.GetAllAsync()).ToList();
            _suppliers = (await _supApi.GetAllAsync()).ToList();

            cboCategory.DisplayMember = "Name";
            cboCategory.ValueMember = "Id";
            cboCategory.DataSource = _categories;

            cboSupplier.DisplayMember = "Name";
            cboSupplier.ValueMember = "Id";
            cboSupplier.DataSource = _suppliers;
        }

        private async Task LoadItemsAsync()
        {
            _items = (await _itemApi.GetAllAsync()).ToList();
            BindGrid(_items);
        }

        private void BindGrid(IEnumerable<Item> data)
        {
            // show category/supplier names even if navigation props are null
            var rows = data.Select(i => new
            {
                i.Id,
                i.Name,
                i.Brand,
                Category = i.Category?.Name ?? _categories.FirstOrDefault(c => c.Id == i.CategoryId)?.Name,
                Supplier = i.Supplier?.Name ?? _suppliers.FirstOrDefault(s => s.Id == i.SupplierId)?.Name,
                i.UnitPrice,
                i.Quantity
            }).ToList();

            gridItems.AutoGenerateColumns = true;
            gridItems.DataSource = rows;
        }

        private void BuildGridColumns()
        {
            // We let AutoGenerate = true in BindGrid, but if you prefer fixed columns, keep this.
            if (gridItems.Columns.Count > 0) return;
            gridItems.AutoGenerateColumns = false;
            gridItems.Columns.Clear();

            DataGridViewTextBoxColumn Add(string dataProperty, string header, int width)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = dataProperty,
                    HeaderText = header,
                    Width = width
                };
                gridItems.Columns.Add(col);
                return col;
            }

            Add(nameof(Item.Id), "ID", 60);
            Add(nameof(Item.Name), "Name", 160);
            Add(nameof(Item.Brand), "Brand", 120);
            Add("Category", "Category", 140);
            Add("Supplier", "Supplier", 140);
            Add(nameof(Item.UnitPrice), "Unit Price", 90);
            Add(nameof(Item.Quantity), "On Hand", 80);
        }

        // ----------------- helpers -----------------
        private Item? GetSelectedItem()
        {
            if (gridItems.CurrentRow == null) return null;
            var idObj = gridItems.CurrentRow.Cells["Id"]?.Value ?? gridItems.CurrentRow.Cells[0].Value;
            if (idObj == null) return null;
            var id = Convert.ToInt32(idObj);
            return _items.FirstOrDefault(x => x.Id == id);
        }

        private void SelectRowById(int id)
        {
            foreach (DataGridViewRow row in gridItems.Rows)
            {
                var val = row.Cells["Id"]?.Value ?? row.Cells[0].Value;
                if (val != null && Convert.ToInt32(val) == id)
                {
                    row.Selected = true;
                    gridItems.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }

        private void ClearEditor()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtBrand.Text = "";
            numUnitPrice.Value = 0;
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
            if (cboSupplier.Items.Count > 0) cboSupplier.SelectedIndex = 0;
        }

        private void PopulateEditor(Item i)
        {
            // txtCode is just a display for Id (since API has no 'Code')
            txtCode.Text = i.Id.ToString();
            txtName.Text = i.Name;
            txtBrand.Text = i.Brand ?? "";
            numUnitPrice.Value = i.UnitPrice;
            cboCategory.SelectedValue = i.CategoryId;
            cboSupplier.SelectedValue = i.SupplierId;
        }

        // ----------------- events -----------------
        private void gridItems_SelectionChanged(object? sender, EventArgs e)
        {
            var sel = GetSelectedItem();
            if (sel != null) PopulateEditor(sel);
        }

        private void txtSearch_TextChanged(object? sender, EventArgs e)
        {
            var q = txtSearch.Text?.Trim() ?? "";
            IEnumerable<Item> data = _items;
            if (!string.IsNullOrEmpty(q))
            {
                var low = q.ToLowerInvariant();
                data = _items.Where(i =>
                    i.Name.ToLower().Contains(low) ||
                    (i.Brand ?? "").ToLower().Contains(low));
            }
            BindGrid(data);
        }

        private async void btnRefresh_Click(object? sender, EventArgs e)
        {
            await LoadItemsAsync();
        }

        private Item BuildItemFromEditor()
        {
            return new Item
            {
                Id = string.IsNullOrWhiteSpace(txtCode.Text) ? 0 : int.Parse(txtCode.Text),
                Name = txtName.Text.Trim(),
                Brand = string.IsNullOrWhiteSpace(txtBrand.Text) ? null : txtBrand.Text.Trim(),
                CategoryId = (int)(cboCategory.SelectedValue ?? 0),
                SupplierId = (int)(cboSupplier.SelectedValue ?? 0),
                UnitPrice = numUnitPrice.Value,
                Quantity = 0 // server maintains quantity; create can start at 0
            };
        }

        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            try
            {
                var model = BuildItemFromEditor();
                model.Id = 0;
                var created = await _itemApi.CreateAsync(model);
                if (created == null) throw new Exception("Create returned no content.");
                await LoadItemsAsync();
                SelectRowById(created.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Add failed:\n\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            try
            {
                var sel = GetSelectedItem();
                if (sel == null) return;

                var model = BuildItemFromEditor();
                var saved = await _itemApi.UpdateAsync(sel.Id, model);
                if (saved == null) throw new Exception("Update returned no content.");

                await LoadItemsAsync();
                SelectRowById(saved.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed:\n\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            var sel = GetSelectedItem();
            if (sel == null) return;

            if (MessageBox.Show($"Delete '{sel.Name}'?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                await _itemApi.DeleteAsync(sel.Id);
                await LoadItemsAsync();
                ClearEditor();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete failed:\n\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReceive_Click(object? sender, EventArgs e)
        {
            var sel = GetSelectedItem();
            if (sel == null) return;

            using var dlg = new ReceiveDialog(sel);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    await _stockApi.ReceiveAsync(sel.Id, dlg.Quantity, dlg.Note);
                    await LoadItemsAsync();
                    SelectRowById(sel.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Receive failed:\n\n{ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnIssue_Click(object? sender, EventArgs e)
        {
            var sel = GetSelectedItem();
            if (sel == null) return;

            using var dlg = new IssueDialog(sel);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    await _stockApi.IssueAsync(sel.Id, dlg.Quantity, dlg.Note);
                    await LoadItemsAsync();
                    SelectRowById(sel.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Issue failed:\n\n{ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

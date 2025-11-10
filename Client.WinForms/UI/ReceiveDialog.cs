using System;
using System.Windows.Forms;
using Client.WinForms.Models;

namespace Client.WinForms
{
    public partial class ReceiveDialog : Form
    {
        private readonly Item _item;

        public int Quantity { get; private set; }
        public string? Note { get; private set; }

        public ReceiveDialog(Item item)
        {
            InitializeComponent();
            _item = item;
            lblItem.Text = $"Receive for: {item.Name}";
            numQty.Minimum = 1;
            numQty.Maximum = 1_000_000;
            numQty.Value = 1;
        }

        private void btnOk_Click(object? sender, EventArgs e)
        {
            Quantity = (int)numQty.Value;
            Note = string.IsNullOrWhiteSpace(txtNote.Text) ? null : txtNote.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

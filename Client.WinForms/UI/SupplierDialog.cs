using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Client.WinForms.UI
{
    public partial class SupplierDialog : Form
    {
        public string SupplierName => txtName.Text.Trim();
        public string? Contact => txtContact.Text.Trim();

        public SupplierDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SupplierName))
            {
                MessageBox.Show("Please enter supplier name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

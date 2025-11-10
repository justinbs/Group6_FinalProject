namespace Client.WinForms
{
    public partial class IssueDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

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

            this.Text = "Issue Stock";
            this.Width = 420;
            this.Height = 220;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            lblItem = new System.Windows.Forms.Label { Left = 12, Top = 12, Width = 380, Text = "Item" };

            var lblQty = new System.Windows.Forms.Label { Left = 12, Top = 48, Width = 80, Text = "Quantity:" };
            numQty = new System.Windows.Forms.NumericUpDown { Left = lblQty.Right + 6, Top = 44, Width = 120, Minimum = 1, Maximum = 100000 };

            var lblNote = new System.Windows.Forms.Label { Left = 12, Top = 80, Width = 80, Text = "Note:" };
            txtNote = new System.Windows.Forms.TextBox { Left = lblNote.Right + 6, Top = 76, Width = 280 };

            btnOk = new System.Windows.Forms.Button { Left = 200, Top = 120, Width = 80, Height = 30, Text = "OK" };
            btnCancel = new System.Windows.Forms.Button { Left = btnOk.Right + 8, Top = 120, Width = 80, Height = 30, Text = "Cancel" };

            btnOk.Click += btnOk_Click;
            btnCancel.Click += btnCancel_Click;

            this.Controls.Add(lblItem);
            this.Controls.Add(lblQty);
            this.Controls.Add(numQty);
            this.Controls.Add(lblNote);
            this.Controls.Add(txtNote);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);
        }

        #endregion
    }
}

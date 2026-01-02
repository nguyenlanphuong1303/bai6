namespace QLSach
{
    partial class ThongKe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ReportForm = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ReportForm
            // 
            this.ReportForm.Location = new System.Drawing.Point(12, 43);
            this.ReportForm.Name = "ReportForm";
            this.ReportForm.ServerReport.BearerToken = null;
            this.ReportForm.Size = new System.Drawing.Size(765, 372);
            this.ReportForm.TabIndex = 0;
            this.ReportForm.Load += new System.EventHandler(this.ReportForm_Load);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(684, 422);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Back";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // Thống_kê
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ReportForm);
            this.Name = "ThongKe";
            this.Text = "Thống kê";
            this.Load += new System.EventHandler(this.ThongKe_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer ReportForm;
        private System.Windows.Forms.Button btnClose;
    }
}
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSach
{
    public partial class ThongKe : Form
    {
        private List<Sach> _data;
        public ThongKe(List<Sach> data)
        {
            InitializeComponent();
            _data = data;
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            try
            {
                this.ReportForm.LocalReport.ReportPath = "Report1.rdlc"; // Assuming Report1.rdlc is the report file name
                var reportDataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", _data); // "DataSet1" must match the dataset name in RDLC
                this.ReportForm.LocalReport.DataSources.Clear();
                this.ReportForm.LocalReport.DataSources.Add(reportDataSource);
                this.ReportForm.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo: " + ex.Message);
            }
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
        }
    }
}

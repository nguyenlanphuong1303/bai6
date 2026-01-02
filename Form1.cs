using BUS;
using DAL;
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
    public partial class Form1 : Form
    {
        private readonly SachService sachService = new SachService();
        private readonly LoaiSachService loaiSachService = new LoaiSachService();
        private List<Sach> listSach;
        private Sach currentSelected = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var listSach = sachService.GetAll();
            var listTL = loaiSachService.GetAll();
            BindGrid(listSach);
            FillTLCombobox(listTL);
        }
        private void FillTLCombobox(List<LoaiSach> listTL)
        {
            listTL.Insert(0, new LoaiSach());
            this.cmbTL.DataSource = listTL;
            this.cmbTL.DisplayMember = "TenLoai";
            this.cmbTL.ValueMember = "MaLoai";
            this.cmbTL.SelectedIndex = 0;
        }
        private void BindGrid(List<Sach> listSach)
        {
            dgvSach.Rows.Clear();
            foreach(var item in  listSach)
            {
                int index = dgvSach.Rows.Add();
                dgvSach.Rows[index].Cells[0].Value = item.MaSach;
                dgvSach.Rows[index].Cells[1].Value = item.TenSach;
                dgvSach.Rows[index].Cells[2].Value = item.NamXB;
                dgvSach.Rows[index].Cells[3].Value = item.LoaiSach.TenLoai;
            }
        }

        private void dgvSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvSach.Rows[e.RowIndex];
                BindCurrentRow(selectedRow);
            }
        }

        private void BindCurrentRow(DataGridViewRow selectedRow)
        {
            txtMa.Text = selectedRow.Cells[0].Value?.ToString();
            txtTen.Text = selectedRow.Cells[1].Value?.ToString();
            txtNam.Text = selectedRow.Cells[2].Value?.ToString();
            cmbTL.Text = selectedRow.Cells[3].Value?.ToString();
        }

        private void thốngKêSáchTheoNămToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SachService service = new SachService();
            var listSach = service.GetSachByNamXB();
            ThongKe frm = new ThongKe(listSach);
            frm.ShowDialog();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text) ||
                string.IsNullOrWhiteSpace(txtTen.Text) ||
                string.IsNullOrWhiteSpace(txtNam.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtMa.Text.Trim().Length != 6)
            {
                MessageBox.Show("Mã Sách phải có 6 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var s = new Sach
            {
                MaSach = txtMa.Text.Trim().ToUpper(),
                TenSach = txtTen.Text.Trim(),
                NamXB = int.Parse(txtNam.Text.Trim()),
                MaLoai = (int)cmbTL.SelectedValue
            };
            if(sachService.AddSach(s))
            {
                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listSach = sachService.GetAll();
                BindGrid(listSach);
            }
            else
            {
                MessageBox.Show("Mã sách đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string maSach = txtMa.Text.Trim();
            if(string.IsNullOrEmpty(maSach))
            {
                MessageBox.Show("Vui long chon ten sach de sua!", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int newMaTL = (int)cmbTL.SelectedValue;
            bool canUpdate = true;

            if (canUpdate)
            {
                var s = new Sach
                {
                    MaSach = maSach,
                    TenSach = txtTen.Text.Trim(),
                    NamXB = int.Parse(txtNam.Text.Trim()),
                    MaLoai = newMaTL
                };
                if (sachService.UpdateSach(s))
                {
                    MessageBox.Show("Cap nhat thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listSach = sachService.GetAll();
                    BindGrid(listSach);
                }
                else
                {
                    MessageBox.Show("Cap nhat that bai!", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string maSach = txtMa.Text.Trim();
                if (string.IsNullOrEmpty(maSach))
                {
                    MessageBox.Show("Vui lòng nhập hoặc chọn Mã Sách cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sách này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    if (sachService.DeleteSach(maSach))
                    {
                        MessageBox.Show("Xóa sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Cập nhật lại grid
                        listSach = sachService.GetAll();
                        BindGrid(listSach);

                        // Clear input fields
                        txtMa.Clear();
                        txtTen.Clear();
                        txtNam.Clear();
                        cmbTL.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sách để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (dgvSach.Rows.Count > 0)
            {
                int currentIndex = dgvSach.CurrentCell != null ? dgvSach.CurrentCell.RowIndex : 0;
                int prevIndex = currentIndex - 1;

                if (prevIndex >= 0)
                {
                    dgvSach.CurrentCell = dgvSach.Rows[prevIndex].Cells[0];
                    BindCurrentRow(dgvSach.Rows[prevIndex]);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
             if (dgvSach.Rows.Count > 0)
            {
                int currentIndex = dgvSach.CurrentCell != null ? dgvSach.CurrentCell.RowIndex : 0;
                // Note: dgvSach.Rows.Count includes the 'new row' placeholder if properties assume so.
                // Assuming manually added rows, check if it's the last real row.
                // If AllowUserToAddRows is true, the last row is new row.
                int maxBuffer = dgvSach.AllowUserToAddRows ? 2 : 1; 
                
                int nextIndex = currentIndex + 1;
                
                // Simple check: can we move to next?
                if (nextIndex < dgvSach.Rows.Count)
                {
                    // If it's the new row placeholder, don't select it as 'data' unless intended.
                    if (dgvSach.Rows[nextIndex].IsNewRow) return;

                    dgvSach.CurrentCell = dgvSach.Rows[nextIndex].Cells[0];
                    BindCurrentRow(dgvSach.Rows[nextIndex]);
                }
            }
        }
    }
}

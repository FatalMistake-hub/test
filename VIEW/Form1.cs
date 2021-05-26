using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   
using System.Windows.Forms;
using MVC.BLL;
using MVC.DTO;
using MVC.VIEW;


namespace MVC.VIEW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Show();
            SetCBBLSH();
        }
        private void Show(int ID_Lop)
        {
            BLL_QLSV BLL = new BLL_QLSV();
            dataGridViewDSSV.DataSource = BLL.GetAllSV_BLL(ID_Lop);
            dataGridViewDSSV.Columns["MSSV"].Visible = false;
            dataGridViewDSSV.Columns["ID_Lop"].Visible = false;

        }
        public void SetCBBLSH()
        {
            BLL_QLSV BLL = new BLL_QLSV();
            if (cbbLopSH.Items != null)
            {
                cbbLopSH.Items.Clear();
            }
            cbbLopSH.Items.Add(new CBBItems { Value = 0, Text = "All" });
            foreach (LSH i in BLL.GetAllLSH_BLL())
            {
                cbbLopSH.Items.Add(new CBBItems()
                {
                    Value = i.ID_Lop,
                    Text = i.NameLop
                });
                cbbLopSH.SelectedIndex = 0;
            }
        }
        private void buttonShow_Click(object sender, EventArgs e)
        {
            
            int ID_Lop = ((CBBItems)cbbLopSH.Items[cbbLopSH.SelectedIndex]).Value;
            Show(ID_Lop);
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form3 TTSV = new Form3(null);
            TTSV.d = new Form3.mydelegate(Show);
            TTSV.Show();
        }
        private void buttonSort_Click(object sender, EventArgs e)
        {
            List<string> LMMS = new List<string>();
            int ID_Lop = ((CBBItems)cbbLopSH.Items[cbbLopSH.SelectedIndex]).Value;

            switch (cbbSort.Text)
            {
                case "Tên, A->Z":
                    dataGridViewDSSV.DataSource = BLL_QLSV.Instance.SortSV_BLL(LMMS,SV.NameAZ);
                    break;
                case "Tên, Z->A":
                    dataGridViewDSSV.DataSource = BLL_QLSV.Instance.SortSV_BLL(LMMS,SV.NameZA);
                    break;
                case "MSSV, Thấp -> Cao":
                    dataGridViewDSSV.DataSource = BLL_QLSV.Instance.SortSV_BLL(LMMS,SV.MSSVThapCao);
                    break;
                case "MSSV, Cao -> Thấp":
                    dataGridViewDSSV.DataSource = BLL_QLSV.Instance.SortSV_BLL(LMMS,SV.MSSVCaoThap);
                    break;
                case "Lớp":
                    dataGridViewDSSV.DataSource = BLL_QLSV.Instance.SortSV_BLL(LMMS, SV.LopSort);
                    break;
                case "Ngày sinh":
                    dataGridViewDSSV.DataSource = BLL_QLSV.Instance.SortSV_BLL(LMMS, SV.NSSort);
                    break;
                default:
                    break;
            }
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewDSSV.SelectedRows.Count == 1)
            {
                string k = (string)dataGridViewDSSV.SelectedRows[0].Cells["MSSV"].Value;
                SV sv = BLL_QLSV.Instance.FindSv(k);
                Form3 f2 = new Form3(sv.MSSV);
                f2.d = new Form3.mydelegate(Show);
                f2.Show();
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            BLL_QLSV BLL = new BLL_QLSV();
            DataGridViewSelectedRowCollection data = dataGridViewDSSV.SelectedRows;
            if (data.Count == 1)
            {
                string MSSV = data[0].Cells["MSSV"].Value.ToString();
                SV s = BLL.GetSVbyMSSV_BLL(MSSV);
                BLL.DeleteSV_BLL(s);
            }
            Show(cbbLopSH.SelectedIndex);
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int ID_Lop = ((CBBItems)cbbLopSH.Items[cbbLopSH.SelectedIndex]).Value;
            Show(ID_Lop);
            List<string> LMSSV = new List<string>();
            foreach (DataGridViewRow i in dataGridViewDSSV.Rows)
            {
                LMSSV.Add(i.Cells[0].Value.ToString());
            }
            dataGridViewDSSV.DataSource = BLL_QLSV.Instance.GetSVByName_BLL(LMSSV, textBoxSearch.Text);
        }
    }
}

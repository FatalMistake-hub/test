using MVC.BLL;
using MVC.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC.VIEW
{
    public partial class Form3 : Form
    {
        public delegate void mydelegate(int ID_Lop);
        public mydelegate d { get; set; }
        private string MSSV { get; set; }
        public Form3(string ms)
        {
            this.MSSV = ms;
            InitializeComponent();
            ADDANDEDIT();
        }
        public void ADDANDEDIT()
        {
            SV s = BLL_QLSV.Instance.GetSVbyMSSV_BLL(this.MSSV);
            if (s != null)
            {
                this.textBoxMSSV.Text = s.MSSV.ToString();
                this.textBoxNameSV.Text = s.NameSV;
                foreach (LSH i in BLL_QLSV.Instance.GetAllLSH_BLL())
                {
                    comboBoxLopSH.Items.Add(new CBBItems
                    {
                        Value = i.ID_Lop,
                        Text = i.NameLop
                    });
                    comboBoxLopSH.SelectedIndex = 0;
                    if (s.ID_Lop == i.ID_Lop)
                    {
                        comboBoxLopSH.SelectedItem = comboBoxLopSH.Items[comboBoxLopSH.Items.Count - 1];
                    }
                }
                textBoxMSSV.ReadOnly = true;
                if (s.Gender == true)
                {
                    radioButtonMale.Checked = true;
                }
                else
                {
                    radioButtonFemale.Checked = true;
                }
                comboBoxLopSH.Text = ((CBBItems)comboBoxLopSH.Items[s.ID_Lop - 1]).Text;
            }
            else
            {
                foreach (LSH i in BLL_QLSV.Instance.GetAllLSH_BLL())
                {
                    comboBoxLopSH.Items.Add(new CBBItems
                    {
                        Value = i.ID_Lop,
                        Text = i.NameLop
                    });
                    comboBoxLopSH.SelectedIndex = 0;
                }

            }
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            string MSSV = textBoxMSSV.Text;
            string NameSV = textBoxNameSV.Text;
            bool Gender = radioButtonMale.Checked;
            DateTime BD = Convert.ToDateTime(dateTimePickerTTSV.Value);
            int LopSH = ((CBBItems)comboBoxLopSH.Items[comboBoxLopSH.SelectedIndex]).Value;
            SV s = new SV(MSSV, NameSV, Gender, BD, LopSH);

            if (this.MSSV == null)
            {
                BLL_QLSV.Instance.AddSV_BLL(s);
                this.Close();
            }
            else
            {
                BLL_QLSV.Instance.UpdateSV_BLL(s);
                this.Close();
            }
            this.d(0);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}


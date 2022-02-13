using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSQL_Picture;

namespace Property_Administration
{
    public partial class Form7 : Form
    {
        int i = 0;
        public Form7()
        {
            InitializeComponent();
        }

        public Form7(Form4 fr, Form1 fr1)
        {
            InitializeComponent();
            frm = fr;
            frm1 = fr1;
        }

        Form4 frm = new Form4();
        Form1 frm1 = new Form1();

        private void Form7_Load(object sender, EventArgs e)
        {
            try
            {
                var a = frm1.dgvOwners.SelectedRows[0];
                var b = frm1.listaproprietari.SearchbyId(int.Parse(a.Cells[0].Value.ToString()));
                var x = frm.dgvEstates.SelectedRows[0];
                var z = b.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));
                this.Text = $"Informatii proprietate ID:{z.Id}";

                label10.Text = z.Name.ToString();
                label9.Text = z.Address;
                label8.Text = z.Price.ToString();
                label7.Text = z.Type.ToString();
                label1.Text = z.CreateDate.ToString();

                z.Pictures.Clear();
                foreach (var m in new DbSqlServerPicture().GetAll(z.Id))
                    z.Pictures.Add(m);

                if (z.Pictures.Count != 0)
                {
                    pictureBox1.ImageLocation = z.Pictures[0].Location;
                    photoNumber.Text = $"{i + 1}/{z.Pictures.Count}";
                    label11.Visible = false;
                }
                else
                {
                    photoNumber.Visible = false;
                    nextButton.Visible = false;
                    previousButton.Visible = false;
                    label11.Visible = true;
                }
            }
            catch
            {
                if (frm.dgvEstates.Rows.Count == 0)
                {
                    MessageBox.Show("Nu exista nicio proprietate adaugata");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Selectati o proprietate din tabel");
                    this.Close();
                }
            }

        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            var a = frm1.dgvOwners.SelectedRows[0];
            var b = frm1.listaproprietari.SearchbyId(int.Parse(a.Cells[0].Value.ToString()));
            var x = frm.dgvEstates.SelectedRows[0];
            var z = b.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));

            if (i > 0)
                i--;
            pictureBox1.ImageLocation = z.Pictures[i].Location;
            photoNumber.Text = $"{i + 1}/{z.Pictures.Count}";

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            var a = frm1.dgvOwners.SelectedRows[0];
            var b = frm1.listaproprietari.SearchbyId(int.Parse(a.Cells[0].Value.ToString()));
            var x = frm.dgvEstates.SelectedRows[0];
            var z = b.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));

            if (i < z.Pictures.Count - 1)
                i++;
            pictureBox1.ImageLocation = z.Pictures[i].Location;
            photoNumber.Text = $"{i + 1}/{z.Pictures.Count}";
        }
    }
}

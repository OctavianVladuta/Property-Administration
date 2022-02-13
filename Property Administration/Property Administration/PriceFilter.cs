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

namespace Property_Administration
{
    public partial class PriceFilter : Form
    {
        internal static int pricemin;
        internal static int pricemax;
        public PriceFilter()
        {
            InitializeComponent();
        }

        public PriceFilter(Form4 fr, Form1 fr1)
        {
            InitializeComponent();
            frm = fr;
            frm1 = fr1;
        }
        Form4 frm = new Form4();
        Form1 frm1 = new Form1();

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            var y = frm1.dgvOwners.SelectedRows[0];
            var z = frm1.listaproprietari.SearchbyId(int.Parse(y.Cells[0].Value.ToString()));
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    frm.adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + z.Id + "'" + "and pret between '" + 0 + "' and" + "'" + int.Parse(textBox2.Text) + "'", frm.connstring);
                    pricemin = 0;
                    pricemax = int.Parse(textBox2.Text);
                }
                else if (string.IsNullOrEmpty(textBox2.Text))
                {
                    frm.adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + z.Id + "'" + "and pret between '" + int.Parse(textBox1.Text) + "' and" + "'" + 99999999999 + "'", frm.connstring);
                    pricemin = int.Parse(textBox1.Text);
                    pricemax = 999999999;
                }
                else if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                {
                    frm.adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + z.Id + "'" + "and pret between '" + int.Parse(textBox1.Text) + "' and" + "'" + int.Parse(textBox2.Text) + "'", frm.connstring);
                    pricemin = int.Parse(textBox1.Text);
                    pricemax = int.Parse(textBox2.Text);
                }
                else if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
                    MessageBox.Show("Introduceti cel putin pretul minim sau cel maxim");
                frm.dt = new DataTable();
            }
            catch
            {
                MessageBox.Show("Introduceti cel putin pretul minim sau cel maxim");
                return;
            }

            frm.adpt.Fill(frm.dt);

            frm.dgvEstates.DataSource = frm.dt;

            frm.dgvEstates.DataSource = frm.dt;
            frm.dt.Columns.Remove("OwnerId");
            frm.dgvEstates.Columns["Id"].Visible = false;
            this.Close();
        }
    }
}

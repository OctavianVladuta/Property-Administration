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
    public partial class TypeFilter : Form
    {
        internal static string type;
        public TypeFilter()
        {
            InitializeComponent();
        }

        public TypeFilter(Form4 fr, Form1 fr1)
        {
            InitializeComponent();
            frm = fr;
            frm1 = fr1;
        }
        Form4 frm = new Form4();
        Form1 frm1 = new Form1();

        private void TypeFilter_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Apartament");
            comboBox1.Items.Add("Casa");
            comboBox1.Items.Add("Teren");
            comboBox1.Items.Add("Birouri");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var y = frm1.dgvOwners.SelectedRows[0];
            var z = frm1.listaproprietari.SearchbyId(int.Parse(y.Cells[0].Value.ToString()));

            type = comboBox1.SelectedItem.ToString();
            frm.adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + z.Id + "'" + "and Tip='" + comboBox1.SelectedItem.ToString() + "'", frm.connstring);
            frm.dt = new DataTable();

            frm.adpt.Fill(frm.dt);

            frm.dgvEstates.DataSource = frm.dt;

            frm.dgvEstates.DataSource = frm.dt;
            frm.dt.Columns.Remove("OwnerId");
            frm.dgvEstates.Columns["Id"].Visible = false;
            this.Close();
        }
    }
}

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
    public partial class YearFilter : Form
    {
        internal static DateTime datamin;
        internal static DateTime datamax;
        public YearFilter()
        {
            InitializeComponent();
        }

        public YearFilter(Form4 fr, Form1 fr1)
        {
            InitializeComponent();
            frm = fr;
            frm1 = fr1;
        }
        Form4 frm = new Form4();
        Form1 frm1 = new Form1();

        private void YearFilter_Load(object sender, EventArgs e)
        {

            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value != dateTimePicker2.Value && dateTimePicker1.Value < dateTimePicker2.Value)
            {
                var y = frm1.dgvOwners.SelectedRows[0];
                var z = frm1.listaproprietari.SearchbyId(int.Parse(y.Cells[0].Value.ToString()));
                datamin = dateTimePicker1.Value.Date;
                datamax = dateTimePicker2.Value.Date;

                frm.adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + z.Id + "'" + "and Data_Fabricatiei between '" + dateTimePicker1.Value.Date + "' and" + "'" + dateTimePicker2.Value.Date + "'", frm.connstring);
                frm.dt = new DataTable();

                frm.adpt.Fill(frm.dt);

                frm.dgvEstates.DataSource = frm.dt;

                frm.dgvEstates.DataSource = frm.dt;
                frm.dt.Columns.Remove("OwnerId");
                frm.dgvEstates.Columns["Id"].Visible = false;
                this.Close();
            }
            else
            {
                MessageBox.Show("Introduceti date valide");
                return;
            }
        }
    }
}

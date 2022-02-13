using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSQL_Owner;

namespace Property_Administration
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(Form1 fr)
        {
            InitializeComponent();
            frm = fr;
        }

        Form1 frm = new Form1();


        private void Form3_Load(object sender, EventArgs e)
        {
            var x = frm.dgvOwners.SelectedRows[0];
            var z = frm.listaproprietari.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));
            this.Text = $"Editare proprietar ID:{z.Id}";


            textBoxName.Text = z.Name.ToString();
            textBoxEmail.Text = z.Email.ToString();
            textBoxPhone.Text = z.Phone.ToString();
            textBoxCNP.Text = z.Cnp.ToString();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            var y = new DbSqlServerOwner();


            var x = frm.dgvOwners.SelectedRows[0];
            var z = frm.listaproprietari.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));


            z.Name = textBoxName.Text;
            z.Email = textBoxEmail.Text;
            z.Phone = textBoxPhone.Text;
            z.Cnp = textBoxCNP.Text;
            y.Update(z);
            frm.LoadData();
            MessageBox.Show("Edited");
            this.Close();
        }
    }
}

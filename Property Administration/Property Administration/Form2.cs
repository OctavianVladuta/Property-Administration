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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Form1 fr)
        {
            InitializeComponent();
            frm = fr;
        }

        Form1 frm = new Form1();

        public int id()
        {
            bool no;
            for (int i = 1; i <= frm.dgvOwners.Rows.Count + 1; i++)
            {
                no = false;
                foreach (var z in frm.listaproprietari.ownerList)
                {
                    if (z.Id == i)
                    {
                        no = true;
                        break;
                    }
                }
                if (no == false)
                {
                    return i;

                }
            }

            return 0;
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            var x = new DbSqlServerOwner();

            //if (string.IsNullOrEmpty(textBoxID.Text))
            //{
            //    MessageBox.Show("Id-ul trebuie completat");
            //    return;
            //}

            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Numele trebuie completat");
                return;
            }
            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                MessageBox.Show("Emailul trebuie completat");
                return;
            }

            if (string.IsNullOrEmpty(textBoxPhone.Text))
            {
                MessageBox.Show("Numarul de telefon trebuie completat");
                return;
            }
            if (string.IsNullOrEmpty(textBoxCNP.Text))
            {
                MessageBox.Show("Cnp-ul trebuie completat");
                return;
            }




            Owner owner = new Owner();


            owner.Id = id();
            var z = frm.listaproprietari.SearchbyId(owner.Id);
            if (z != null)
            {
                MessageBox.Show("Exista deja un proprietar cu acest Id" + " " + frm.listaproprietari.ownerList.Count);
                foreach (var alpa in frm.listaproprietari.ownerList)
                    MessageBox.Show(alpa.ToString());

                return;
            }
            owner.Name = textBoxName.Text;
            owner.Email = textBoxEmail.Text;
            owner.Phone = textBoxPhone.Text;
            owner.Cnp = textBoxCNP.Text;

            // frm.listaproprietari.Add(owner);
            x.Create(owner);
            frm.LoadData();

            //frm.dgvOwners.Rows.Add(owner.Id, owner.Name, owner.Email, owner.Phone, owner.Cnp);
            MessageBox.Show("Added");
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = "Adăugare proprietar";
        }
    }
}

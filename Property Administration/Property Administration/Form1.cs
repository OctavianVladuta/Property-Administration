using System;                   //Octavian Vladuta  
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSQL_Owner;
using System.Configuration;
using System.Data.SqlClient;
using DSQL_Estate;
using DSQL_Picture;
using System.IO;

namespace Property_Administration
{
    public partial class Form1 : Form
    {
        internal ListOwner listaproprietari = new ListOwner();
        

        string connstring = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
        SqlDataAdapter adpt;
        DataTable dt;


        public Form1()
        {
            InitializeComponent();
            listaproprietari.ownerList.OrderBy(owner => owner.Id).ToList();
            // LoadData();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Proprietari";
            listaproprietari.ownerList.OrderBy(owner => owner.Id).ToList();
            dgvOwners.Font = new Font("Microsoft Sans Serif", 10);
            LoadData();
        }



        public void LoadData()
        {
            adpt = new SqlDataAdapter("select * from Owners", connstring);
            dt = new DataTable();
            adpt.Fill(dt);

            dgvOwners.DataSource = dt;
            dgvOwners.Columns["Id"].Visible = false;
            listaproprietari.ownerList.Clear();
            foreach (var x in new DbSqlServerOwner().GetAll())
            {
                listaproprietari.Add(x);
            }
            listaproprietari.ownerList.OrderBy(owner => owner.Id).ToList();
        }


        private void addOwner_Click(object sender, EventArgs e)
        {
            bool isOpen = false;

            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Adăugare proprietar")
                {
                    isOpen = true;
                    f.BringToFront();
                    break;
                }
            }

            if (isOpen == false)
            {
                Form2 frm2 = new Form2(this);
                frm2.Show();
            }

        }

        private void deleteOwnerButton_Click(object sender, EventArgs e)
        {
            var y = new DbSqlServerOwner();
            var y1 = new DbSqlServerEstate();
            var y2 = new DbSqlServerPicture();
            var directory = ConfigurationManager.AppSettings["UserDataPath"];



            try
            {
                var a = dgvOwners.SelectedRows[0];
                var b1 = listaproprietari.SearchbyId(int.Parse(a.Cells[0].Value.ToString()));

                foreach (var m in listaproprietari.ownerList)
                    foreach (var n in new DbSqlServerEstate().GetAll(m.Id))
                    {
                        m.Estates.Add(n);
                    }

                Form5.listaphoto.pictureList.Clear();
                foreach (var x in new DbSqlServerPicture().GetAll())
                {
                    Form5.listaphoto.pictureList.Add(x);
                }

                b1.Estates.Clear();
                foreach (var val in new DbSqlServerEstate().GetAll(b1.Id))
                {
                    b1.Estates.Add(val);
                    foreach (var val1 in b1.Estates)
                    {
                        val1.Pictures.Clear();
                        foreach (var m in new DbSqlServerPicture().GetAll(val1.Id))
                            val1.Pictures.Add(m);
                    }
                }
            }
            catch
            {
                if (dgvOwners.Rows.Count == 0)
                {
                    MessageBox.Show("Nu exista niciun proprietar adaugat");
                    return;
                }
                MessageBox.Show("Selectati un proprietar");
            }


            try
            {
                var x = dgvOwners.SelectedRows[0];
                var z = listaproprietari.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));




                foreach (var m in z.Estates)
                {


                    if (m.Pictures.Count != 0)
                    {
                        y2.Delete(m.Id);
                        foreach (var n in Form5.listaphoto.pictureList)
                        {
                            if (n.EstateId == m.Id)
                                if (File.Exists(n.Location))
                                    File.Delete(n.Location);
                        }

                        Directory.Delete(directory + $"\\{m.Id}");
                    }

                    y1.Delete(m.Id);


                }

                y.Delete(z.Id);
                listaproprietari.Remove(z);
                dgvOwners.Rows.Remove(x);
                MessageBox.Show("Removed");
                // LoadData();

            }
            catch
            {
                if (dgvOwners.Rows.Count == 0)
                {
                    MessageBox.Show("Nu exista niciun proprietar adaugat");
                    return;
                }
                MessageBox.Show("Selectati un proprietar");
            }
        }


        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool isOpen = false;
                var x = dgvOwners.SelectedRows[0];
                var z = listaproprietari.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));

                foreach (Form f in Application.OpenForms)
                {
                    if (f.Text == $"Editare proprietar ID:{z.Id}")
                    {
                        isOpen = true;
                        f.BringToFront();
                        break;
                    }
                }

                if (isOpen == false)
                {
                    Form3 frm3 = new Form3(this);
                    frm3.Show();
                }
            }
            catch
            {
                if (dgvOwners.Rows.Count == 0)
                {
                    MessageBox.Show("Nu exista niciun proprietar adaugat");
                    return;
                }
                MessageBox.Show("Selectati un proprietar");
            }

        }

        private void viewOwnerButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool isOpen = false;
                var x = dgvOwners.SelectedRows[0];
                var z = listaproprietari.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));

                foreach (Form f in Application.OpenForms)
                {
                    if (f.Text == $"Profil Proprietar ID:{z.Id}")
                    {
                        isOpen = true;
                        f.BringToFront();
                        break;
                    }
                }

                if (isOpen == false)
                {
                    Form4 frm4 = new Form4(this);
                    frm4.Show();
                }
            }
            catch
            {
                if (dgvOwners.Rows.Count == 0)
                    MessageBox.Show("Nu exista niciun proprietar adaugat");
                else
                    MessageBox.Show("Selectati un proprietar din tabel");
            }
        }
    }
}

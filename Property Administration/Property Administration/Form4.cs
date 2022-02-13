using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSQL_Estate;
using DSQL_Picture;

namespace Property_Administration
{
    public partial class Form4 : Form
    {
        internal ListEstate listaproprietati = new ListEstate();
        internal string connstring = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
        internal SqlDataAdapter adpt;
        internal DataTable dt;

        public Form4()
        {
            InitializeComponent();
            listaproprietati.estateList.OrderBy(estate => estate.Id).ToList();
            //LoadData();
        }

        public Form4(Form1 fr)
        {
            InitializeComponent();
            frm = fr;
        }

        Form1 frm = new Form1();


        private void Form4_Load(object sender, EventArgs e)
        {
            var x = frm.dgvOwners.SelectedRows[0];
            var z = frm.listaproprietari.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));
            this.Text = $"Profil Proprietar ID:{z.Id}";


            label9.Text = z.Name.ToString();
            label8.Text = z.Email.ToString();
            label7.Text = z.Phone.ToString();
            label6.Text = z.Cnp.ToString();

            listaproprietati.estateList.OrderBy(estate => estate.Id).ToList();
            comboBox1.Items.Add("No Filter");
            comboBox1.Items.Add("Pret");
            comboBox1.Items.Add("Tip");
            comboBox1.Items.Add("An Fabricatie");
            comboBox1.Items.Add("Cu poze");
            comboBox1.Text = comboBox1.Items[0].ToString();

            dgvEstates.Font= new Font("Microsoft Sans Serif", 10);

            LoadData();
        }


        public void LoadData()
        {
            var y = frm.dgvOwners.SelectedRows[0];
            var z = frm.listaproprietari.SearchbyId(int.Parse(y.Cells[0].Value.ToString()));
            adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + z.Id + "'", connstring);
            dt = new DataTable();

            adpt.Fill(dt);

            dgvEstates.DataSource = dt;
            dt.Columns.Remove("OwnerId");
            dgvEstates.Columns["Id"].Visible = false;
            listaproprietati.estateList.Clear();
            foreach (var n in new DbSqlServerEstate().GetAll())
            {
                listaproprietati.estateList.Add(n);
            }

            Form5.listaphoto.pictureList.Clear();
            foreach (var x in new DbSqlServerPicture().GetAll())
            {
                Form5.listaphoto.pictureList.Add(x);
            }


            //try
            //{
            //    var ig = dgvEstates.SelectedRows[0];
            //    var est = listaproprietati.SearchbyId(int.Parse(ig.Cells[0].Value.ToString()));

            //    est.Pictures.Clear();
            //    foreach (var x in new DbSqlServerPicture().GetAll(est.Id))
            //    {
            //        est.Pictures.Add(x);
            //    }
            //}
            //catch { }



            z.Estates.Clear();
            foreach (var x in new DbSqlServerEstate().GetAll(z.Id))
            {

                z.Estates.Add(x);
            }

            z.Estates.OrderBy(estate => estate.Id).ToList();




            //var c = frm.dgvOwners.SelectedRows[0];
            //var d = frm.listaproprietari.SearchbyId(int.Parse(c.Cells[0].Value.ToString()));
            //var e = frm.dgvOwners.SelectedRows[0];
            //var f = d.SearchbyId(int.Parse(e.Cells[0].Value.ToString()));

            //if (f !=null)
            //{
            //    f.Pictures.Clear();
            //    foreach (var est in new DbSqlServerPicture().GetAll(f.Id))
            //    {
            //        f.Pictures.Add(est);
            //    }
            //}


        }


        private void addEstateButton_Click(object sender, EventArgs e)
        {
            bool isOpen = false;
            var a = frm.dgvOwners.SelectedRows[0];
            var b = frm.listaproprietari.SearchbyId(int.Parse(a.Cells[0].Value.ToString()));
            var x = frm.dgvOwners.SelectedRows[0];
            var z = b.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));

            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == "Adăugare proprietate")
                {
                    isOpen = true;
                    f.BringToFront();
                    break;
                }
            }

            if (isOpen == false)
            {
                Form5 frm5 = new Form5(this, frm);
                frm5.Show();
            }
        }

        private void removeEstateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var directory = ConfigurationManager.AppSettings["UserDataPath"];
                var y = new DbSqlServerEstate();
                var y1 = new DbSqlServerPicture();


                var a = frm.dgvOwners.SelectedRows[0];
                var b = frm.listaproprietari.SearchbyId(int.Parse(a.Cells[0].Value.ToString()));
                var x = dgvEstates.SelectedRows[0];
                var z = b.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));


                y.Delete(z.Id);
                dgvEstates.Rows.Remove(x);

                Form5.listaphoto.pictureList.Clear();
                foreach (var x1 in new DbSqlServerPicture().GetAll())
                {
                    Form5.listaphoto.pictureList.Add(x1);
                }

                z.Pictures.Clear();
                foreach (var val in new DbSqlServerPicture().GetAll(z.Id))
                    z.Pictures.Add(val);

                // foreach (var m in z.Pictures)
                if (z.Pictures.Count != 0)
                {
                    y1.Delete(z.Id);
                    foreach (var m in Form5.listaphoto.pictureList)
                    {
                        if (m.EstateId == z.Id)
                            if (File.Exists(m.Location))
                                File.Delete(m.Location);
                    }

                    Directory.Delete(directory + $"\\{z.Id}");
                }


                //listaproprietati.Remove(z);
                b.Estates.Remove(z);
                MessageBox.Show("Removed");
                //LoadData();
            }
            catch
            {
                if (dgvEstates.Rows.Count == 0)
                    MessageBox.Show("Nu exista nicio proprietate adaugata");
                else
                    MessageBox.Show("Selectati o proprietate din tabel");
            }

        }

        private void modifyEstateButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool isOpen = false;
                var x = frm.dgvOwners.SelectedRows[0];
                var z = frm.listaproprietari.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));

                foreach (Form f in Application.OpenForms)
                {
                    if (f.Text == $"Editare proprietate ID:{dgvEstates.SelectedRows[0].Cells[0].Value.ToString()}")
                    {
                        isOpen = true;
                        f.BringToFront();
                        break;
                    }
                }

                if (isOpen == false)
                {
                    Form6 frm6 = new Form6(this, frm);
                    frm6.Show();
                }
            }
            catch
            {
                if (dgvEstates.Rows.Count == 0)
                    MessageBox.Show("Nu exista nicio proprietate adaugata");
                else
                    MessageBox.Show("Selectati o proprietate din tabel");
            }
        }

        private void viewOwnerButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool isOpen = false;
                var x = frm.dgvOwners.SelectedRows[0];
                var z = frm.listaproprietari.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));

                foreach (Form f in Application.OpenForms)
                {
                    if (f.Text == $"Informatii proprietate ID:{dgvEstates.SelectedRows[0].Cells[0].Value.ToString()}")
                    {
                        isOpen = true;
                        f.BringToFront();
                        break;
                    }
                }

                if (isOpen == false)
                {
                    Form7 frm7 = new Form7(this, frm);
                    frm7.Show();
                }
            }
            catch
            {
                if (dgvEstates.Rows.Count == 0)
                    MessageBox.Show("Nu exista nicio proprietate adaugata");
                else
                    MessageBox.Show("Selectati o proprietate din tabel");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var y = frm.dgvOwners.SelectedRows[0];
            var z = frm.listaproprietari.SearchbyId(int.Parse(y.Cells[0].Value.ToString()));



            if (comboBox1.SelectedItem.ToString() == "Pret")
            {

                bool isOpen = false;


                foreach (Form f in Application.OpenForms)
                {
                    if (f.Text == "PriceFilter")
                    {
                        isOpen = true;
                        f.BringToFront();
                        break;
                    }
                }

                if (isOpen == false)
                {
                    PriceFilter frm8 = new PriceFilter(this, frm);
                    frm8.Show();
                }


                //adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + z.Id + "'" + "and pret between '" + int.Parse(textBox1Filter.Text) + "' and" + "'" + int.Parse(textBox2Filter.Text) + "'", connstring);


                //dt = new DataTable();

                //adpt.Fill(dt);

                //dgvEstates.DataSource = dt;

                //dgvEstates.DataSource = dt;
                //dt.Columns.Remove("OwnerId");
                //dgvEstates.Columns["Id"].Visible = false;
            }



            if (comboBox1.SelectedItem.ToString() == "Tip")
            {

                bool isOpen = false;


                foreach (Form f in Application.OpenForms)
                {
                    if (f.Text == "TypeFilter")
                    {
                        isOpen = true;
                        f.BringToFront();
                        break;
                    }
                }

                if (isOpen == false)
                {
                    TypeFilter frm9 = new TypeFilter(this, frm);
                    frm9.Show();
                }

                //adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + z.Id + "'" + "and pret between '" + 0 + "' and" + "'" + 40000 + "'", connstring);
                //dt = new DataTable();

                //adpt.Fill(dt);

                //dgvEstates.DataSource = dt;

                //dgvEstates.DataSource = dt;
                //dt.Columns.Remove("OwnerId");
                //dgvEstates.Columns["Id"].Visible = false;
            }


            if (comboBox1.SelectedItem.ToString() == "An Fabricatie")
            {
                bool isOpen = false;


                foreach (Form f in Application.OpenForms)
                {
                    if (f.Text == "YearFilter")
                    {
                        isOpen = true;
                        f.BringToFront();
                        break;
                    }
                }

                if (isOpen == false)
                {
                    YearFilter frm10 = new YearFilter(this, frm);
                    frm10.Show();
                }



                //adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + z.Id + "'" + "and pret between '" + 0 + "' and" + "'" + 40000 + "'", connstring);
                //dt = new DataTable();

                //adpt.Fill(dt);

                //dgvEstates.DataSource = dt;

                //dgvEstates.DataSource = dt;
                //dt.Columns.Remove("OwnerId");
                //dgvEstates.Columns["Id"].Visible = false;
            }

            if (comboBox1.SelectedItem.ToString() == "Cu poze")
            {
                SqlDataReader rdr;
                int stop = 0;
                dt = new DataTable();
                foreach (var estate in z.Estates)
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlConnection con = new SqlConnection(connstring);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.CommandText = "select count(*) as Poze from Pictures where EstateId =" + estate.Id;
                    con.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (int.Parse(rdr["Poze"].ToString()) > 0)
                        {
                            adpt = new SqlDataAdapter("select * from Estates where Id=" + estate.Id, connstring);

                            adpt.Fill(dt);

                            dgvEstates.DataSource = dt;

                            dgvEstates.DataSource = dt;
                            dt.Columns.Remove("OwnerId");
                            dgvEstates.Columns["Id"].Visible = false;
                            stop = 1;
                        }

                    }
                }
                if (stop == 0)
                {
                    adpt = new SqlDataAdapter("select *from Estates where Id=-1000", connstring);
                    dt = new DataTable();
                    adpt.Fill(dt);

                    dgvEstates.DataSource = dt;

                    dgvEstates.DataSource = dt;
                    dt.Columns.Remove("OwnerId");
                    dgvEstates.Columns["Id"].Visible = false;
                }
            }


            if (comboBox1.SelectedItem.ToString() == "No Filter" || string.IsNullOrEmpty(comboBox1.Text))
            { LoadData(); }


        }

   
    }
}

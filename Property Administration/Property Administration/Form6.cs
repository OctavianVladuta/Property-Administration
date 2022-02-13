using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSQL_Estate;
using DSQL_Picture;

namespace Property_Administration
{
    public partial class Form6 : Form
    {
        internal List<Picture> list = new List<Picture>();
        int stop;
        public Form6()
        {
            InitializeComponent();
        }

        public Form6(Form4 fr, Form1 fr1)
        {
            InitializeComponent();
            frm = fr;
            frm1 = fr1;

            comboBoxType.Items.Add("Apartament");
            comboBoxType.Items.Add("Casa");
            comboBoxType.Items.Add("Teren");
            comboBoxType.Items.Add("Birouri");
        }

        Form4 frm = new Form4();
        Form1 frm1 = new Form1();

        private void editButton_Click(object sender, EventArgs e)
        {
            var y = new DbSqlServerEstate();
            var x1 = new DbSqlServerPicture();

            var a = frm1.dgvOwners.SelectedRows[0];
            var b1 = frm1.listaproprietari.SearchbyId(int.Parse(a.Cells[0].Value.ToString()));
            var x = frm.dgvEstates.SelectedRows[0];
            var z = b1.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));


            z.Name = textBoxName.Text;
            z.Address = textBoxAdress.Text;
            try
            {
                z.Price = int.Parse(textBoxPrice.Text);
            }
            catch
            {
                MessageBox.Show("Introduceti date corecte pentru campul Pret");
                return;
            }
            z.Type = comboBoxType.Text;
            z.CreateDate = dateTimePicker1.Value.ToString();




            int idpicture()
            {
                bool no;
                for (int i = 1; i <= Form5.listaphoto.pictureList.Count + 1; i++)
                {
                    no = false;
                    foreach (var aux in Form5.listaphoto.pictureList)
                    {
                        if (aux.Id == i)
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


            var list = new DbSqlServerPicture().GetAll(z.Id);
            foreach (var m in comboBoxPictures.Items)
            {
                stop = 0;

                foreach (var n in list)
                    if (m.ToString() == n.Location)
                    {
                        stop = 1;
                        break;
                    }

                if (stop == 0)
                {

                    Picture picture = new Picture();
                    picture.Id = idpicture();
                    picture.Name = Path.GetFileName(m.ToString());
                    picture.EstateId = z.Id;


                    var directory = ConfigurationManager.AppSettings["UserDataPath"];
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);
                    var path = directory + $"\\{z.Id}";
                    Directory.CreateDirectory(path);

                    Bitmap b = new Bitmap(m.ToString());


                    if (!File.Exists(path + $"\\\\{picture.Name}"))

                    {
                        b.Save(path + $"\\\\{picture.Name}");
                    }

                    picture.Location = path + $"\\\\{picture.Name}";

                    z.Pictures.Add(picture);
                    Form5.listaphoto.Add(picture);


                    x1.Create(picture);

                }
            }


            y.Update(z);
            if (frm.comboBox1.SelectedIndex == 0)
                frm.LoadData();
            else
            {
                if (frm.comboBox1.SelectedIndex == 1)
                {
                    frm.adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + b1.Id + "'" + "and pret between '" + PriceFilter.pricemin + "' and" + "'" + PriceFilter.pricemax + "'", frm.connstring);


                    frm.dt = new DataTable();

                    frm.adpt.Fill(frm.dt);

                    frm.dgvEstates.DataSource = frm.dt;

                    frm.dgvEstates.DataSource = frm.dt;
                    frm.dt.Columns.Remove("OwnerId");
                    frm.dgvEstates.Columns["Id"].Visible = false;
                }

                else if (frm.comboBox1.SelectedIndex == 2)
                {
                    frm.adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + b1.Id + "'" + "and Tip='" + TypeFilter.type + "'", frm.connstring);
                    frm.dt = new DataTable();

                    frm.adpt.Fill(frm.dt);

                    frm.dgvEstates.DataSource = frm.dt;

                    frm.dgvEstates.DataSource = frm.dt;
                    frm.dt.Columns.Remove("OwnerId");
                    frm.dgvEstates.Columns["Id"].Visible = false;
                }
                else if (frm.comboBox1.SelectedIndex == 3)
                {
                    frm.adpt = new SqlDataAdapter("select * from Estates where OwnerId='" + b1.Id + "'" + "and Data_Fabricatiei between '" + YearFilter.datamin + "' and" + "'" + YearFilter.datamax + "'", frm.connstring);
                    frm.dt = new DataTable();

                    frm.adpt.Fill(frm.dt);


                    frm.dgvEstates.DataSource = frm.dt;

                    frm.dgvEstates.DataSource = frm.dt;
                    frm.dt.Columns.Remove("OwnerId");
                    frm.dgvEstates.Columns["Id"].Visible = false;
                }
                else if (frm.comboBox1.SelectedIndex == 4)
                {
                    SqlDataReader rdr;
                    int stop = 0;
                    frm.dt = new DataTable();
                    foreach (var estate in b1.Estates)
                    {
                        SqlCommand cmd = new SqlCommand();
                        SqlConnection con = new SqlConnection(frm.connstring);
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        cmd.CommandText = "select count(*) as Poze from Pictures where EstateId =" + estate.Id;
                        con.Open();
                        rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            if (int.Parse(rdr["Poze"].ToString()) > 0)
                            {
                                frm.adpt = new SqlDataAdapter("select * from Estates where Id=" + estate.Id, frm.connstring);

                                frm.adpt.Fill(frm.dt);

                                frm.dgvEstates.DataSource = frm.dt;

                                frm.dgvEstates.DataSource = frm.dt;
                                frm.dt.Columns.Remove("OwnerId");
                                frm.dgvEstates.Columns["Id"].Visible = false;
                                stop = 1;
                            }

                        }
                    }
                    if (stop == 0)
                    {
                        frm.adpt = new SqlDataAdapter("select *from Estates where Id=-1000", frm.connstring);
                        frm.dt = new DataTable();
                        frm.adpt.Fill(frm.dt);

                        frm.dgvEstates.DataSource = frm.dt;

                        frm.dgvEstates.DataSource = frm.dt;
                        frm.dt.Columns.Remove("OwnerId");
                        frm.dgvEstates.Columns["Id"].Visible = false;
                    }
                }
            }
            list.Clear();
            MessageBox.Show("Edited");
            this.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            label7.Visible = false;


            try
            {
                var a1 = frm1.dgvOwners.SelectedRows[0];
                var b1 = frm1.listaproprietari.SearchbyId(int.Parse(a1.Cells[0].Value.ToString()));
                var c = frm.dgvEstates.SelectedRows[0];
                var d = b1.SearchbyId(int.Parse(c.Cells[0].Value.ToString()));

                d.Pictures.Clear();
                foreach (var m in new DbSqlServerPicture().GetAll(d.Id))
                    d.Pictures.Add(m);

                foreach (var val in d.Pictures)
                    comboBoxPictures.Items.Add(val.Location.ToString());






                var a = frm1.dgvOwners.SelectedRows[0];
                var b = frm1.listaproprietari.SearchbyId(int.Parse(a.Cells[0].Value.ToString()));
                var x = frm.dgvEstates.SelectedRows[0];
                var z = b.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));
                this.Text = $"Editare proprietate ID:{z.Id}";

                CultureInfo provider = CultureInfo.InvariantCulture;
                textBoxName.Text = z.Name;
                textBoxAdress.Text = z.Address;
                textBoxPrice.Text = z.Price.ToString();
                comboBoxType.Text = z.Type;
                dateTimePicker1.Value = DateTime.Parse(z.CreateDate);
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



            try
            {
                pictureBox1.ImageLocation = comboBoxPictures.Items[0].ToString();

                comboBoxPictures.SelectedIndex = 0;
            }
            catch { }

        }

        private void browseImageButton_Click(object sender, EventArgs e)
        {
            label7.Visible = false;
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| JPEG files(*.jpeg)|*.jpeg| PNG files(*.png)|*.png| All Files(*.*)|*.*";
                try
                {
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //estate.Pictures.Add(dialog.FileName);
                        
                        comboBoxPictures.Items.Add(dialog.FileName);
                        pictureBox1.ImageLocation = dialog.FileName;
                        comboBoxPictures.SelectedIndex = comboBoxPictures.Items.Count - 1;
                        MessageBox.Show("You added " + (comboBoxPictures.Items.Count) + " photo");

                    }
                }
                catch { }
            }
            catch { MessageBox.Show("An Error Ocurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); };
        }

        private void comboBoxPictures_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = $"{comboBoxPictures.SelectedItem}";
        }

        private void deletePictureButton_Click(object sender, EventArgs e)
        {
            var y = new DbSqlServerPicture();
            var path = $"{comboBoxPictures.SelectedItem}";
            var a = frm1.dgvOwners.SelectedRows[0];
            var b = frm1.listaproprietari.SearchbyId(int.Parse(a.Cells[0].Value.ToString()));
            var x = frm.dgvEstates.SelectedRows[0];
            var z = b.SearchbyId(int.Parse(x.Cells[0].Value.ToString()));

            foreach (var val in z.Pictures)
                if (val.Location == path)
                {
                    File.Delete(path);
                    Form5.listaphoto.pictureList.Remove(val);
                    z.Pictures.Remove(val);

                    y.Delete1(val.Id); //y.Delete(z.Id);

                    break;

                }



            //adphoto.photoList.Remove($"{comboBoxPhoto.SelectedItem}");

            comboBoxPictures.Items.Remove(comboBoxPictures.SelectedItem);
            comboBoxPictures.SelectedIndex = comboBoxPictures.Items.Count - 1;

            if (comboBoxPictures.Items.Count == 0)
            {
                comboBoxPictures.Text = "No images";
                label7.Visible = true;
            }
            try
            {
                pictureBox1.ImageLocation = $"{ comboBoxPictures.Items[comboBoxPictures.SelectedIndex]}";
            }
            catch
            {
                pictureBox1.Image = null;
                return;
            };
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            label7.Visible = false;
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as string[];
                if (fileNames.Length > 0)
                {
                    pictureBox1.Image = Image.FromFile(fileNames[0]);
                    comboBoxPictures.Items.Add(fileNames[0]);
                    comboBoxPictures.SelectedIndex = comboBoxPictures.Items.Count - 1;
                    MessageBox.Show("You added " + (comboBoxPictures.Items.Count) + " photo");
                }
            }
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}

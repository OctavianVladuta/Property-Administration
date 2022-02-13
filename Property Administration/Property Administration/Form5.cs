using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
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
    public partial class Form5 : Form
    {
        internal static ListPhoto listaphoto = new ListPhoto();

        public Form5()
        {
            InitializeComponent();
        }

        public Form5(Form4 fr, Form1 fr1)
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


        public int id()
        {
            bool no;
            for (int i = 1; i <= frm.listaproprietati.estateList.Count + 1; i++)
            {
                no = false;
                foreach (var z in frm.listaproprietati.estateList)
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
            var x = new DbSqlServerEstate(); //IRepository nu uita!!!
            var x1 = new DbSqlServerPicture();

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
            if (string.IsNullOrEmpty(textBoxAdress.Text))
            {
                MessageBox.Show("Adresa trebuie completat");
                return;
            }

            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Pretul trebuie completat");
                return;
            }
            if (string.IsNullOrEmpty(comboBoxType.Text))
            {
                MessageBox.Show("Tipul trebuie completat");
                return;
            }

            Estate estate = new Estate();

            var dgv = frm1.dgvOwners.SelectedRows[0];
            var z = frm1.listaproprietari.SearchbyId(int.Parse(dgv.Cells[0].Value.ToString()));



            estate.Id = id();
            var y = z.SearchbyId(estate.Id);
            if (y != null)
            {
                MessageBox.Show("Exista deja o proprietate cu acest Id" + " " + frm.listaproprietati.estateList.Count);
                foreach (var alpa in frm.listaproprietati.estateList)
                    MessageBox.Show(alpa.ToString());
                return;
            }
            estate.Name = textBoxName.Text;
            estate.Address = textBoxAdress.Text;
            try
            {
                estate.Price = int.Parse(textBoxPrice.Text);
            }
            catch
            {
                MessageBox.Show("Introduceti date corecte pentru campul Pret");
                return;
            }
            estate.Type = comboBoxType.Text;
            estate.CreateDate = dateTimePicker1.Value.ToString();

            estate.OwnerId = z.Id;



            int idpicture()
            {
                bool no;
                for (int i = 1; i <= listaphoto.pictureList.Count + 1; i++)
                {
                    no = false;
                    foreach (var aux in listaphoto.pictureList)
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


            foreach (var m in comboBoxPictures.Items)
            {
                Picture picture = new Picture();
                picture.Id = idpicture();
                picture.Name = Path.GetFileName(m.ToString());
                picture.EstateId = estate.Id;


                var directory = ConfigurationManager.AppSettings["UserDataPath"];
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                var path = directory + $"\\{estate.Id}";
                Directory.CreateDirectory(path);

                Bitmap b = new Bitmap(m.ToString());


                if (!File.Exists(path + $"\\\\{picture.Name}"))

                {
                    b.Save(path + $"\\\\{picture.Name}");
                }

                picture.Location = path + $"\\\\{picture.Name}";

                estate.Pictures.Add(picture);
                listaphoto.Add(picture);


                x1.Create(picture);

            }





            z.Estates.Add(estate);
            //MessageBox.Show(z.Estates[z.Estates.Count-1].Pictures[0].Location);
            frm.listaproprietati.Add(estate);
            x.Create(estate);
            frm.LoadData();

            // frm.dgvEstates.Rows.Add(owner.Id, owner.Name, owner.Email, owner.Phone, owner.Cnp);


            var a = frm1.dgvOwners.SelectedRows[0];
            var b1 = frm1.listaproprietari.SearchbyId(int.Parse(a.Cells[0].Value.ToString()));



            var xy = frm.dgvEstates.SelectedRows[0];
            var z1 = b1.SearchbyId(int.Parse(xy.Cells[0].Value.ToString()));



            z1.Pictures.Clear();
            foreach (var m in new DbSqlServerPicture().GetAll(z.Id))
                z1.Pictures.Add(m);

            MessageBox.Show("Added");
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

            this.Text = "Adăugare proprietate";
            pictureBox1.AllowDrop = true;
            frm.comboBox1.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Today;

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

        private void browseImageButton_Click(object sender, EventArgs e)
        {
            label7.Visible = false;
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| JPEG files(*jpeg)|*.jpeg| PNG files(*.png)|*.png| All Files(*.*)|*.*";
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

            var path = $"{comboBoxPictures.SelectedItem}";


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
    }
}

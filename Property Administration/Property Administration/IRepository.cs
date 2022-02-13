using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Property;
using Property_Administration;

namespace Property
{
    interface IRepository<T>
    {
        void GetById(int id);
        List<T> GetAll();
        void Create(T value); //creaza un nou obiect T in baza de date
        void Delete(int id);
        void Update(T value); //se face update (modificare) a obiectului T
    }
}

namespace DSQL_Owner
{
    internal class DbSqlServerOwner : IRepository<Owner>
    {
        string connstring = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
        public static SqlConnection con;
        public DbSqlServerOwner()
        {
            con = new SqlConnection(connstring);
        }

        public void Create(Owner owner)
        {
            string strInsert =
            "insert into Owners values ("
            + "'" + owner.Id + "', '" + owner.Name + "', '" + owner.Email + "','" + owner.Phone + "','" + owner.Cnp +
           "')";
            try
            {
                SqlCommand cmd = new SqlCommand(strInsert, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            { Console.WriteLine("Err: Inserare esuata\n" + strInsert); }

            con.Close();

        }
        public void Delete(int id)
        {
            string strDelete = "delete from Owners where ID=" + id;
            // +"'";
            int n;
            try
            {
                SqlCommand cmd = new SqlCommand(strDelete, con);
                con.Open();
                n = cmd.ExecuteNonQuery();
                con.Close();
                if (n == 0) throw new Exception("Id negasit");
            }
            catch (Exception e)
            {
                MessageBox.Show("Err: Stergere esuata\n"
                + strDelete + "\n" + e.ToString());
            };
        }

        internal void Create(Estate estate)
        {
            throw new NotImplementedException();
        }

        public void Update(Owner owner)
        {
            string strUpdate;
            int n;
            SqlCommand cmd;

            strUpdate = "update Owners set Nume= '" +   //Name
            owner.Name + "' where ID='" + owner.Id + "'";
            try
            {
                cmd = new SqlCommand(strUpdate, con);
                con.Open();
                n = cmd.ExecuteNonQuery(); //nr randuri afectate
                if (n == 0) throw new Exception("Id inexistent");
            }
            catch (Exception e)
            {
                Console.WriteLine("Err: Modificare esuata\n" +
                strUpdate + "\n" + e.ToString());
            }



            strUpdate = "update Owners set Email= '" +       //Email
           owner.Email + "' where ID='" + owner.Id + "'";
            try
            {
                cmd = new SqlCommand(strUpdate, con);

                n = cmd.ExecuteNonQuery(); //nr randuri afectate
                if (n == 0) throw new Exception("Id inexistent");
            }
            catch (Exception e)
            {
                Console.WriteLine("Err: Modificare esuata\n" +
                strUpdate + "\n" + e.ToString());
            }



            strUpdate = "update Owners set Telefon= '" +       //Phone
            owner.Phone + "' where ID='" + owner.Id + "'";
            try
            {
                cmd = new SqlCommand(strUpdate, con);

                n = cmd.ExecuteNonQuery(); //nr randuri afectate
                if (n == 0) throw new Exception("Id inexistent");
            }
            catch (Exception e)
            {
                Console.WriteLine("Err: Modificare esuata\n" +
                strUpdate + "\n" + e.ToString());
            }




            strUpdate = "update Owners set Cnp= '" +       //CNP
            owner.Cnp + "' where ID='" + owner.Id + "'";
            try
            {
                cmd = new SqlCommand(strUpdate, con);

                n = cmd.ExecuteNonQuery(); //nr randuri afectate
                if (n == 0) throw new Exception("Id inexistent");
            }
            catch (Exception e)
            {
                Console.WriteLine("Err: Modificare esuata\n" +
                strUpdate + "\n" + e.ToString());
            }


            con.Close();
        }



        public List<Owner> GetAll()
        {
            SqlDataReader rdr;
            List<Owner> list = new List<Owner>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;// implicit
            cmd.Connection = con;
            cmd.CommandText = "select * from Owners";
            con.Open();
            rdr = cmd.ExecuteReader();



            while (rdr.Read())
            {
                Owner owner = new Owner();
                owner.Id = (int)rdr["Id"];
                owner.Name = (string)rdr["Nume"];
                owner.Email = (string)rdr["Email"];
                owner.Phone = (string)rdr["Telefon"];
                owner.Cnp = (string)rdr["Cnp"];
                list.Add(owner);
            }


            rdr.Close();
            con.Close();
            return list;
        }

        public void GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

namespace DSQL_Estate
{
    internal class DbSqlServerEstate : IRepository<Estate>
    {
        string connstring = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
        public static SqlConnection con;
        public DbSqlServerEstate()
        {
            con = new SqlConnection(connstring);
        }

        public void Create(Estate estate)
        {
            string strInsert =
            "insert into Estates values ("
            + "'" + estate.Id + "', '" + estate.Name + "', '" + estate.Address + "','" + estate.OwnerId + "','" + estate.Price + "','" + estate.Type + "','" + estate.CreateDate +
           "')";
            try
            {
                SqlCommand cmd = new SqlCommand(strInsert, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            { Console.WriteLine("Err: Inserare esuata\n" + strInsert); }

            con.Close();

        }
        public void Delete(int id)
        {
            string strDelete = "delete from Estates where ID=" + id;
            // +"'";
            int n;
            try
            {
                SqlCommand cmd = new SqlCommand(strDelete, con);
                con.Open();
                n = cmd.ExecuteNonQuery();
                con.Close();
                if (n == 0) throw new Exception("Id negasit");
            }
            catch (Exception e)
            {
                MessageBox.Show("Err: Stergere esuata\n"
                + strDelete + "\n" + e.ToString());
            };
        }
        public void Update(Estate estate)
        {
            string strUpdate;
            int n;
            SqlCommand cmd;

            strUpdate = "update Estates set Nume= '" +   //Name
            estate.Name + "' where ID='" + estate.Id + "'";
            try
            {
                cmd = new SqlCommand(strUpdate, con);
                con.Open();
                n = cmd.ExecuteNonQuery(); //nr randuri afectate
                if (n == 0) throw new Exception("Id inexistent");
            }
            catch (Exception e)
            {
                Console.WriteLine("Err: Modificare esuata\n" +
                strUpdate + "\n" + e.ToString());
            }



            strUpdate = "update Estates set Adresa= '" +       //Adresa
           estate.Address + "' where ID='" + estate.Id + "'";
            try
            {
                cmd = new SqlCommand(strUpdate, con);

                n = cmd.ExecuteNonQuery(); //nr randuri afectate
                if (n == 0) throw new Exception("Id inexistent");
            }
            catch (Exception e)
            {
                Console.WriteLine("Err: Modificare esuata\n" +
                strUpdate + "\n" + e.ToString());
            }



            strUpdate = "update Estates set Pret= '" +       //Pret
            estate.Price + "' where ID='" + estate.Id + "'";
            try
            {
                cmd = new SqlCommand(strUpdate, con);

                n = cmd.ExecuteNonQuery(); //nr randuri afectate
                if (n == 0) throw new Exception("Id inexistent");
            }
            catch (Exception e)
            {
                Console.WriteLine("Err: Modificare esuata\n" +
                strUpdate + "\n" + e.ToString());
            }




            strUpdate = "update Estates set Tip= '" +       //Tip
            estate.Type + "' where ID='" + estate.Id + "'";
            try
            {
                cmd = new SqlCommand(strUpdate, con);

                n = cmd.ExecuteNonQuery(); //nr randuri afectate
                if (n == 0) throw new Exception("Id inexistent");
            }
            catch (Exception e)
            {
                Console.WriteLine("Err: Modificare esuata\n" +
                strUpdate + "\n" + e.ToString());
            }



            strUpdate = "update Estates set Data_Fabricatiei= '" +       //Data
            estate.CreateDate + "' where ID='" + estate.Id + "'";
            try
            {
                cmd = new SqlCommand(strUpdate, con);

                n = cmd.ExecuteNonQuery(); //nr randuri afectate
                if (n == 0) throw new Exception("Id inexistent");
            }
            catch (Exception e)
            {
                Console.WriteLine("Err: Modificare esuata\n" +
                strUpdate + "\n" + e.ToString());
            }


            con.Close();
        }



        public List<Estate> GetAll()
        {
            SqlDataReader rdr;
            List<Estate> list = new List<Estate>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;// implicit
            cmd.Connection = con;
            cmd.CommandText = "select * from Estates";
            con.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {

                Estate estate = new Estate();
                estate.Id = (int)rdr["Id"];
                estate.Name = (string)rdr["Nume"];
                estate.Address = (string)rdr["Adresa"];
                estate.OwnerId = (int)rdr["OwnerId"];
                estate.Price = (int)rdr["Pret"];
                estate.Type = (string)rdr["Tip"];
                estate.CreateDate = rdr["Data_Fabricatiei"].ToString();

                list.Add(estate);
            }


            rdr.Close();
            con.Close();
            return list;
        }


        public List<Estate> GetAllbyPrice(int pricemin, int pricemax)
        {
            SqlDataReader rdr;
            List<Estate> list = new List<Estate>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;// implicit
            cmd.Connection = con;
            cmd.CommandText = "select * from Estates where pret between '" + pricemin + "' and" + "'" + pricemax + "'";
            con.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {

                Estate estate = new Estate();
                estate.Id = (int)rdr["Id"];
                estate.Name = (string)rdr["Nume"];
                estate.Address = (string)rdr["Adresa"];
                estate.OwnerId = (int)rdr["OwnerId"];
                estate.Price = (int)rdr["Pret"];
                estate.Type = (string)rdr["Tip"];
                estate.CreateDate = (string)rdr["Data_Fabricatiei"];

                list.Add(estate);
            }


            rdr.Close();
            con.Close();
            return list;
        }

        public List<Estate> GetAll(int id)
        {
            SqlDataReader rdr;
            List<Estate> list = new List<Estate>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;// implicit
            cmd.Connection = con;

            cmd.CommandText = "select * from Estates where OwnerId='" + id + "'";
            con.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {

                Estate estate = new Estate();
                estate.Id = (int)rdr["Id"];
                estate.Name = (string)rdr["Nume"];
                estate.Address = (string)rdr["Adresa"];
                estate.OwnerId = (int)rdr["OwnerId"];
                estate.Price = (int)rdr["Pret"];
                estate.Type = (string)rdr["Tip"];
                estate.CreateDate = rdr["Data_Fabricatiei"].ToString();

                list.Add(estate);
            }


            rdr.Close();
            con.Close();
            return list;
        }

        public void GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
namespace DSQL_Picture
{
    internal class DbSqlServerPicture : IRepository<Picture>
    {
        string connstring = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
        public static SqlConnection con;
        public DbSqlServerPicture()
        {
            con = new SqlConnection(connstring);
        }

        public void Create(Picture picture)
        {
            string strInsert =
            "insert into Pictures values ("
            + "'" + picture.Id + "', '" + picture.Name + "', '" + picture.Location + "', '" + picture.EstateId +
           "')";
            try
            {
                SqlCommand cmd = new SqlCommand(strInsert, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            { Console.WriteLine("Err: Inserare esuata\n" + strInsert); }

            con.Close();

        }
        public void Delete(int id)
        {
            string strDelete = "delete from Pictures where EstateId=" + id;
            // +"'";
            int n;
            try
            {
                SqlCommand cmd = new SqlCommand(strDelete, con);
                con.Open();
                n = cmd.ExecuteNonQuery();
                con.Close();
                if (n == 0) throw new Exception("Id negasit");
            }
            catch (Exception e)
            {
                MessageBox.Show("Err: Stergere esuata\n"
                + strDelete + "\n" + e.ToString());
            };
        }


        public void Delete1(int id)
        {
            string strDelete = "delete from Pictures where Id=" + id;
            // +"'";
            int n;
            try
            {
                SqlCommand cmd = new SqlCommand(strDelete, con);
                con.Open();
                n = cmd.ExecuteNonQuery();
                con.Close();
                if (n == 0) throw new Exception("Id negasit");
            }
            catch (Exception e)
            {
                MessageBox.Show("Err: Stergere esuata\n"
                + strDelete + "\n" + e.ToString());
            };
        }


        public void GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Picture> GetAll()
        {
            SqlDataReader rdr;
            List<Picture> list = new List<Picture>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;// implicit
            cmd.Connection = con;
            cmd.CommandText = "select * from Pictures";
            con.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {

                Picture picture = new Picture();
                picture.Id = (int)rdr["Id"];
                picture.Name = (string)rdr["_Name"];
                picture.Location = (string)rdr["_Location"];
                picture.EstateId = (int)rdr["EstateId"];

                list.Add(picture);
            }


            rdr.Close();
            con.Close();
            return list;
        }



        public List<Picture> GetAll(int id)
        {
            SqlDataReader rdr;
            List<Picture> list = new List<Picture>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;// implicit
            cmd.Connection = con;
            cmd.CommandText = "select * from Pictures where EstateId='" + id + "'";
            con.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {

                Picture picture = new Picture();
                picture.Id = (int)rdr["Id"];
                picture.Name = (string)rdr["_Name"];
                picture.Location = (string)rdr["_Location"];
                picture.EstateId = (int)rdr["EstateId"];

                list.Add(picture);
            }


            rdr.Close();
            con.Close();
            return list;
        }


        public void Update(Picture value)
        {
            throw new NotImplementedException();
        }
    }
}

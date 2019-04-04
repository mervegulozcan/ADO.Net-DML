using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Connected_Update_Delete
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(Tools.ConnectionString);

        private void Form1_Load(object sender, EventArgs e)
        {
            KategoriDoldur();

        }

        private void KategoriDoldur()
        {
            SqlCommand cmd = new SqlCommand("select CategoryID,CategoryName,Description from Categories", con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                cmbCategories.Items.Clear();

                while (rdr.Read())
                {
                    cmbCategories.Items.Add(rdr["CategoryName"]);
                }
            }
            con.Close();

        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCategories.Text = cmbCategories.SelectedItem.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCategories.Text == "" || cmbCategories.SelectedItem.ToString() == "")
            {
                errorProvider1.SetError(txtCategories, "Bu alan boş geçilemez");
                errorProvider1.SetError(cmbCategories, "Bir kategori seçiniz");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Update Categories set CategoryName=@catName where CategoryName=@name ", con);
                cmd.Parameters.AddWithValue("@catName", txtCategories.Text);
                cmd.Parameters.AddWithValue("@name", cmbCategories.SelectedItem.ToString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int etkilenen = cmd.ExecuteNonQuery();
                if (etkilenen > 0)
                {
                    MessageBox.Show("Güncelleme işlemi başaralı");
                }
                else
                {
                    MessageBox.Show("Güncelleme işlemi başarısız");
                }
                con.Close();
                KategoriDoldur();
                txtCategories.Text = "";
                cmbCategories.Text = "";
                errorProvider1.Clear();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           

            if (cmbCategories.Text != "")
            {
                
                SqlCommand cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryName=@catName", con);
                cmd.Parameters.AddWithValue("@catName", cmbCategories.SelectedItem.ToString());
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int etkilenen = cmd.ExecuteNonQuery();
                if (etkilenen > 0)
                {
                    MessageBox.Show("Silme işlemi başaralı");
                }
                else
                {
                    MessageBox.Show("Silme işlemi başarısız");
                }
                con.Close();
                KategoriDoldur();
                txtCategories.Text = "";
                cmbCategories.Text = "";
                errorProvider1.Clear();

            }
            else
            {
                errorProvider1.SetError(cmbCategories, "Bir kategori seçiniz");
            }


        }
        }
}

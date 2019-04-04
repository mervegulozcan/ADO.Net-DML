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
using System.Configuration;

namespace Connected_Insert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(Tools.Connectionstring);
        SqlCommand cmd;
        int etkilenenSatirSayisi=0;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            // or 'a'='a sorgu bu şekilde sql injection'a  açıktır.Bu aşamada bu vurgulanmalıdır.

            //SQL INJECTION 
            //SqlCommand cmd = new SqlCommand("INSERT INTO Categories (CategoryName,Description) values "('"+txtCategory.Text+"','"+txtDescription.Text+"')",con);

            //Burada Drop Databese,Drop Table gibi komutlar gösterilebilir.

            try
            {
                if (txtCategory.Text != "")
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO Categories(CategoryName, Description) values (@catName,@desc) Select  cast (scope_identity() as int)", con);
                    cmd.Parameters.AddWithValue("@catName", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
                    int categoryID = (int)cmd.ExecuteScalar();
                    lblID.Text = categoryID.ToString();

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            if (con.State == ConnectionState.Open) con.Close();

            //con.Close();

            //if (etkilenenSatirSayisi > 0)
            //{
            //    MessageBox.Show("Kayıt başarıyla eklenmiştir.");
            //}
            //else
            //{
            //    MessageBox.Show("Kayıt eklenemedi.");
            //}


        }

        private void btnSil_Click(object sender, EventArgs e)
        {

        }

        private void btnInsertSp_Click(object sender, EventArgs e)
        {
            int donendeger =0;
            try
            {
                if(txtCategory.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("SP_INSERTCAT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@catName", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@catDesc", txtDescription.Text);
                    con.Open();
                    donendeger = cmd.ExecuteNonQuery();
                    if(donendeger>0)
                    {
                        MessageBox.Show("Kayıt başarılı.");
                    
                    }
                    else
                    {
                        MessageBox.Show("Kayıt başarısız.");
                    }
                }
            }
            catch (Exception ex )
            {

                throw new Exception("Bir hata oluştu " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
    }
}

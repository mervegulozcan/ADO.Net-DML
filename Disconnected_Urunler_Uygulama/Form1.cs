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

namespace Disconnected_Urunler_Uygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);
        SqlCommand cmd;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGoster_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Products", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnIDGetir_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                SqlDataAdapter adp = new SqlDataAdapter("Select * from Products where ProductID=@ID", con);
                adp.SelectCommand.Parameters.AddWithValue("@ID", txtID.Text);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView1.DataSource = dt;
            }

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("insert into Products(ProductName,UnitsInStock,UnitPrice) values (@ad,@stok,@fiyat)", con);
            cmd.Parameters.AddWithValue("@ad", txtAd.Text);
            cmd.Parameters.AddWithValue("@stok", txtStok.Text);
            cmd.Parameters.AddWithValue("@fiyat", txtFiyat.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            txtAd.Text = "";
            txtStok.Text = "";
            txtFiyat.Text = "";
            btnGoster_Click(sender, e);
            MessageBox.Show("Yeni ürün eklendi");
        }
    }
}

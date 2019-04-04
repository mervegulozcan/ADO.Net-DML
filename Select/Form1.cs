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

namespace Select
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection();

        private void btnGetir_Click(object sender, EventArgs e)
        {
            lstEmployees.Items.Clear();
            conn.ConnectionString = Tools.Connectionstring;
            SqlCommand cmd = new SqlCommand("select *  from Employees", conn);
            conn.Open();
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    string FirstName = rdr["FirstName"].ToString();
                    string LastName = rdr["LastName"].ToString();
                    string BirthDate = rdr["BirthDate"].ToString();
                    lstEmployees.Items.Add(FirstName + "  " + LastName + "  " + BirthDate);
                }
            }
            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

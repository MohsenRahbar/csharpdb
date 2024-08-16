using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Class5_connecttosqlserver2017
{
    public partial class Form1 : Form
    {
        SqlConnection sqlcon = new SqlConnection("Data Source=.;Initial Catalog=dbtest;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }
        public void getlist()
        {
            SqlDataAdapter sqlDa = new SqlDataAdapter("getlistStudent",sqlcon);
            DataSet DS = new DataSet();
            sqlDa.Fill(DS, "T1");
            dataGridView1.DataSource = DS.Tables["T1"];
            //_______________________________________________

            dataGridView1.Columns[0].HeaderText = "کد";
            dataGridView1.Columns[1].HeaderText = "اسم";
            dataGridView1.Columns[2].HeaderText = "فامیلی";
        }
        public void cleartext()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox1.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getlist();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "insertStudent";
            sqlcmd.Parameters.Add("@Id", SqlDbType.Char, 4).Value = textBox1.Text;
            sqlcmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = textBox2.Text;
            sqlcmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = textBox3.Text;
            sqlcmd.Parameters.Add("@R", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
            if (sqlcmd.Parameters["@R"].Value.ToString()=="1")
            {
                MessageBox.Show("inset sucess");
                getlist();
                cleartext();
            }
            else
            {
                MessageBox.Show("cod you exiest");
            }
            sqlcon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "deletestudent";
            sqlcmd.Parameters.Add("@ID", SqlDbType.Char, 4).Value = textBox1.Text;
            sqlcmd.Parameters.Add("@R", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
            if (sqlcmd.Parameters["@R"].Value.ToString() == "1")
            {
                MessageBox.Show("Delet Sucsse");
                getlist();
                cleartext();
            }
            else
            {
                MessageBox.Show("cod you not exiest");
            }
            sqlcon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "updatstudent";
            sqlcmd.Parameters.Add("@Id", SqlDbType.Char, 4).Value = textBox1.Text;
            sqlcmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = textBox2.Text;
            sqlcmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = textBox3.Text;
            sqlcmd.Parameters.Add("@R", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

            sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
            if (sqlcmd.Parameters["@R"].Value.ToString() == "0")
            {
                MessageBox.Show("cod you exiest");
            }
            else
            {
                MessageBox.Show("update sucess");
                getlist();
                cleartext();
            }
            sqlcon.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cleartext();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "getnamestudent";
            sqlcmd.Parameters.Add("ID", SqlDbType.Char, 4).Value = textBox1.Text;
            sqlcmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
            sqlcmd.Parameters.Add("@R", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
            if (sqlcmd.Parameters["@R"].Value.ToString()=="1")
            {
                string strname = sqlcmd.Parameters["@Name"].Value.ToString();
                MessageBox.Show(strname);
            }
            else
            {
                MessageBox.Show("cod you not exits");
            }
            sqlcon.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "getferrcod";
            sqlcmd.Parameters.Add("@R", SqlDbType.Char, 4).Direction = ParameterDirection.ReturnValue;
            sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
            textBox1.Text = sqlcmd.Parameters["@R"].Value.ToString();
            sqlcon.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "saechbyname";
            sqlcmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = textBox2.Text;
            SqlDataAdapter sqlda = new SqlDataAdapter();
            sqlda.SelectCommand = sqlcmd;
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlcon;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "saechbylastname";
            sqlcmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = textBox3.Text;
            SqlDataAdapter sqlda = new SqlDataAdapter();
            sqlda.SelectCommand = sqlcmd;
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}


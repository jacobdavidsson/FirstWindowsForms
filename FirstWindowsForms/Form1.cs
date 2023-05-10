using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstWindowsForms
{
    public partial class Form1 : Form
    {

        MySqlConnection conn;

        // Test push 

        public Form1()
        {
            InitializeComponent();

            string server = "localhost";
            string database = "elever";
            string user = "root";            
            string pass = "EmmaWindows";  

           string connString  = $"SERVER={server}; DATABASE = {database}; UID={user}; PASSWORD={pass};";

            conn = new MySqlConnection(connString); 
        }



        // insert query
        private void Button_insert(object sender, EventArgs e)
        {
            // hämta text från textfält
            string strName = TxtName.Text;
            int intAge = Convert.ToInt32(TxtAge.Text);

            // skriv SQL insert statment  "c
            string strSql = $"INSERT INTO elever1(elever1_name, elever1_age) VALUES ('{ strName}', {intAge})";

            // skapa en mysqlcommand objekt
            MySqlCommand cmd = new MySqlCommand(strSql, conn);

            // öppna koppling till DB
            conn.Open();

            // skicka iväg SQL command till db
            cmd.ExecuteReader();

            // stänga koppling till db
            conn.Close();

            // bekräftelse till användare "insert completed" 
            MessageBox.Show("Data insert completed"); // pop-up box
        }


        // hämta data query
        private void Button_fetch(object sender, EventArgs e)
        {
            // skriva sql statment
            string strsql =  $"SELECT * FROM elever1";

            // skapa mysql command objekt
            MySqlCommand cmd = new MySqlCommand(strsql, conn);

            // nollställer objekt
            lblID.Text = "ID";
            lblName.Text = "Name";
            lblAge.Text = "Age";

            // öppnar koppling till db igen
            conn.Open();

            // exekvera kommando till db
            MySqlDataReader reader = cmd.ExecuteReader();

            // använder while loop för att läsa varje rad 

            while (reader.Read())
            {
                // så länge det finns rader att läsa är vi i while loopen

                // skriver ut id värde
                lblID.Text += Environment.NewLine + reader["elever1_id"];
                lblName.Text += Environment.NewLine + reader["elever1_name"];
                lblAge.Text += Environment.NewLine + reader["elever1_age"];

                

            }
            // stäng koppling

            conn.Close();           

        }


        // delete query
        private void Button_delete(object sender, EventArgs e)
        {
            string strId = textBox1.Text;
            string sql = $"DELETE FROM elever1 WHERE elever1_id = ({strId})";         
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            

            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Delete of student completed.");
        }


        // update query
        private void Button_update(object sender, EventArgs e)
        {
            string strId = textBox1.Text;
            string strName = TxtName.Text;
            int intAge = Convert.ToInt32(TxtAge.Text);

            string strSql = $"UPDATE elever1 SET elever1_name = '{strName}', elever1_age = {intAge} WHERE elever1_id = ({strId})";

            MySqlCommand cmd = new MySqlCommand(strSql, conn);

            conn.Open();
            cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Update completed"); // pop-up box
        }
    }
}

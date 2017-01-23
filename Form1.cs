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

namespace Magazyn
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-3USQUN7Q\\SQLEXPRESS;Integrated Security=True");

        public Form1()
        {

                InitializeComponent();

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Produkt";

                SqlDataAdapter adapterP = new SqlDataAdapter(command);

                DataSet ds = new DataSet();
                adapterP.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

                command.CommandText = "SELECT * FROM Klient";

                SqlDataAdapter adapterK = new SqlDataAdapter(command);

                DataSet dsK = new DataSet();

                adapterK.Fill(dsK);
                dataGridView2.DataSource = dsK.Tables[0];

                command.CommandText = "SELECT * FROM Zamowienie";

                SqlDataAdapter adapterZ = new SqlDataAdapter(command);

                DataSet dsZ = new DataSet();

                adapterZ.Fill(dsZ);
                dataGridView3.DataSource = dsZ.Tables[0];

                //command.CommandText = "SELECT * FROM Kategoria";

                //SqlDataAdapter adapterKat = new SqlDataAdapter(command);

                //DataSet dsKat = new DataSet();

                //adapterKat.Fill(dsKat);
                //dataGridView4.DataSource = dsKat.Tables[0];

                connection.Close();

        }


        //Dodaj produkt
        private void buttonPDodaj_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Produkt VALUES(@Nazwa, @Typ, @Producent, @CenaJednostkowa, @Ilosc)";

  
            command.Parameters.AddWithValue("Nazwa", textBoxPNazwa.Text);
            command.Parameters.AddWithValue("Typ", comboBoxPTyp.Text);
            command.Parameters.AddWithValue("Producent", textBoxPProducent.Text);
            command.Parameters.AddWithValue("CenaJednostkowa", textBoxPCenaJedn.Text);
            command.Parameters.AddWithValue("ilosc", textBoxPIlosc.Text);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            command.CommandText = "SELECT * FROM Produkt";

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }


        //Dodaj klienta
        private void buttonKDodaj_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Klient VALUES(@Nazwa, @Typ, @Rabat)";

            command.Parameters.AddWithValue("Nazwa", textBoxKNazwa.Text);
            command.Parameters.AddWithValue("Typ", comboBoxKTyp.Text);
            command.Parameters.AddWithValue("Rabat", numericUpDownKlient.Value);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            command.CommandText = "SELECT * FROM Klient";

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }


        //Dodaj zamowienie
        private void buttonZDodaj_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int selectedRowIndexP = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRowP = dataGridView1.Rows[selectedRowIndexP];
            int Produkt_ID = Convert.ToInt32(selectedRowP.Cells["Produkt_ID"].Value);

            int selectedRowIndexK = dataGridView2.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRowK = dataGridView2.Rows[selectedRowIndexK];
            int Klient_ID = Convert.ToInt32(selectedRowK.Cells["Klient_ID"].Value);
            command.CommandText = "INSERT INTO Zamowienie VALUES(@Nazwa, @Klient_ID, @Produkt_ID, @Ilosc)";
            command.Parameters.AddWithValue("Nazwa", textBox1.Text);
            command.Parameters.AddWithValue("Klient_ID", Klient_ID);
            command.Parameters.AddWithValue("Produkt_ID", Produkt_ID);
            command.Parameters.AddWithValue("Ilosc", numericUpDown1.Value);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            command.CommandText = "SELECT Z.Zamowienie_ID, P.Nazwa as Produkt, K.Nazwa FROM Klient as K FROM Zamowienie as Z, Klient as K, Produkt as P WHERE K.Klient_ID = Z.Klient_ID AND P.Produkt_ID = Z.Produkt_ID";

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
        }


    }
}

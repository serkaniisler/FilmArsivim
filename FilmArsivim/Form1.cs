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

namespace FilmArsivim
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-2FA3KGC;Initial Catalog=FilmArsivi;Integrated Security=True");

        void filmler()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from TBLFILMLER",baglanti);
            DataTable dt = new DataTable(); //Veri tablosu nesnesi , verileri hafızada tutmak için
            dataAdapter.Fill(dt); //doldur
            dataGridView1.DataSource = dt; //datagridwiev de göster
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            filmler();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLFILMLER(AD,KATEGORI,LINK) values (@P1,@P2,@P3)",baglanti);
            komut.Parameters.AddWithValue("@P1", txtFilmAd.Text);
            komut.Parameters.AddWithValue("@P2", txtKategori.Text);
            komut.Parameters.AddWithValue("@P3", txtLink.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Film listenize eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            filmler();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex; //Seçilen 0. hücreye göre satır indexini hafızaya al
            string link = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            webBrowser1.Navigate(link);

        }

        private void btnHakkimizda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu proje Serkan İşler tarafından 24 Nisan 2024 de kodlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

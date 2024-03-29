﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Urun_Takip
{
    public partial class FrmSatislar : Form
    {
        public FrmSatislar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-8C8KD0P\\SQLEXPRESS;Initial Catalog=DbUrun;Integrated Security=True");
        DataSet1TableAdapters.TBLSATISLARTableAdapter ds = new DataSet1TableAdapters.TBLSATISLARTableAdapter();

        private void BtnListele_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("Execute SatisListesi", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void FrmSatislar_Load(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("SELECT * FROM TBLURUNLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut2);
            DataTable dt2 = new DataTable();
            da.Fill(dt2);
            comboBox1.ValueMember = "UrunId";
            comboBox1.DisplayMember = "UrunAd";
            comboBox1.DataSource = dt2;


            dataGridView1.DataSource = ds.SatisListesi();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            ds.SatisEkle(int.Parse(comboBox1.SelectedValue.ToString()), int.Parse(TxtMusteri.Text), byte.Parse(TxtAdet.Text), decimal.Parse(TxtFiyat.Text), decimal.Parse(TxtToplam.Text), DataTime.Parse(MskTarih.Text));
            MessageBox.Show("Satış başarıyla yapıldı.");

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            double adet, fiyat, toplam;
            adet = Convert.ToDouble(TxtAdet.Text);
            fiyat = Convert.ToDouble(TxtFiyat.Text);
            toplam = Convert.ToDouble(TxtToplam.Text);
            toplam = adet * fiyat;
            TxtToplam.Text = toplam.ToString();
        }
    }
}

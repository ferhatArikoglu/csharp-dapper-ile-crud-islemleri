using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;

namespace dappercrud1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
        private string sql = "";
        void cud(DynamicParameters dynamic = null)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }

            sqlCon.Execute(sql, dynamic, commandType: CommandType.Text);
            sqlCon.Close();
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            dataGridView1.DataSource = sqlCon.Query<departmanlar>("select * from ogrencis");
        }

        void sqlConOpen()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
        }

        void sqlConClosed()
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sqlCon.Query<departmanlar>("select * from ogrencis");
        }

        private void btnekle_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cmbguncelle.Text))
            {

                MessageBox.Show("item seçiniz lütfen ");
            }
            else
            {
                if (string.IsNullOrEmpty(txtad.Text) || string.IsNullOrEmpty(txtsoyad.Text) || string.IsNullOrEmpty(txtbolum.Text))
                {
                    MessageBox.Show("texboxu boş bıraktınız lütfen boş kutuları doldurunuz");
                }
                else
                {
                    if (cmbguncelle.SelectedIndex == 0)
                    {
                        try
                        {
                            sqlConOpen();
                            DynamicParameters param = new DynamicParameters();

                            //param.Add("@Id", contactid);
                            param.Add("@OgrAd", txtad.Text.Trim());
                            param.Add("@OgrSoyad", txtsoyad.Text.Trim());
                            param.Add("@OgrBolum", txtbolum.Text.Trim());
                            sqlCon.Execute("Procedure3", param, commandType: CommandType.StoredProcedure);
                            cud();

                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            sqlConClosed();
                        }
                    }
                    else if (cmbguncelle.SelectedIndex == 1)
                    {
                        try
                        {
                            DynamicParameters param = new DynamicParameters();
                            param.Add("@OgrAd", txtad.Text.Trim());
                            param.Add("@OgrSoyad", txtsoyad.Text.Trim());
                            param.Add("@OgrBolum", txtbolum.Text.Trim());
                            sql = "insert into Ogrencis(OgrAd,OgrSoyad,OgrBolum)values(@OgrAd,@OgrSoyad,@OgrBolum)";
                            cud(param);

                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                            throw;
                        }
                    }
                }
            }

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConOpen();
                sql = "delete from Ogrencis where Id='" + int.Parse(txtid.Text) + "'";
                cud();
                sqlConClosed();
            }
            catch (Exception)
            {
                MessageBox.Show("id'yi girmediniz lütfen idyi giriniz");
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id",int.Parse(txtid.Text));
                param.Add("@OgrAd", txtad.Text.Trim());
                param.Add("@OgrSoyad", txtsoyad.Text.Trim());
                param.Add("@OgrBolum", txtbolum.Text.Trim());
                sql = "update Ogrencis set OgrAd=@OgrAd , OgrSoyad=@OgrSoyad,OgrBolum=@OgrSoyad where Id=@Id";
                cud(param);


            }
            catch (Exception exception)
            {
                MessageBox.Show(""+exception);
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DovuzKuru
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //!#! CHANGE THIS IN TWO PROJECTS
        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                XmlDocument xmlVerisi = new XmlDocument();
                xmlVerisi.Load("http://www.tcmb.gov.tr/kurlar/today.xml");

                decimal dolar = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "USD")).InnerText.Replace('.', ','));
                decimal euro = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "EUR")).InnerText.Replace('.', ','));
                decimal sterlin = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "GBP")).InnerText.Replace('.', ','));
                decimal jpn = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "JPY")).InnerText.Replace('.', ','));
                //decimal # = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "#")).InnerText.Replace('.', ','));
                lblDolar.Text = dolar.ToString();
                lblEuro.Text = euro.ToString();
                lblSterlin.Text = sterlin.ToString();
                lbljpn.Text = jpn.ToString();

            }
            catch (XmlException xml)
            {
                
                MessageBox.Show(xml.ToString());
            }






        }

        private const string API_KEY = "8d38fe5bfde0f53cb48bc740bf9ab659";
        // YOUR CITY CODE = #
        private const string CITY_ID = "#";
        private const string API_URL = "http://api.openweathermap.org/data/2.5/weather?id={0}&appid={1}&units=metric";


        private void button2_Click(object sender, EventArgs e)
        {

            UpdateWeather();



        }

        private void UpdateWeather()
        {
            string apiUrl = string.Format(API_URL, CITY_ID, API_KEY);

            using (WebClient client = new WebClient())
            {
                try
                {
                    string json = client.DownloadString(apiUrl);
                    dynamic data = JObject.Parse(json);

                    
                    label3.Text = string.Format("Sıcaklık: {0} °C", data.main.temp);
                    label5.Text = string.Format("Hissedilen Sıcaklık: {0} °C", data.main.feels_like);
                    label6.Text = string.Format("Hava Durumu: {0}", data.weather[0].description);
                    lblWindSpeed.Text = string.Format("Rüzgar Hızı: {0} m/s", data.wind.speed);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hava durumu verileri alınamadı. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

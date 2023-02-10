using Manager;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;

namespace CReaderUI
{
    public partial class _FormDonwloadDialog : Form
    {

        public class DataObject
        {
            public string Name { get; set; }
        }

        private const string BASE_URL = "http://192.168.1.64:8020/";
        private const string API_URL = "api/file/Getcontractfile/";

        public DialogReturn dialogReturn;
        public enum DialogReturn
        {
            NULL = 0,
            LOAD = 1
        }
        public _FormDonwloadDialog(Form app)
        {
            InitializeComponent();
            dialogReturn = DialogReturn.NULL;

        }
        private void dlContract_Click(object sender, EventArgs e)
        {
            
            string UID = txtuid.Text;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(API_URL + UID).Result;
            if( response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsAsync<dynamic>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll

                var c_detail = (JToken)dataObjects["ContractDetails"];
                var commodity = (JArray)dataObjects["CommodityDetails"];
                var rates = (JArray)dataObjects["RatesDetails"];
                var rates_header = (JArray)dataObjects["RatesContainerHeader"];
                var rates_surcharge_header = (JArray)dataObjects["RatesSurchargeHeader"];
                var city = (JArray)dataObjects["CityDetails"];
                var arbs = (JArray)dataObjects["ArbsDetail"];

                DLJsonModel dmodel = new DLJsonModel(c_detail, commodity, rates, rates_header, rates_surcharge_header, city, arbs);
                ContractManageWriter cmw = new ContractManageWriter(dmodel);
                cmw.createContract();

                MessageBox.Show("Converted Success");
            }
            else
            {
                MessageBox.Show("Failed accessing the server");
            }
            client.Dispose();
        }

    }
}

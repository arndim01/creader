
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TemplateWriter.Build;

namespace TemplateWriter.Data
{
    public class Assemble
    {
        public Assemble() { }
        public void CONSTRUCT_TEMPLATE(JToken c_detail, JArray commodity, JArray rates, JArray rates_header, JArray rates_surcharge_header, JArray city, JArray arbs)
        {

            string T_DIR_PATH = Path.GetTempPath() + "/CReaderTemplate";
            string S_PATH = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/Template/CatapultTemplate.xlsm";
            string T_PATH = Path.GetTempPath() + "/CReaderTemplate/CatapultTemplate.xlsm";
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Directory.CreateDirectory(Path.GetTempPath() + "/CReaderTemplate");
            Directory.CreateDirectory(documentPath + "/CReader");

            if ( File.Exists(T_PATH))
            {
                File.Delete(T_PATH);
            }

            File.Copy(S_PATH, T_PATH);

            var prop = new TemplateProperties( T_PATH );
            ITemplateBuilder commodityBuilder = new Commodity(prop);
            ITemplateBuilder ratesBuilder = new Rates(prop);
            ITemplateBuilder arbsBuilder = new Arbs(prop);
            ITemplateBuilder groupBuilder = new GroupCity(prop);

            DataSet set = new DataSet();

            commodityBuilder.WRITE_TEMPLATE_COLUMN();
            commodityBuilder.WRITE_TEMPLATE_DATA(commodity, c_detail);

            ratesBuilder.WRITE_TEMPLATE_COLUMN(rates_header, rates_surcharge_header);
            ratesBuilder.WRITE_TEMPLATE_DATA(c_detail, rates, city);

            arbsBuilder.WRITE_TEMPLATE_COLUMN();
            arbsBuilder.WRITE_TEMPLATE_DATA(c_detail, arbs, city);

            groupBuilder.WRITE_TEMPLATE_COLUMN();
            groupBuilder.WRITE_TEMPLATE_DATA(city);

            set.Tables.Add(commodityBuilder.details);
            set.Tables.Add(ratesBuilder.details);
            set.Tables.Add(arbsBuilder.details);
            set.Tables.Add(arbsBuilder.details2);
            set.Tables.Add(groupBuilder.details);

            TemplateCreateColumn(set, prop);
            TemplateCreateData(set, prop);


            string guid = Guid.NewGuid().ToString().ToUpper();

            while ( File.Exists(documentPath + "/CReader/" + guid + ".xlsm"))
            {
                guid = Guid.NewGuid().ToString().ToUpper();
            }

            Directory.CreateDirectory(documentPath + "/CReader/" + guid);
            File.Copy(T_PATH, documentPath + "/CReader/" + guid + "/CatapultTemplateOutput.xlsm");
            File.Delete(T_PATH);
            Directory.Delete(T_DIR_PATH);


        }
        
        private void TemplateCreateColumn(DataSet set, TemplateProperties prop)
        {
            OleDbConnection con;
            OleDbCommand com;
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties='Excel 12.0 Macro;HDR=Yes;'", prop.contractPath);
            foreach(DataTable table in set.Tables)
            {
                string tablename = TemplateWriter.ConstructTablename(table.TableName);
                string query = "Create Table [" + tablename + "$] " + TemplateWriter.ConstructCreateColumn(table);


                using (con = new OleDbConnection(connectionString))
                using (com = new OleDbCommand(query, con))
                {
                    con.Open();
                    com.ExecuteNonQuery();
                }
            }
                
        }
        private void TemplateCreateData(DataSet set, TemplateProperties prop)
        {
            OleDbConnection con;
            OleDbDataAdapter adapter;
            string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties='Excel 12.0 Macro;HDR=No;Readonly=false;'", prop.contractPath);

            foreach(DataTable table in set.Tables)
            {
                string tablename = TemplateWriter.ConstructTablename(table.TableName);
                string query = "INSERT INTO [" + tablename + "$] " + TemplateWriter.ConstructQueryData(table);
                using (con = new OleDbConnection(connectionString))
                {
                    con.Open();
                    adapter = new OleDbDataAdapter();
                    adapter.InsertCommand = new OleDbCommand(query, con);
                    int increment = 1;
                    foreach (DataColumn col in table.Columns)
                    {
                        adapter.InsertCommand.Parameters.Add("@" + col.ColumnName.Replace(" ", ""), OleDbType.LongVarChar, 10000, col.ColumnName);
                        increment++;
                    }
                    adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    adapter.Update(table);
                }
            }
        }

    }
}

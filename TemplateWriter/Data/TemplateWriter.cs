using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWriter.Data
{
    public static class TemplateWriter
    {
        public static void ConstructDataTableModel(out DataTable data, TemplateConstructModel.ColumnModel typeModel)
        {
            data = null;
            switch (typeModel)
            {
                case TemplateConstructModel.ColumnModel.COMMODITY:
                    data = new DataTable("commodity");
                    data = createColumnAdditional(new TemplateConstructModel.CommodityColumns { }, data);
                    break;
                case TemplateConstructModel.ColumnModel.RATES:
                    data = new DataTable("rates");
                    data = createColumnAdditional(new TemplateConstructModel.RatesColumns { }, data);
                    break;
            }
        }
        private static DataTable createColumnAdditional(Enum enumValue, DataTable mTable)
        {
            DataTable table = mTable;
            foreach (Enum value in Enum.GetValues(enumValue.GetType()))
            {
                table.Columns.Add(new DataColumn(value.GetStringValue()));
            }
            return table;
        }
        public static List<string> constructListValueOfEnum(Enum enumValue)
        {
            List<string> enumList = new List<string>();
            foreach(Enum value in Enum.GetValues(enumValue.GetType()))
            {
                enumList.Add(value.GetStringValue());

            }

            return enumList;
        }

        public static string ConstructTablename(string tableName)
        {
            switch (tableName)
            {   
                case "commodity": return "Commodity";
                case "rates": return "Rates";
                case "origin": return "Origin Arbitraries";
                case "destination": return "Destination Arbitraries";
                case "groupcode": return "Group Code Info";
            }
            return "";
        }
        public static string ConstructCreateColumn(DataTable table)
        {
            string dataColumn = "";
            foreach (DataColumn col in table.Columns)
            {
                if (col == table.Columns[table.Columns.Count - 1])
                    dataColumn += "[" + col.ColumnName + "] String";
                else
                    dataColumn += "[" + col.ColumnName + "] String" + ", ";
            }
            return "(" + dataColumn + ")";
        }
        public static string ConstructQueryData(DataTable table)
        {
            string query = "";
            switch (table.TableName)
            {
                case "commodity":
                    query = ConstructQueryDataModel(table);
                    break;
                case "rates":
                    query = ConstructQueryDataModel(table);
                    break;
                case "origin":
                    query = ConstructQueryDataModel(table);
                    break;
                case "destination":
                    query = ConstructQueryDataModel(table);
                    break;
                case "groupcode":
                    query = ConstructQueryDataModel(table);
                    break;
            }
            return query;
        }
        private static string ConstructQueryDataModel(DataTable table)
        {
            string queryColumn = "(" + dataColumn(table) + ")";
            string queryValue = "(" + dataReferenceColumn(table) + ")";
            return queryColumn + " VALUES " + queryValue;
        }
        private static string dataColumn(DataTable table)
        {
            string dataColumn = "";
            int increment = 1;
            foreach (DataColumn col in table.Columns)
            {
                if (col == table.Columns[table.Columns.Count - 1])
                    dataColumn += "F" + increment;
                else
                    dataColumn += "F" + increment + ", ";

                increment++;
            }
            return dataColumn;
        }
        private static string dataReferenceColumn(DataTable table)
        {
            string dataColumn = "";
            foreach (DataColumn col in table.Columns)
            {
                if (col == table.Columns[table.Columns.Count - 1])
                    dataColumn += "@" + col.ColumnName.Replace(" ", "");
                else
                    dataColumn += "@" + col.ColumnName.Replace(" ", "") + ", ";
            }
            return dataColumn;
        }

        internal static string hashData(this string value)
        {
            value = value.Replace("@", "").Replace(" ", "").ToUpperInvariant();
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(value));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}

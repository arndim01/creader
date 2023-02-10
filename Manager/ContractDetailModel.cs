using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public interface IContractDetail<T>
    {
        IItem<T> details { get; }
        void CompileDataTable(DataSet set);
    }

    //ITEMS INTERFACE
    public interface IItem<T> : IEnumerable
    {
        void Add(T item);
    }
    public class Items<T> : IEnumerable, IItem<T>
    {
        private List<T> _itemList = new List<T>();

        public void Add(T item)
        {
            _itemList.Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_itemList.ToList()).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    //CORE
    #region
    public class CommodityDetails<T> : IContractDetail<T> where T : class
    {
        public IItem<T> details { get; private set; }
        //CONVERT DATA TO LIST
        public void CompileDataTable(DataSet set)
        {
            details = new Items<T>();
            foreach (DataRow row in set.Tables["COMMODITY"].Rows)
            {
                var detail = details.Cast<List<dynamic>>().AsEnumerable().Where(c => c[1] == row["COMMODITY_CODE"].ToString().hashData()).FirstOrDefault();
                if (detail == null)
                {

                    var dict = new List<dynamic>()
                    {
                        new List<int>() { Convert.ToInt32(row["RATES_ID"]) },
                        row["COMMODITY_DESC"].ToString().hashData(),
                        row["COMMODITY_DESC"].ToString(),
                        row["COMMODITY_DESC"].ToString(),
                        "",
                        ""
                    };

                    details.Add(dict as T);
                }
                else
                {
                    (details.Cast<List<dynamic>>().AsEnumerable().Where(c => c[1] == row["COMMODITY_CODE"].ToString().hashData()).FirstOrDefault()[0] as List<int>).Add(Convert.ToInt32(row["RATES_ID"]));
                }
            }
        }
    }
    #endregion
    public static class CustomStringHelper
    {
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
        internal static string Pointer(this string value, bool port = true)
        {
            string pointer = "";
            if (port)
            {
                if (value == "R_ORG")
                {
                    pointer = "R_OV";
                }
                else if (value == "R_DES")
                {
                    pointer = "R_DV";
                }
                else if (value == "A_ORG")
                {
                    pointer = "A_OV";
                }
                else if (value == "A_DES")
                {
                    pointer = "A_DV";
                }
            }
            else
            {
                if (value == "R_ORG")
                {
                    pointer = "R_O";
                }
                else if (value == "R_DES")
                {
                    pointer = "R_D";
                }
                else if (value == "A_ORG")
                {
                    pointer = "A_O";
                }
                else if (value == "A_DES")
                {
                    pointer = "A_D";
                }
            }
            return pointer;
        }
        internal static string ZipString(this string value)
        {
            byte[] byteArray = new byte[value.Length];
            int indexBA = 0;
            foreach (char item in value.ToCharArray())
            {
                byteArray[indexBA++] = (byte)item;
            }
            MemoryStream ms = new MemoryStream();
            System.IO.Compression.GZipStream sw = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress);
            sw.Write(byteArray, 0, byteArray.Length);
            sw.Close();

            byteArray = ms.ToArray();
            StringBuilder sb = new StringBuilder(byteArray.Length);
            foreach (byte item in byteArray)
            {
                sb.Append((char)item);
            }
            ms.Close();
            sw.Dispose();
            ms.Dispose();
            return sb.ToString();
        }
        internal static string UnZipString(string value)
        {
            //Transform string into byte[]
            byte[] byteArray = new byte[value.Length];
            int indexBA = 0;
            foreach (char item in value.ToCharArray())
            {
                byteArray[indexBA++] = (byte)item;
            }

            //Prepare for decompress
            System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArray);
            System.IO.Compression.GZipStream sr = new System.IO.Compression.GZipStream(ms,
                System.IO.Compression.CompressionMode.Decompress);

            //Reset variable to collect uncompressed result
            byteArray = new byte[byteArray.Length];

            //Decompress
            int rByte = sr.Read(byteArray, 0, byteArray.Length);

            //Transform byte[] unzip data to string
            System.Text.StringBuilder sB = new System.Text.StringBuilder(rByte);
            //Read the number of bytes GZipStream red and do not a for each bytes in
            //resultByteArray;
            for (int i = 0; i < rByte; i++)
            {
                sB.Append((char)byteArray[i]);
            }
            sr.Close();
            ms.Close();
            sr.Dispose();
            ms.Dispose();
            return sB.ToString();
        }
    }
}


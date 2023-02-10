
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateWriter.Build;

namespace TemplateWriter.Data
{
    public class Rates : ITemplateBuilder
    {
        private TemplateProperties prop;

        public DataTable details { get; set; }
        private List<string> additionalColumn { get; set; }

        public DataTable details2 { get; set; }
        public Rates(TemplateProperties properties)
        {
            prop = properties;
            details = new DataTable("rates");
            additionalColumn = new List<string>();
        }

        public void WRITE_TEMPLATE_COLUMN(params object[] obj)
        {
            DataTable customTable;
            //TemplateWriter.ConstructDataTableModel(out customTable, TemplateConstructModel.ColumnModel.RATES);
            //details = customTable;
            List<string> main_column = TemplateWriter.constructListValueOfEnum(new TemplateConstructModel.RatesColumns { });
            List<string> rates_header_column = new List<string>();

            JArray rates_header = (JArray)obj[0];
            JArray rates_surcharge_header = (JArray)obj[1];


            foreach (JToken r_header in rates_header)
            {
                string containertype = r_header[0].Value<string>();
                string container20 = r_header[1].Value<string>();
                string container40 = r_header[2].Value<string>();
                string container40h = r_header[3].Value<string>();
                string container45 = r_header[4].Value<string>();

                if (containertype == "DRY" || containertype == "DG")
                {
                    if (container20 == "" && container40 == "" && container40h == "" && container45 == "")
                    {
                        if (!rates_header_column.Contains("BR_20_DC_PC_USD"))
                            rates_header_column.Add("BR_20_DC_PC_USD");
                        if (!rates_header_column.Contains("BR_40_DC_PC_USD"))
                            rates_header_column.Add("BR_40_DC_PC_USD");
                        if (!rates_header_column.Contains("BR_40HC_DC_PC_USD"))
                            rates_header_column.Add("BR_40HC_DC_PC_USD");
                        if (!rates_header_column.Contains("BR_45_DC_PC_USD"))
                            rates_header_column.Add("BR_45_DC_PC_USD");
                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(r_header[1].Value<string>()))
                        {
                            var container = r_header[1].Value<string>().Split('_');
                            string head = "BR_20_" + r_header[0] + "_" + r_header[1] + "_PC_USD";
                            if (!rates_header_column.Contains(head))
                            {
                                rates_header_column.Add(head);
                            }
                        }
                        if (!String.IsNullOrWhiteSpace(r_header[2].Value<string>()))
                        {
                            var container = r_header[2].Value<string>().Split('_');
                            string head = "BR_40_" + r_header[0] + "_" + r_header[2] + "_PC_USD";
                            if (!rates_header_column.Contains(head))
                            {
                                rates_header_column.Add(head);
                            }
                        }
                        if (!String.IsNullOrWhiteSpace(r_header[3].Value<string>()))
                        {
                            var container = r_header[3].Value<string>().Split('_');
                            string head = "BR_40HC_" + r_header[0] + "_" + r_header[3] + "_PC_USD";
                            if (!rates_header_column.Contains(head))
                            {
                                rates_header_column.Add(head);
                            }
                        }
                        if (!String.IsNullOrWhiteSpace(r_header[4].Value<string>()))
                        {
                            var container = r_header[4].Value<string>().Split('_');
                            string head = "BR_45_" + r_header[0] + "_" + r_header[4] + "_PC_USD";
                            if (!rates_header_column.Contains(head))
                            {
                                rates_header_column.Add(head);
                            }
                        }
                    }
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(r_header[1].Value<string>()))
                    {
                        var container = r_header[1].Value<string>().Split('_');
                        string head = "BR_20_" + r_header[0] + "_" + r_header[1] + "_PC_USD";
                        if (!rates_header_column.Contains(head))
                        {
                            rates_header_column.Add(head);
                        }
                    }
                    if (!String.IsNullOrWhiteSpace(r_header[2].Value<string>()))
                    {
                        var container = r_header[2].Value<string>().Split('_');
                        string head = "BR_40_" + r_header[0] + "_" + r_header[2] + "_PC_USD";
                        if (!rates_header_column.Contains(head))
                        {
                            rates_header_column.Add(head);
                        }
                    }
                    if (!String.IsNullOrWhiteSpace(r_header[3].Value<string>()))
                    {
                        var container = r_header[3].Value<string>().Split('_');
                        string head = "BR_40HC_" + r_header[0] + "_" + r_header[3] + "_PC_USD";
                        if (!rates_header_column.Contains(head))
                        {
                            rates_header_column.Add(head);
                        }
                    }
                    if (!String.IsNullOrWhiteSpace(r_header[4].Value<string>()))
                    {
                        var container = r_header[4].Value<string>().Split('_');
                        string head = "BR_45_" + r_header[0] + "_" + r_header[4] + "_PC_USD";
                        if (!rates_header_column.Contains(head))
                        {
                            rates_header_column.Add(head);
                        }
                    }
                }

            }
            foreach (JToken tk in rates_surcharge_header)
            {

                bool type = tk[3].Value<bool>();
                if (type)
                {
                    string head = tk[2].Value<string>() + "_OXX_OXX_PC_USD";
                    if (!rates_header_column.Contains(head))
                    {
                        rates_header_column.Add(head);
                    }
                }
                else
                {
                    string head = "N" + tk[2].Value<string>() + "_OXX_OXX_PC_USD";
                    if (!rates_header_column.Contains(head))
                    {
                        rates_header_column.Add(head);
                    }
                }
            }
            additionalColumn.AddRange(rates_header_column);
            main_column.AddRange(rates_header_column);
            foreach (string hee in main_column)
            {
                details.Columns.Add(hee);

            }

        }

        public void WRITE_TEMPLATE_DATA(params object[] obj)
        {
            JToken contractDetail = (JToken)obj[0];
            JArray rates = (JArray)obj[1];
            JArray city = (JArray)obj[2];

            foreach (JToken tk in rates)
            {
                string c_eff = contractDetail[1].Value<string>();
                string c_exp = contractDetail[2].Value<string>();

                string carrier = contractDetail[0].Value<string>();
                string contract_id = contractDetail[3].Value<string>();
                string amd_id = contractDetail[4].Value<string>();
                string eff = tk[7][0][0].Value<string>();
                string exp = tk[7][0][1].Value<string>();
                string commodity_code = tk[2].Value<string>();
                string scope = tk[3].Value<string>();
                string arbs_const = tk[7][3].Value<string>();
                string general_notes = tk[7][1].Value<string>();
                foreach (JToken min in tk[5].Value<JArray>())
                {
                    string header = min[1].Value<string>();
                    string origin = header.Split('|')[0];
                    string origin_city = "";
                    string origin_state = "";
                    string origin_country = "";
                    string origin_unloc = "";

                    string origin_via = header.Split('|')[1];
                    string origin_via_city = "";
                    string origin_via_state = "";
                    string origin_via_country = "";
                    string origin_via_unloc = "";

                    if (!String.IsNullOrWhiteSpace(origin))
                    {
                        var arr = city.FirstOrDefault(x => x[1].Value<string>() == origin);
                        if (arr != null)
                        {

                            if (arr[2].Count() == 1)
                            {
                                origin = "";
                                origin_city = arr[2][0][1].Value<string>();
                                origin_state = arr[2][0][2].Value<string>();
                                origin_country = arr[2][0][3].Value<string>();
                                origin_unloc = arr[2][0][4].Value<string>();


                            }
                            
                        }
                    }
                    if ( !String.IsNullOrWhiteSpace(origin_via))
                    {
                        var arr = city.FirstOrDefault(x => x[1].Value<string>() == origin_via);
                        if (arr != null)
                        {

                            if (arr[2].Count() == 1)
                            {
                                origin_via = "";
                                origin_via_city = arr[2][0][1].Value<string>();
                                origin_via_state = arr[2][0][2].Value<string>();
                                origin_via_country = arr[2][0][3].Value<string>();
                                origin_via_unloc = arr[2][0][4].Value<string>();


                               
                            }
                        }
                    }
                    foreach (JToken min2 in min[4].Value<JArray>())
                    {
                        details.Rows.Add();

                        string destination = min2[0].Value<string>();
                        string destination_city = "";
                        string destination_state = "";
                        string destination_country = "";
                        string destination_unloc = "";

                        string destination_via = min2[1].Value<string>();
                        string destination_via_city = "";
                        string destination_via_state = "";
                        string destination_via_country = "";
                        string destination_via_unloc = "";

                        if( !String.IsNullOrWhiteSpace(destination))
                        {
                            var arr = city.FirstOrDefault(x => x[1].Value<string>() == destination);
                            if( arr != null)
                            {
                                if( arr[2].Count() == 1)
                                {
                                    destination = "";
                                    destination_city = arr[2][0][1].Value<string>();
                                    destination_state = arr[2][0][2].Value<string>();
                                    destination_country = arr[2][0][3].Value<string>();
                                    destination_unloc = arr[2][0][4].Value<string>();

                                }
                            }
                        }
                        if( !String.IsNullOrWhiteSpace(destination_via))
                        {
                            var arr = city.FirstOrDefault(x => x[1].Value<string>() == destination);
                            if (arr != null)
                            {
                                if (arr[2].Count() == 1)
                                {
                                    destination_via = "";
                                    destination_via_city = arr[2][0][1].Value<string>();
                                    destination_via_state = arr[2][0][2].Value<string>();
                                    destination_via_country = arr[2][0][3].Value<string>();
                                    destination_via_unloc = arr[2][0][4].Value<string>();

                                }
                            }
                        }

                        if( !String.IsNullOrWhiteSpace(origin_via) || !String.IsNullOrWhiteSpace(origin_via_city))
                        {

                        }
                        

                        string mode = min2[2].Value<string>();
                        string c_notes = min2[3].Value<string>();
                        string n_notes = min2[4].Value<string>();
                        string haz_type = min2[5].Value<string>();
                        string dum_serv = min2[6].Value<string>() + "/" + min2[7].Value<string>();
                        string service = dum_serv;

                        string br_20 = min2[8].Value<string>();
                        string br_40 = min2[9].Value<string>();
                        string br_40h = min2[10].Value<string>();
                        string br_45 = min2[11].Value<string>();

                        string cont_type = min2[12].Value<string>();
                        string currency = min2[12].Value<string>();

                        string contract_notes = general_notes;

                        if( !String.IsNullOrWhiteSpace(n_notes))
                        {
                            var arr = min[3].FirstOrDefault(x => x[0].Value<int>() == Convert.ToInt32(n_notes));
                            if( arr != null)
                            {
                                contract_notes += arr[1][1].Value<string>();
                                if( c_eff != arr[1][0][0].Value<string>())
                                {
                                    eff = arr[1][0][0].Value<string>();
                                }
                                if( c_exp != arr[1][0][1].Value<string>())
                                {
                                    exp = arr[1][0][1].Value<string>();
                                }

                                if( arr[1][3].Value<string>() != "O-D")
                                {
                                    arbs_const = arr[1][3].Value<string>();
                                }
                                if( arr[1][2] != null)
                                {
                                    for(int ix = 0; ix < arr[1][2].Count(); ix++)
                                    {
                                        if (arr[1][2][ix][3].Value<bool>())
                                        {
                                            details.Rows[details.Rows.Count - 1][arr[1][2][ix][2].Value<string>() + "_OXX_OXX_PC_USD"] = "-1";

                                        }else
                                        {
                                            details.Rows[details.Rows.Count - 1]["N" + arr[1][2][ix][2].Value<string>() + "_OXX_OXX_PC_USD"] = "-4";
                                        }
                                    }
                                }
                            }
                        }

                        if (tk[7][2] != null)
                        {
                            for (int ix = 0; ix < tk[7][2].Count(); ix++)
                            {
                                
                                if(tk[7][2][ix][3].Value<bool>() == true)
                                {
                                    details.Rows[details.Rows.Count - 1][tk[7][2][ix][2].Value<string>() + "_OXX_OXX_PC_USD"] = "-1";

                                }else
                                        {
                                    details.Rows[details.Rows.Count - 1]["N" + tk[7][2][ix][2].Value<string>() + "_OXX_OXX_PC_USD"] = "-4";
                                }
                            }
                        }

                        var b20 = (!String.IsNullOrWhiteSpace(min2[8].Value<string>()) ? min2[8].Value<string>().Split('/') : "".Split('/'));
                        var b40 = (!String.IsNullOrWhiteSpace(min2[9].Value<string>()) ? min2[9].Value<string>().Split('/') : "".Split('/'));
                        var b40h = (!String.IsNullOrWhiteSpace(min2[10].Value<string>()) ? min2[10].Value<string>().Split('/') : "".Split('/'));
                        var b45 = (!String.IsNullOrWhiteSpace( min2[11].Value<string>())  ?  min2[11].Value<string>().Split('/') : "".Split('/'));

                        if (b20.Count() == 2)
                        {
                            details.Rows[details.Rows.Count - 1]["BR_20_" + cont_type + "_" + b20[0] + "_20_PC_USD"] = b20[1];
                        }
                        else
                        {
                            details.Rows[details.Rows.Count - 1]["BR_20_DC_PC_USD"] = min2[8].Value<string>();
                        }

                        if (b40.Count() == 2)
                        {
                            details.Rows[details.Rows.Count - 1]["BR_40_" + cont_type + "_" + b40[0] + "_40_PC_USD"] = b40[1];
                        }
                        else
                        {
                            details.Rows[details.Rows.Count - 1]["BR_40_DC_PC_USD"] = min2[9].Value<string>();
                        }

                        if (b40h.Count() == 2)
                        {
                            details.Rows[details.Rows.Count - 1]["BR_40HC_" + cont_type + "_" + b40h[0] + "_40H_PC_USD"] = b40h[1];
                        }
                        else
                        {
                            details.Rows[details.Rows.Count - 1]["BR_40HC_DC_PC_USD"] = min2[10].Value<string>();
                        }

                        if (b45.Count() == 2)
                        {
                            details.Rows[details.Rows.Count - 1]["BR_45_" + cont_type + "_" + b45[0] + "_45_PC_USD"] = b45[1];
                        }
                        else
                        {
                            details.Rows[details.Rows.Count - 1]["BR_45_DC_PC_USD"] = min2[11].Value<string>();
                        }


                        details.Rows[details.Rows.Count - 1]["contract_carrier"] = carrier;
                        details.Rows[details.Rows.Count - 1]["contract_rate_id"] = "";
                        details.Rows[details.Rows.Count - 1]["contract_id"] = contract_id;
                        details.Rows[details.Rows.Count - 1]["contract_amendment_id"] = amd_id;
                        details.Rows[details.Rows.Count - 1]["amendment_change"] = "";
                        details.Rows[details.Rows.Count - 1]["contract_effective_date"] = Convert.ToDateTime(eff).convertDateFormat();
                        details.Rows[details.Rows.Count - 1]["contract_expiration_date"] = Convert.ToDateTime(exp).convertDateFormat();
                        details.Rows[details.Rows.Count - 1]["commodity_code"] = commodity_code;


                        details.Rows[details.Rows.Count - 1]["origin_group"] = origin;
                        details.Rows[details.Rows.Count - 1]["origin_city"] = origin_city;
                        details.Rows[details.Rows.Count - 1]["origin_state"] = origin_state;
                        details.Rows[details.Rows.Count - 1]["origin_country"] = origin_country;
                        details.Rows[details.Rows.Count - 1]["origin_UNLOC_code"] = origin_unloc;



                        details.Rows[details.Rows.Count - 1]["origin_via_group_name"] = origin_via;
                        details.Rows[details.Rows.Count - 1]["origin_via_city"] = origin_via_city;
                        details.Rows[details.Rows.Count - 1]["origin_via_state"] = origin_via_state;
                        details.Rows[details.Rows.Count - 1]["origin_via_country"] = origin_via_country;
                        details.Rows[details.Rows.Count - 1]["origin_via_UNLOC_code"] = origin_via_unloc;


                        details.Rows[details.Rows.Count - 1]["origin_trade"] = "";
                        
                        details.Rows[details.Rows.Count - 1]["destination_group"] = destination;
                        details.Rows[details.Rows.Count - 1]["destination_city"] = destination_city;
                        details.Rows[details.Rows.Count - 1]["destination_state"] = destination_state;
                        details.Rows[details.Rows.Count - 1]["destination_country"] = destination_country;
                        details.Rows[details.Rows.Count - 1]["destination_UNLOC_code"] = destination_unloc;

                        details.Rows[details.Rows.Count - 1]["destination_via_group_name"] = destination_via;
                        details.Rows[details.Rows.Count - 1]["destination_via_city"] = destination_via_city;
                        details.Rows[details.Rows.Count - 1]["destination_via_state"] = destination_via_state;
                        details.Rows[details.Rows.Count - 1]["destination_via_country"] = destination_via_country;
                        details.Rows[details.Rows.Count - 1]["destination_via_UNLOC_code"] = destination_via_unloc;

                        details.Rows[details.Rows.Count - 1]["destination_trade"] = "";

                        details.Rows[details.Rows.Count - 1]["Arbitrary Construct"] = arbs_const;
                        details.Rows[details.Rows.Count - 1]["service"] = service;
                        details.Rows[details.Rows.Count - 1]["mode"] = mode;
                        details.Rows[details.Rows.Count - 1]["analyst notes"] = "";
                        details.Rows[details.Rows.Count - 1]["contract notes"] = contract_notes;
                        details.Rows[details.Rows.Count - 1]["data entry notes"] = n_notes;
                        details.Rows[details.Rows.Count - 1]["rate type"] = "FCL";
                        details.Rows[details.Rows.Count - 1]["rate transit"] = "";
                        details.Rows[details.Rows.Count - 1]["Hazardous Charges"] = haz_type;
                        details.Rows[details.Rows.Count - 1]["Origin Free Time"] = "";
                        details.Rows[details.Rows.Count - 1]["Destination Free Time"] = "";
                        details.Rows[details.Rows.Count - 1]["Customer ID"] = "";
                        details.Rows[details.Rows.Count - 1]["SCOPE"] = scope;
                        details.Rows[details.Rows.Count - 1]["LASTAMD"] = amd_id;

                    }

                }
            }
        }
    }
}

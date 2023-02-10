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
    public class Arbs : ITemplateBuilder
    {
        private TemplateProperties prop;
        public DataTable details { get; set; }

        public DataTable details2 { get; set; }

        public Arbs(TemplateProperties properties)
        {
            prop = properties;
            details = new DataTable("origin");
            details2 = new DataTable("destination");
        }
        public void WRITE_TEMPLATE_COLUMN(params object[] obj)
        {
            List<string> main_column = TemplateWriter.constructListValueOfEnum(new TemplateConstructModel.OriginArbsColumn { });
            List<string> main_column2 = TemplateWriter.constructListValueOfEnum(new TemplateConstructModel.DestinationArbsColumn { });

            foreach(string m1 in main_column)
            {
                details.Columns.Add(m1);
            }
            foreach(string m2 in main_column2)
            {
                details2.Columns.Add(m2);
            }

        }
        public void WRITE_TEMPLATE_DATA(params object[] obj)
        {
            JToken contractDetail = (JToken)obj[0];
            JArray arbs = (JArray)obj[1];
            JArray city = (JArray)obj[2];

            foreach(JToken tk in arbs)
            {
                string point = tk[4].Value<string>();

                string c_eff = contractDetail[1].Value<string>();
                string c_exp = contractDetail[2].Value<string>();
                string carrier = contractDetail[0].Value<string>();
                string contract_id = contractDetail[3].Value<string>();
                string amd_id = contractDetail[4].Value<string>();
                string scope = tk[3].Value<string>();
                string notes = tk[6].Value<string>();


                string inland_via = tk[2].Value<string>();
                string inland_via_city = "";
                string inland_via_state = "";
                string inland_via_country = "";
                string inland_via_unloc = "";

                if( !String.IsNullOrWhiteSpace(inland_via))
                {
                    var arr = city.FirstOrDefault(x => x[1].Value<string>() == inland_via);
                    if (arr != null)
                    {

                        if (arr[2].Count() == 1)
                        {
                            inland_via = "";
                            inland_via_city = arr[2][0][1].Value<string>();
                            inland_via_state = arr[2][0][2].Value<string>();
                            inland_via_country = arr[2][0][3].Value<string>();
                            inland_via_unloc = arr[2][0][4].Value<string>();

                        }

                    }
                }


                if ( point == "ORIGIN")
                {
                    foreach( JToken min in tk[5])
                    {

                        details.Rows.Add();

                        string inland = min[0].Value<string>();
                        string inland_city = "";
                        string inland_state = "";
                        string inland_country = "";
                        string inland_unloc = "";

                        if (!String.IsNullOrWhiteSpace(inland))
                        {
                            var arr = city.FirstOrDefault(x => x[1].Value<string>() == inland);
                            if (arr != null)
                            {

                                if (arr[2].Count() == 1)
                                {
                                    inland = "";
                                    inland_city = arr[2][0][1].Value<string>();
                                    inland_state = arr[2][0][2].Value<string>();
                                    inland_country = arr[2][0][3].Value<string>();
                                    inland_unloc = arr[2][0][4].Value<string>();
                                }
                            }
                        }

                        string mode = min[2].Value<string>();
                        string c_notes = min[3].Value<string>();
                        string n_notes = min[4].Value<string>();
                        string cont_type = min[12].Value<string>();
                        string haz_type = min[5].Value<string>();
                        string service = (String.IsNullOrWhiteSpace( min[6].Value<string>()) ? min[7].Value<string>(): min[6].Value<string>()) ;
                        string br20 = min[8].Value<string>();
                        string br40 = min[9].Value<string>();
                        string br40h = min[10].Value<string>();
                        string br45 = min[11].Value<string>();
                        string currency = min[13].Value<string>();


                        details.Rows[details.Rows.Count - 1]["contract_carrier"] = carrier;

                        details.Rows[details.Rows.Count - 1]["contract_rate_id"] = "";
                        details.Rows[details.Rows.Count - 1]["contract_id"] = contract_id;
                        details.Rows[details.Rows.Count - 1]["contract_amendment_id"] = amd_id;
                        details.Rows[details.Rows.Count - 1]["amendment_change"] = "";
                        details.Rows[details.Rows.Count - 1]["contract_effective_date"] = c_eff;
                        details.Rows[details.Rows.Count - 1]["contract_expiration_date"] = c_exp;
                        details.Rows[details.Rows.Count - 1]["commodity_code"] = "";
                        details.Rows[details.Rows.Count - 1]["origin_group"] = inland;
                        details.Rows[details.Rows.Count - 1]["origin_city"] = inland_city;
                        details.Rows[details.Rows.Count - 1]["origin_state"] = inland_state;
                        details.Rows[details.Rows.Count - 1]["origin_country"] = inland_country;
                        details.Rows[details.Rows.Count - 1]["origin_UNLOC_code"] = inland_unloc;
                        details.Rows[details.Rows.Count - 1]["origin_via_group_name"] = inland_via;
                        details.Rows[details.Rows.Count - 1]["origin_via_city"] = inland_via_city;
                        details.Rows[details.Rows.Count - 1]["origin_via_state"] = inland_via_state;
                        details.Rows[details.Rows.Count - 1]["origin_via_country"] = inland_via_country;
                        details.Rows[details.Rows.Count - 1]["origin_via_UNLOC_code"] = inland_via_unloc;
                        details.Rows[details.Rows.Count - 1]["origin_trade"] = "";
                        details.Rows[details.Rows.Count - 1]["destination_trade"] = "";
                        details.Rows[details.Rows.Count - 1]["service"] = service;
                        details.Rows[details.Rows.Count - 1]["mode"] = mode;
                        details.Rows[details.Rows.Count - 1]["analyst notes"] = notes;
                        details.Rows[details.Rows.Count - 1]["contract notes"] = "";
                        details.Rows[details.Rows.Count - 1]["data entry notes"] = n_notes;
                        details.Rows[details.Rows.Count - 1]["rate type"] = "FCL";
                        details.Rows[details.Rows.Count - 1]["rate_transit"] = "";
                        details.Rows[details.Rows.Count - 1]["Hazardous Charges"] = haz_type;

                        details.Rows[details.Rows.Count - 1]["Origin Free Time Code"] = "";
                        details.Rows[details.Rows.Count - 1]["CUR"] = currency;
                        details.Rows[details.Rows.Count - 1]["SCOPE"] = scope;
                        details.Rows[details.Rows.Count - 1]["LASTAMD"] = amd_id;


                        if( !String.IsNullOrWhiteSpace(br20))
                        {
                            details.Rows[details.Rows.Count - 1]["ARBO_20_DC_PC_USD"] = br20;
                        }else
                        {
                            details.Rows[details.Rows.Count - 1]["ARBO_20_DC_PC_USD"] = "";
                        }


                        if (!String.IsNullOrWhiteSpace(br40))
                        {
                            details.Rows[details.Rows.Count - 1]["ARBO_40_DC_PC_USD"] = br40;
                        }
                        else
                        {
                            details.Rows[details.Rows.Count - 1]["ARBO_40_DC_PC_USD"] = "";
                        }


                        if (!String.IsNullOrWhiteSpace(br40h))
                        {
                            details.Rows[details.Rows.Count - 1]["ARBO_40HC_DC_PC_USD"] = br40h;
                        }
                        else
                        {
                            details.Rows[details.Rows.Count - 1]["ARBO_40HC_DC_PC_USD"] = "";
                        }


                        if (!String.IsNullOrWhiteSpace(br45))
                        {
                            details.Rows[details.Rows.Count - 1]["ARBO_45_DC_PC_USD"] = br45;
                        }
                        else
                        {
                            details.Rows[details.Rows.Count - 1]["ARBO_45_DC_PC_USD"] = "";
                        }

                    }
                }
                else if( point == "DESTINATION")
                {
                    foreach (JToken min in tk[5])
                    {

                        details2.Rows.Add();

                        string inland = min[0].Value<string>();
                        string inland_city = "";
                        string inland_state = "";
                        string inland_country = "";
                        string inland_unloc = "";

                        if (!String.IsNullOrWhiteSpace(inland))
                        {
                            var arr = city.FirstOrDefault(x => x[1].Value<string>() == inland);
                            if (arr != null)
                            {

                                if (arr[2].Count() == 1)
                                {
                                    inland = "";
                                    inland_city = arr[2][0][1].Value<string>();
                                    inland_state = arr[2][0][2].Value<string>();
                                    inland_country = arr[2][0][3].Value<string>();
                                    inland_unloc = arr[2][0][4].Value<string>();
                                }
                            }
                        }

                        string mode = min[2].Value<string>();
                        string c_notes = min[3].Value<string>();
                        string n_notes = min[4].Value<string>();
                        string cont_type = min[12].Value<string>();
                        string haz_type = min[5].Value<string>();
                        string service = (String.IsNullOrWhiteSpace(min[6].Value<string>()) ? min[7].Value<string>() : min[6].Value<string>());
                        string br20 = min[8].Value<string>();
                        string br40 = min[9].Value<string>();
                        string br40h = min[10].Value<string>();
                        string br45 = min[11].Value<string>();
                        string currency = min[13].Value<string>();


                        details2.Rows[details2.Rows.Count - 1]["contract_carrier"] = carrier;

                        details2.Rows[details2.Rows.Count - 1]["contract_rate_id"] = "";
                        details2.Rows[details2.Rows.Count - 1]["contract_id"] = contract_id;
                        details2.Rows[details2.Rows.Count - 1]["contract_amendment_id"] = amd_id;
                        details2.Rows[details2.Rows.Count - 1]["amendment_change"] = "";
                        details2.Rows[details2.Rows.Count - 1]["contract_effective_date"] = c_eff;
                        details2.Rows[details2.Rows.Count - 1]["contract_expiration_date"] = c_exp;
                        details2.Rows[details2.Rows.Count - 1]["commodity_code"] = "";
                        details2.Rows[details2.Rows.Count - 1]["destination_group"] = inland;
                        details2.Rows[details2.Rows.Count - 1]["destination_city"] = inland_city;
                        details2.Rows[details2.Rows.Count - 1]["destination_state"] = inland_state;
                        details2.Rows[details2.Rows.Count - 1]["destination_country"] = inland_country;
                        details2.Rows[details2.Rows.Count - 1]["destination_UNLOC_code"] = inland_unloc;
                        details2.Rows[details2.Rows.Count - 1]["destination_via_group_name"] = inland_via;
                        details2.Rows[details2.Rows.Count - 1]["destination_via_city"] = inland_via_city;
                        details2.Rows[details2.Rows.Count - 1]["destination_via_state"] = inland_via_state;
                        details2.Rows[details2.Rows.Count - 1]["destination_via_country"] = inland_via_country;
                        details2.Rows[details2.Rows.Count - 1]["destination_via_UNLOC_code"] = inland_via_unloc;
                        details2.Rows[details2.Rows.Count - 1]["origin_trade"] = "";
                        details2.Rows[details2.Rows.Count - 1]["destination_trade"] = "";
                        details2.Rows[details2.Rows.Count - 1]["service"] = service;
                        details2.Rows[details2.Rows.Count - 1]["mode"] = mode;
                        details2.Rows[details2.Rows.Count - 1]["analyst notes"] = notes;
                        details2.Rows[details2.Rows.Count - 1]["contract notes"] = "";
                        details2.Rows[details2.Rows.Count - 1]["data entry notes"] = n_notes;
                        details2.Rows[details2.Rows.Count - 1]["rate type"] = "FCL";
                        details2.Rows[details2.Rows.Count - 1]["rate_transit"] = "";
                        details2.Rows[details2.Rows.Count - 1]["Hazardous Charges"] = haz_type;

                        details2.Rows[details2.Rows.Count - 1]["Destination Free Time Code"] = "";
                        details2.Rows[details2.Rows.Count - 1]["CUR"] = currency;
                        details2.Rows[details2.Rows.Count - 1]["SCOPE"] = scope;
                        details2.Rows[details2.Rows.Count - 1]["LASTAMD"] = amd_id;

                        if (!String.IsNullOrWhiteSpace(br20))
                        {
                            details2.Rows[details2.Rows.Count - 1]["ARBD_20_DC_PC_USD"] = br20;
                        }
                        else
                        {
                            details2.Rows[details2.Rows.Count - 1]["ARBD_20_DC_PC_USD"] = "";
                        }


                        if (!String.IsNullOrWhiteSpace(br40))
                        {
                            details2.Rows[details2.Rows.Count - 1]["ARBD_40_DC_PC_USD"] = br40;
                        }
                        else
                        {
                            details2.Rows[details2.Rows.Count - 1]["ARBD_40_DC_PC_USD"] = "";
                        }


                        if (!String.IsNullOrWhiteSpace(br40h))
                        {
                            details2.Rows[details2.Rows.Count - 1]["ARBD_40HC_DC_PC_USD"] = br40h;
                        }
                        else
                        {
                            details2.Rows[details2.Rows.Count - 1]["ARBD_40HC_DC_PC_USD"] = "";
                        }


                        if (!String.IsNullOrWhiteSpace(br45))
                        {
                            details2.Rows[details2.Rows.Count - 1]["ARBD_45_DC_PC_USD"] = br45;
                        }
                        else
                        {
                            details2.Rows[details2.Rows.Count - 1]["ARBD_45_DC_PC_USD"] = "";
                        }

                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWriter.Data
{
    public static class TemplateConstructModel
    {
        public enum ColumnModel : int
        {
            NULL = 0,
            COMMODITY = 1,
            RATES = 2,
            ORIGIN = 3,
            DESTINATION = 4,
            GROUPCODE = 5
        }
        public enum CommodityColumns : int
        {
            [StringValue("commodity_carrier")]
            CommodityCarrier = 1,
            [StringValue("commodity_contract_id")]
            CommodityContractId = 2,
            [StringValue("commodity_amendment_id")]
            CommodityAmendment = 3,
            [StringValue("commodity_amendment_change")]
            CommodityAmendmentChange = 4,
            [StringValue("commodity_effective_date")]
            CommodityEffectiveDate = 5,
            [StringValue("commodity_expiration_date")]
            CommodityExpirationDate = 6,
            [StringValue("commodity_code")]
            CommodityCode = 7,
            [StringValue("commodity_description")]
            CommodityDescription = 8,
            [StringValue("commodity_brief_description")]
            CommodityBriefDescription = 9,
            [StringValue("commodity_named_account")]
            CommodityNamedAccount = 10,
            [StringValue("commodity_exclusions")]
            CommodityExclusions = 11,
            [StringValue("commodity_keywords")]
            CommodityKeywords = 12
        }
        public enum RatesColumns : int
        {
            [StringValue("contract_carrier")]
            ContractCarrier = 1,
            [StringValue("contract_rate_id")]
            ContractRateId = 2,
            [StringValue("contract_id")]
            ContractID = 3,
            [StringValue("contract_amendment_id")]
            ContractAmdId = 4,
            [StringValue("amendment_change")]
            AmendmentChange = 5,
            [StringValue("contract_effective_date")]
            ContractEffectiveDate = 6,
            [StringValue("contract_expiration_date")]
            ContractExpirationDate = 7,
            [StringValue("commodity_code")]
            CommodityCode = 8,
            [StringValue("origin_group")]
            OriginGroup = 9,
            [StringValue("origin_city")]
            OriginCity = 10,
            [StringValue("origin_state")]
            OriginState = 11,
            [StringValue("origin_country")]
            OriginCountry = 12,
            [StringValue("origin_UNLOC_code")]
            OriginUnloc = 13,
            [StringValue("origin_via_group_name")]
            OriginViaGroup = 14,
            [StringValue("origin_via_city")]
            OriginViaCity = 15,
            [StringValue("origin_via_state")]
            OriginViaState = 16,
            [StringValue("origin_via_country")]
            OriginViaCountry = 17,
            [StringValue("origin_via_UNLOC_code")]
            OriginViaUnloc = 18,
            [StringValue("origin_trade")]
            OriginTrade = 19,
            [StringValue("destination_group")]
            DestinationGroup = 20,
            [StringValue("destination_city")]
            DestinationCity = 21,
            [StringValue("destination_state")]
            DestinationState = 22,
            [StringValue("destination_country")]
            DestinationCountry = 23,
            [StringValue("destination_UNLOC_code")]
            DestinationUnloc = 24,
            [StringValue("destination_via_group_name")]
            DestinationViaGroup = 25,
            [StringValue("destination_via_city")]
            DestinationViaCity = 26,
            [StringValue("destination_via_state")]
            DestinationViaState = 27,
            [StringValue("destination_via_country")]
            DestinationViaCountry = 28,
            [StringValue("destination_via_UNLOC_code")]
            DestinationViaUnloc = 29,
            [StringValue("destination_trade")]
            DestinationTrade = 30,
            [StringValue("Arbitrary Construct")]
            ArbitraryConstruct = 31,
            [StringValue("service")]
            Service = 32,
            [StringValue("mode")]
            Mode = 33,
            [StringValue("analyst notes")]
            AnalystNotes = 34,
            [StringValue("contract notes")]
            ContractNotes = 35,
            [StringValue("data entry notes")]
            DataEntryNotes = 36,
            [StringValue("rate type")]
            RateType = 37,
            [StringValue("rate transit")]
            RateTransit = 38,
            [StringValue("Hazardous Charges")]
            HazardousCharges = 39,
            [StringValue("Origin Free Time")]
            OriginFreeTimeCode = 40,
            [StringValue("Destination Free Time")]
            DestinationFreeTimeCode = 41,
            [StringValue("Customer ID")]
            CustomerId = 42,
            [StringValue("SCOPE")]
            Scope = 43,
            [StringValue("LASTAMD")]
            LastAmd = 44
        }
        public enum OriginArbsColumn : int
        {
            [StringValue("contract_carrier")]
            ContractCarrier = 1,
            [StringValue("contract_rate_id")]
            ContractRateId = 2,
            [StringValue("contract_id")]
            ContractID = 3,
            [StringValue("contract_amendment_id")]
            ContractAmdId = 4,
            [StringValue("amendment_change")]
            AmendmentChange = 5,
            [StringValue("contract_effective_date")]
            ContractEffectiveDate = 6,
            [StringValue("contract_expiration_date")]
            ContractExpirationDate = 7,
            [StringValue("commodity_code")]
            CommodityCode = 8,
            [StringValue("origin_group")]
            OriginGroup = 9,
            [StringValue("origin_city")]
            OriginCity = 10,
            [StringValue("origin_state")]
            OriginState = 11,
            [StringValue("origin_country")]
            OriginCountry = 12,
            [StringValue("origin_UNLOC_code")]
            OriginUnloc = 13,
            [StringValue("origin_via_group_name")]
            OriginViaGroup = 14,
            [StringValue("origin_via_city")]
            OriginViaCity = 15,
            [StringValue("origin_via_state")]
            OriginViaState = 16,
            [StringValue("origin_via_country")]
            OriginViaCountry = 17,
            [StringValue("origin_via_UNLOC_code")]
            OriginViaUnloc = 18,
            [StringValue("origin_trade")]
            OriginTrade = 19,
            [StringValue("destination_trade")]
            DestinationTrade = 20,
            [StringValue("service")]
            Service = 21,
            [StringValue("mode")]
            Mode = 22,
            [StringValue("analyst notes")]
            AnalystNotes = 23,
            [StringValue("contract notes")]
            ContractNotes = 24,
            [StringValue("data entry notes")]
            DataEntryNotes = 25,
            [StringValue("rate type")]
            RateType = 26,
            [StringValue("rate_transit")]
            RateTransit = 27,
            [StringValue("Hazardous Charges")]
            HazardousCharges = 28,
            [StringValue("Origin Free Time Code")]
            OriginFreeTimeCode = 29,
            [StringValue("CUR")]
            RatesId = 30,
            [StringValue("SCOPE")]
            Scope = 31,
            [StringValue("LASTAMD")]
            LastAmd = 32,
            [StringValue("ARBO_20_DC_PC_USD")]
            BR20 = 33,
            [StringValue("ARBO_40_DC_PC_USD")]
            BR40 = 34,
            [StringValue("ARBO_40HC_DC_PC_USD")]
            BR40H = 35,
            [StringValue("ARBO_45_DC_PC_USD")]
            BR45 = 36,
        }

        public enum DestinationArbsColumn : int
        {
            [StringValue("contract_carrier")]
            ContractCarrier = 1,
            [StringValue("contract_rate_id")]
            ContractRateId = 2,
            [StringValue("contract_id")]
            ContractID = 3,
            [StringValue("contract_amendment_id")]
            ContractAmdId = 4,
            [StringValue("amendment_change")]
            AmendmentChange = 5,
            [StringValue("contract_effective_date")]
            ContractEffectiveDate = 6,
            [StringValue("contract_expiration_date")]
            ContractExpirationDate = 7,
            [StringValue("commodity_code")]
            CommodityCode = 8,
            [StringValue("origin_trade")]
            OriginTrade = 9,
            [StringValue("destination_group")]
            DestinationGroup = 10,
            [StringValue("destination_city")]
            DestinationCity = 11,
            [StringValue("destination_state")]
            DestinationState = 12,
            [StringValue("destination_country")]
            DestinationCountry = 13,
            [StringValue("destination_UNLOC_code")]
            DestinationUnloc = 14,
            [StringValue("destination_via_group_name")]
            DestinationViaGroup = 15,
            [StringValue("destination_via_city")]
            DestinationViaCity = 16,
            [StringValue("destination_via_state")]
            DestinationViaState = 17,
            [StringValue("destination_via_country")]
            DestinationViaCountry = 18,
            [StringValue("destination_via_UNLOC_code")]
            DestinationViaUnloc = 19,
            [StringValue("destination_trade")]
            DestinationTrade = 20,
            [StringValue("service")]
            Service = 21,
            [StringValue("mode")]
            Mode = 22,
            [StringValue("analyst notes")]
            AnalystNotes = 23,
            [StringValue("contract notes")]
            ContractNotes = 24,
            [StringValue("data entry notes")]
            DataEntryNotes = 25,
            [StringValue("rate type")]
            RateType = 26,
            [StringValue("rate_transit")]
            RateTransit = 27,
            [StringValue("Hazardous Charges")]
            HazardousCharges = 28,
            [StringValue("Destination Free Time Code")]
            DestinationFreeTimeCode = 29,
            [StringValue("CUR")]
            RatesId = 30,
            [StringValue("SCOPE")]
            Scope = 31,
            [StringValue("LASTAMD")]
            LastAmd = 32,
            [StringValue("ARBD_20_DC_PC_USD")]
            BR20 = 33,
            [StringValue("ARBD_40_DC_PC_USD")]
            BR40 = 34,
            [StringValue("ARBD_40HC_DC_PC_USD")]
            BR40H = 35,
            [StringValue("ARBD_45_DC_PC_USD")]
            BR45 = 36,
        }
        public enum GroupCodeColumn : int
        {
            [StringValue("Group Code")]
            GroupCode = 1,
            [StringValue("Location City")]
            LocationCity = 2,
            [StringValue("Location State")]
            LocationState = 3,
            [StringValue("Location Country")]
            LocationCountry = 4,
            [StringValue("Location Code")]
            LocationCode = 5,
            [StringValue("Trade")]
            Trade = 6,
            [StringValue("Org. Freetime")]
            OrgFreetime = 7,
            [StringValue("Dest. Freetime")]
            DestFreetime = 8
        }
        public static DataTable createModel(DataTable table)
        {
            DataTable newTable = new DataTable();
            foreach (DataColumn col in table.Columns)
                newTable.Columns.Add(new DataColumn(col.ColumnName, typeof(String)));

            foreach (DataRow row in table.Rows)
            {
                newTable.Rows.Add();
                foreach (DataColumn col in table.Columns)
                {
                    newTable.Rows[newTable.Rows.Count - 1][col.ColumnName] = row[col.ColumnName].ToString();
                }
            }

            return newTable;
        }
    }
}

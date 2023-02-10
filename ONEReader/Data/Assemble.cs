using ONEReader.Data;
using ONEReader.DataHelper;
using ONEReader.DataModel;
using ONEReader.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ONEReader.Build
{
    [XmlRoot("ONE")]
    public class OneCompile
    {
        [XmlElement("RATES")]
        public ItemDetails<RateDetails> _compileRates { get; set; }
        [XmlElement("GROUP_NOTES")]
        public ItemDetails<GroupDetails> _compileGnotes { get; set; }
        [XmlElement("ARBITRARY")]
        public ItemDetails<InlandDetail> _compileArbs { get; set; }
        [XmlElement("ARB_GROUP_NOTES")]
        public ItemDetails<GroupDetails> _compileGArbsNotes { get; set; }
        [XmlElement("CONTRACT_INFO")]
        public BasicInfo _basicInfo;
        [XmlElement("DATA_MISC")]
        public DataMisc dataMisc;
        public OneCompile(BasicInfo basicInfo, IItem<RateDetails> compileRates, IItem<GroupDetails> compileGNotes, IItem<InlandDetail> compileArbs, IItem<GroupDetails> compileGArbsNotes)
        {
            _basicInfo = basicInfo;
            _compileRates = (compileRates as ItemDetails<RateDetails>);
            _compileGnotes = (compileGNotes as ItemDetails<GroupDetails>);

            _compileArbs = (compileArbs as ItemDetails<InlandDetail>);
            _compileGArbsNotes = (compileGArbsNotes as ItemDetails<GroupDetails>);
            dataMisc = new DataMisc(
                compileRates.Cast<RateDetails>().Count(),
                compileArbs.Cast<InlandDetail>().Count(),
                compileGNotes.Cast<GroupDetails>().Count(),
                compileGArbsNotes.Cast<GroupDetails>().Count()
                );
        }
        public OneCompile() { }
    }
    public class Assemble: IDisposable
    {
        private bool foundMainSearch = false;
        private bool rateLocationFound = false;
        private bool arbLocationFound = false;
        private IPatternXML _pattern;
        private IContractsheet _contract;
        private BasicInfo _basicInfo;
        private IHeader headerRate;
        private IHeader headerArb;
        
        public ICompile<RateDetails> compileRates;
        public ICompile<InlandDetail> compileArbs;
        

        public Assemble(IPatternXML pattern, IContractsheet contract)
        {
            _basicInfo = new BasicInfo(contract.contractInfo.carrier, contract.contractInfo.eff.convertDateFormat(), contract.contractInfo.exp.convertDateFormat(), contract.contractInfo.contractId, contract.contractInfo.amdId, contract.sheetName);
            _pattern = pattern;
            _contract = contract;
            //INITIALIZE  HEADER(SCOPE COMMMODITY, ORIGIN and ORIGIN VIA) DATA AND COMPILED DATA
            compileRates = new CompileRates<RateDetails>(_contract);
            compileArbs = new CompileArbs<InlandDetail>(_contract);
            headerRate = new HeaderRate();
            headerArb = new HeaderArb();
        }
        public void ReadData()
        {
            //START CONTRACT READER ALGORITHM
            //START RATES
            for (; _contract.currentLine < _contract.getTotalRowsUsed; _contract.nextLine())
            {
                string strStored = _contract.GetEntireRow(_contract.currentLine);
                if( !String.IsNullOrWhiteSpace(strStored))
                {
                    //LOCATE THE STARTING KEYWORD
                    if(_contract.pattern.isMainSearchExist(strStored)) foundMainSearch = true;
                    if(_contract.pattern.isKillSearchExist(strStored)) break;
                    if( foundMainSearch)
                    {
                        SetCurrentSearchLocation(_contract, strStored);
                        if (rateLocationFound)
                        {
                            ExtractAndCompileData(headerRate, compileRates, strStored);
                        }
                        if ( arbLocationFound)
                        {
                            ExtractAndCompileData(headerArb, compileArbs, strStored);
                        }
                    }
                   
                }
            }
        }
        public void WriteData()
        {
            var oneCompile = new OneCompile(_basicInfo, compileRates.items, compileRates.groupItems , compileArbs.items, compileArbs.groupItems);
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(oneCompile.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, oneCompile);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            //Console.WriteLine(xmlString);
            XElement xElement = XElement.Parse(xmlString);

            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string guid = Guid.NewGuid().ToString().ToUpper();
            Directory.CreateDirectory(documentPath + "/CReader/" + guid);
            xElement.Save(documentPath + "/CReader/" + guid + "/myset.xml");
            //xElement.Save(@"C:\Users\nel\Desktop\REFERENCE FILE\myset.xml");
        }
        private void ExtractAndCompileData<T>(IHeader header, ICompile<T> compile, string strStored) where T : class
        {
            //EXTRACTION OF SCOPE, COMMODITY, ORIGIN and ORIGIN VIA
            if (_contract.pattern.isHeaderExist(strStored) || _contract.pattern.isArbsHeaderExist(strStored))
            {

                //Console.WriteLine(strStored);
                _contract.GetHeaderData(header, out strStored);
                header.StartNewExtract();
            }
            //EXTRACTION OF TABLE AND NOTES
            //Console.WriteLine(header);
            List<TableContent> bodyString = new List<TableContent>();
            FooterContent footer = new FooterContent();
            if (_contract.pattern.isTableExist(strStored))
            {
                bodyString = _contract.GetTableData();

                if (header.Type == DataModel.RateType.RATE)
                {
                    footer = _contract.GetNotes(header);
                }
                else
                {
                    footer = _contract.GetArbNotes(header);
                }
                _contract.backLine();
                compile.CompileData(header, bodyString, footer);
            }
            Console.WriteLine("Break");
        }
        private void SetCurrentSearchLocation(IContractsheet contract, string sStore)
        {
            if( contract.pattern.isMainRatesHeader(sStore))
            {
                rateLocationFound = true;
                arbLocationFound = false;
            }
            else if( contract.pattern.isMainArbsHeader(sStore))
            {
                rateLocationFound = false;
                arbLocationFound = true;
            }
        }
        public void Dispose()
        {
            _pattern = null;
            _contract = null;
            headerRate = null;
            headerArb = null;
            compileRates = null;
            compileArbs = null;
        }

    }
}

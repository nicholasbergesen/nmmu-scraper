using System;
using HtmlParser;

namespace NMMU_Scraper
{
    internal class Program
    {
        public static string? VolumeNo { get; private set; }
        public static string? RateNumber { get; private set; }
        public static string? StreetNo { get; private set; }
        public static string? StreetName { get; private set; }
        public static string? Suburb { get; private set; }
        public static string? ERF { get; private set; }
        public static string? Portion { get; private set; }
        public static string? DeedsTown { get; private set; }
        public static string? SchemeName { get; private set; }
        public static string? SectionNumber { get; private set; }
        public static HttpClient httpClient = new();

        static async Task Main(string[] args)
        {
            Dictionary<string, string> suburbs = new()
            {
                { "ADCOCKVALE", "12078" },
                { "ADMIRALTY WAY", "12079" },
                { "ALGOA PARK", "12080" },
                { "ALGOA PARK&amp; NEW BIRG", "12081" },
                { "ALOES", "12082" },
                { "AMSTERDAMHOEK", "12083" },
                { "ARCADIA", "12084" },
                { "BARCELONA", "12085" },
                { "BEACHVIEW", "12086" },
                { "BEN KAMMA", "12087" },
                { "BETHELSDORP", "12088" },
                { "BETHELSDORP EXT 21", "12089" },
                { "BETHELSDORP EXT 22", "12090" },
                { "BETHELSDORP EXT 27", "12091" },
                { "BETHELSDORP EXT 28", "12092" },
                { "BETHELSDORP EXT 30", "12093" },
                { "BETHELSDORP EXT 31", "12094" },
                { "BETHELSDORP EXT 32", "12095" },
                { "BETHELSDORP EXT 33", "12096" },
                { "BETHELSDORP EXT 34", "12097" },
                { "BETHELSDORP EXT 35", "12098" },
                { "BETHELSDORP EXT 36", "12099" },
                { "BETHELSDORP EXT 37", "12100" },
                { "BETHELSDORP X24", "12101" },
                { "BEVERLEY GROVE", "12102" },
                { "BLOEMENDAL", "12103" },
                { "BLOEMENDAL BLOCK 23", "12104" },
                { "BLOEMENDAL EXT 6", "12105" },
                { "BLUE HORIZON BAY", "12106" },
                { "BLUE WATER BAY", "12107" },
                { "BLUEWATER BAY", "12108" },
                { "BOOYSENS PARK", "12109" },
                { "BRAMHOPE", "12110" },
                { "BRENTWOOD PARK", "12111" },
                { "BRIDGEMEAD", "12112" },
                { "BROADWOOD", "12113" },
                { "BUFFELSFONTEIN VILL", "12114" },
                { "BURMAN ROAD", "12115" },
                { "BURT DRIVE", "12116" },
                { "CAMPHER PARK", "12117" },
                { "CANNONVILLE", "12118" },
                { "CAPE ROAD INDUSTRIAL", "12119" },
                { "CENTRAL", "12120" },
                { "CHARLO", "12121" },
                { "CHATTY", "12122" },
                { "CHATTY EXT 17", "12123" },
                { "CHELSEA", "12124" },
                { "CHELSEA FARM", "12125" },
                { "CHURCH ROAD", "12126" },
                { "CLARENDON MARINE", "12127" },
                { "CLEARY ESTATE", "12128" },
                { "CLEARY PARK", "12129" },
                { "Coega", "15565" },
                { "COLCHESTER", "12130" },
                { "COLLEEN GLEN", "12131" },
                { "COTSWOLD", "12132" },
                { "CRADOCK PLACE", "12133" },
                { "CRESC WALMER HEIGHTS", "12134" },
                { "CROCKARTS HOPE", "12135" },
                { "CURRY", "12136" },
                { "DEAL PARTY", "12137" },
                { "DEAPATCH", "12138" },
                { "DENHOLME", "12139" },
                { "DESPATCH", "12140" },
                { "DRIVE BLOEMENDAL", "12141" },
                { "DWADWESI", "12142" },
                { "DYKE ROAD ALGOA PARK", "12143" },
                { "ECHOVALE", "12144" },
                { "EMERALD HILL", "12145" },
                { "FAIRVIEW", "12146" },
                { "FARM DRAAIFONTEIN", "12147" },
                { "FARM KRAGGA KAMMA", "12148" },
                { "FARMS PORT ELIZABETH", "12149" },
                { "FARMS UITENHAGE", "12150" },
                { "FERGUSON TOWNSHIP", "12151" },
                { "FERNGLEN", "12152" },
                { "FERNWOOD PARK", "12153" },
                { "FITCHHOLME", "12154" },
                { "FORDVILLE", "12155" },
                { "FOREST HILL", "12156" },
                { "FOUNTAIN ST WALMER", "12157" },
                { "FRAMESBY", "12158" },
                { "FRAMESBY EXT 1", "12159" },
                { "FRAMESBY NORTH", "12160" },
                { "FRANCIS EVATT PARK", "12161" },
                { "GAP TAP KWAZAKHELE", "12162" },
                { "GELVAN PARK", "12163" },
                { "GELVANDALE", "12164" },
                { "GELVANDALE EXT 12", "12165" },
                { "GLEN HURD", "12166" },
                { "GLENDINNINGVALE", "12167" },
                { "GOLDWATER", "12168" },
                { "GOMERY AVENUE", "12169" },
                { "GREENACRES", "12170" },
                { "GREENBUSHES", "12171" },
                { "GREENFIELDS", "12172" },
                { "GREENSHIELDS PARK", "12173" },
                { "HARBOUR", "12174" },
                { "HELENVALE", "12175" },
                { "HEUWELKRUIN", "12176" },
                { "HILLSIDE", "12177" },
                { "HOETS CRESCENT", "12178" },
                { "HOLLAND PARK", "12179" },
                { "HUMERAIL", "12180" },
                { "HUMEWOOD", "12181" },
                { "HUNTERS", "12182" },
                { "HUNTERS RETREAT", "12183" },
                { "IBHAYI", "12184" },
                { "JACHTVLAKTE", "12185" },
                { "JANSENDAL", "12186" },
                { "JOE SLOVO", "12187" },
                { "KABAH/LANGA", "12188" },
                { "KABEGA", "12189" },
                { "KABEGA PARK", "12190" },
                { "KAMMA CREEK", "12191" },
                { "KAMMA HEIGHTS", "12192" },
                { "KAMMA PARK", "12193" },
                { "KAMMA RIDGE", "12194" },
                { "KEMSLEY PARK", "12195" },
                { "KENSINGTON", "12196" },
                { "KINI BAY", "12197" },
                { "KLEINSKOOL", "12198" },
                { "KORSTEN", "12199" },
                { "KORSTEN/HOLLAND PARK", "12200" },
                { "KRAGGA KAMMA PARK", "12201" },
                { "KUNENE PARK", "12202" },
                { "KUWAIT", "12203" },
                { "KUYGA", "12204" },
                { "KWADESI", "12205" },
                { "KWADWESI", "12206" },
                { "KWAFORD", "12207" },
                { "KWAMAGXAKI", "12208" },
                { "KWANMAGXAKI", "12209" },
                { "KWANOBUHLE", "12210" },
                { "KWAZAKHELE", "12211" },
                { "KWWAZAKHELE", "12212" },
                { "LINKSIDE", "12213" },
                { "LINTON GRANGE", "12214" },
                { "LITTLE CHELSEA NO 10", "12215" },
                { "LONDT PARK", "12216" },
                { "LONGWAY AVENUE", "12217" },
                { "LORRAINE", "12218" },
                { "LOVEMORE CRESCENT", "12219" },
                { "LOVEMORE HEIGHTS", "12220" },
                { "LOVEMORE PARK", "12221" },
                { "MAIN STREET CENTRAL", "12222" },
                { "MALABAR", "12223" },
                { "MANGOLD PARK", "12224" },
                { "MANOR HEIGHTS", "12225" },
                { "MARAIS TOWNSHIP", "12226" },
                { "MARINE DRIVE", "12227" },
                { "MARINER'S ROW", "12228" },
                { "MARKMAN INDUSTRIAL TOWNSHIP", "12229" },
                { "MASANGWANAVILLE", "12230" },
                { "MAY WAY SUNRIDGE PK", "12231" },
                { "MCNAUGHTON", "12232" },
                { "MHLABA VILLAGE", "12233" },
                { "MILL PARK", "12234" },
                { "MILLARD GRANGE", "12235" },
                { "MIRAMAR", "12236" },
                { "MISSIONVALE", "12237" },
                { "MNQUMA STR KWADWESI", "12238" },
                { "MORNINGSIDE", "12239" },
                { "MOSEL", "12240" },
                { "MOTHERWELL", "12241" },
                { "MOUNT CROIX", "12242" },
                { "MOUNT PLEASANT", "12243" },
                { "MURRAY PARK", "12244" },
                { "NEAVE TOWNSHIP", "12245" },
                { "NEW BRIGHTON", "12246" },
                { "NEWTON PARK", "12247" },
                { "NORTH END", "12248" },
                { "NORTHCLIFFE HUMEWOOD", "12249" },
                { "OVERBAAKENS", "12250" },
                { "PAPENKUILSVALLEY", "12251" },
                { "PARI PARK", "12252" },
                { "PARK DRIVE", "12253" },
                { "PARKSIDE", "12254" },
                { "PARSONS HILL", "12255" },
                { "PARSONS VLEI", "12256" },
                { "PATERSON ROAD", "12257" },
                { "PERRIDGEVALE", "12258" },
                { "PERSEVERANCE", "12259" },
                { "POPLAR AVENUE", "12260" },
                { "PORT ELIZABETH", "12261" },
                { "PRIMROSE SQUARS", "12262" },
                { "PRINGLE AVENUE", "12263" },
                { "PROVIDENTIA", "12264" },
                { "REDHOUSE", "12265" },
                { "RENDALLTON", "12266" },
                { "RETIEF", "12267" },
                { "ROADWAYS", "12268" },
                { "Rocklands", "15522" },
                { "ROWALLAN PARK", "12269" },
                { "SALSONEVILLE", "12270" },
                { "SALT LAKE", "12271" },
                { "SANCTOR", "12272" },
                { "SARDINIA BAY", "12273" },
                { "SCHAUDERVILLE", "12274" },
                { "SCHOENMAKKERSKOP", "12275" },
                { "SEAVIEW", "12276" },
                { "SHERWOOD", "12277" },
                { "SIDWELL", "12278" },
                { "SILVERTOWN", "12279" },
                { "SISONKE", "12280" },
                { "SITE &amp; SERVICE", "12281" },
                { "SKEGNESS ROAD", "12282" },
                { "SOUTH END", "12283" },
                { "SOUTH END CEMETERY", "12284" },
                { "SOWETO-ON-SEA", "12285" },
                { "SPORTS CLUB", "12286" },
                { "SPRINGDALE", "12287" },
                { "ST GEORGES PARK", "12288" },
                { "ST GEORGES STRAND", "12289" },
                { "STARLING CRESCENT", "12290" },
                { "STELLA LONDT DRIVE", "12291" },
                { "STEVE TSHWETE VILLAG", "12292" },
                { "STEYTLER TOWNSHIP", "12293" },
                { "STR WALMER HEIGHTS", "12294" },
                { "STRUANDALE", "12295" },
                { "STUART TOWNSHIP", "12296" },
                { "SUMMERSTRAND", "12297" },
                { "SUNRIDGE PARK", "12298" },
                { "SWARTKOPS", "12299" },
                { "SYDENHAM", "12300" },
                { "TAYBANK", "12301" },
                { "THAMBO VILLAGE", "12302" },
                { "THE FARM CHELSEA", "12303" },
                { "THEESCOMBE", "12304" },
                { "THEMBALETHU", "12305" },
                { "THRUSH ROAD", "12306" },
                { "TJOKSVILLE", "12307" },
                { "TJOKSVILLE M/WELL", "12308" },
                { "TREEHAVEN", "12309" },
                { "TUDOR GARDENS", "12310" },
                { "TULBAGH", "12311" },
                { "UITENHAGE", "12312" },
                { "UNKNOWN", "12313" },
                { "VALLEISIG", "12314" },
                { "VAN DER STEL", "12315" },
                { "VAN RIEBEECK HOOGTE", "12316" },
                { "VAN STADENS RIVER", "12317" },
                { "VANES ESTATES", "12318" },
                { "VICTORIA PARK", "12319" },
                { "VIKINGVALE", "12320" },
                { "WALMER", "12321" },
                { "WALMER AREA B", "12322" },
                { "WALMER DOWNS", "12323" },
                { "WALMER DUNES", "12324" },
                { "WALMER HEIGHTS", "12325" },
                { "WALMER LOCATION", "12326" },
                { "WALMER TOWNSHIP", "12327" },
                { "WAY TOWNSHIP", "12328" },
                { "WEDGEWOOD", "15504" },
                { "WELLS ESTATE", "12329" },
                { "WEST END", "12330" },
                { "WESTERING", "12331" },
                { "WESTLANDS", "12332" },
                { "WEYBRIDGE PARK", "12333" },
                { "WHYTELEAF DRIVE", "12334" },
                { "WINCHESTER WAY", "12335" },
                { "WINDVOGEL", "12336" },
                { "WONDERVIEW", "12337" },
                { "WOODLANDS", "12338" },
                { "WOODLANDS PARK", "12339" },
                { "YOUNG PARK", "12340" },
                { "ZWIDE", "12341" },
            };

            foreach (var suburb in suburbs)
            {
                Console.WriteLine($"{suburb.Key} properties:");
                Suburb = suburb.Value;

                var url = $"https://www.nelsonmandelabay.gov.za/propertyregister/FramePages/SearchResult.aspx?Roll=1&VolumeNo={VolumeNo}&RateNumber={RateNumber}&StreetNo={StreetNo}&StreetName={StreetName}&Suburb={Suburb}&ERF={ERF}&Portion={Portion}&DeedsTown={DeedsTown}&SchemeName={SchemeName}&SectionNumber={SectionNumber}&All=true";
                var properties = GetPropertiesForSuburb(url);

                await foreach (var property in properties)
                {
                    Console.WriteLine(property);
                }

                await Task.Delay(5000); //don't kill their site by spamming requests, should implement exponential backoff.
            }
        }

        private static async IAsyncEnumerable<string> GetPropertiesForSuburb(string url)
        {
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var htmlNodes = Parser.Parse(content, true);
            var resultTable = htmlNodes.First(x => x.Type == NodeType.table
                                && x.Attributes.TryGetValue("class", out string? classString)
                                && classString.Contains("searchResultTable"));

            var resultTableRows = htmlNodes.Where(x => x.Type == NodeType.tr
                                    && resultTable.OpenPosition < x.OpenPosition
                                    && x.ClosedPosition < resultTable.ClosedPosition
                                    && x.Depth > resultTable.Depth);

            foreach (var tableTow in resultTableRows)
            {
                var columns = htmlNodes.Where(x => x.Type == NodeType.td
                                && tableTow.OpenPosition < x.OpenPosition
                                && tableTow.ClosedPosition > x.ClosedPosition
                                && x.Depth > tableTow.Depth);

                List<string> propertyDetails = new();
                foreach (var column in columns)
                {
                    if (string.IsNullOrWhiteSpace(column.Content))
                    {
                        break;
                    }

                    int fromIndex = column.Content.IndexOf('>') + 1;
                    int toIndex = column.Content.LastIndexOf('<');

                    //fragile way to handle ErfKey and Legal Description, wrapped in anchor tag.
                    if (column.Content.StartsWith("<td align=\"left\">\r\n              <a"))
                    {
                        fromIndex = column.Content.IndexOf("();") + 6;
                        toIndex = column.Content.IndexOf("</a>");
                    }

                    propertyDetails.Add(column.Content[fromIndex..toIndex].Trim());
                }

                if (propertyDetails.Any())
                {
                    yield return string.Join(',', propertyDetails);
                }
            }
        }
    }
}
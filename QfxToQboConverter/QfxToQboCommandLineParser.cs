using System;
using System.IO;
using Westwind.Utilities.System;

namespace QfxToQboConverter
{
    public class QfxToQboCommandLineParser : CommandLineParser
    {
        public string QfxFile { get; set; }
        public string QboFile { get; set; }
        public bool Quiet { get; set; }
        public bool IsHelp { get; set;  }


        /// <summary>
        /// BankId code to 'fake' in import - this is hte bank that will show up in your list
        /// In Quickbooks these show up in the Web Connect Dialog:
        /// Chase Bank  2430    2200 - Affinity Credit Union  19 - Alberta Credit Union
        /// Canadian banks: https://docs.com/ParkwayInc/8952/intuit-s-intu-bid-list-for-canadian-banks
        /// </summary>
        public string BankId { get; set; } = "2200";  //2430   

        public QfxToQboCommandLineParser(string[] args = null, string cmdLine = null) : base(args, cmdLine)
        {
            
        }
        
        public override void Parse()
        {
            if (Args.Length > 0)                
                QfxFile  = Args[0];

            Quiet = ParseParameterSwitch("-n");
            IsHelp = ParseParameterSwitch("/?");

            QboFile = ParseStringParameterSwitch("-o",QboFile);
            BankId = ParseStringParameterSwitch("-b", BankId);
        }
        
    }
}
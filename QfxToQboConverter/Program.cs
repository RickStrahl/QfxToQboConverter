using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Westwind.Utilities;

namespace QfxToQboConverter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var color = Console.ForegroundColor;

            var cmdLine = new QfxToQboCommandLineParser();
            cmdLine.Parse();

            if (cmdLine.IsHelp)
            {
                

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"
Qfx to Qbo File Converter - converts Quicken QIF files to QuickBooks QBO files
by 'faking' a known <INTU.BID> value and assigning it to an existing QIF file.");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(@"
Syntax:
-------
QfxToQbo  inputFile [-o outputFile] [/? Help] [-n noAutoOpen] [-b bankId (default 2200)]

Arguments:
----------
inputfile  - the QFX input file. If not specified you'll be prompted.

-o QBO output file to create
-n Don't open in QuickBooks automaticall
-b Fake Bank Id to assign - default is 2200 

");
                Console.ForegroundColor = color;
                return;
            }

            if (string.IsNullOrEmpty(cmdLine.QfxFile) || !File.Exists(cmdLine.QfxFile))
            {
                var fd = new OpenFileDialog
                {
                    DefaultExt = ".qfx",
                    Filter = "Qfx files (*.qfx)|*.qfx|" +
                             "All files (*.*)|*.*",
                    CheckFileExists = true,
                    RestoreDirectory = true,
                    Multiselect = false,
                    Title = "Select Qfx file to convert"
                };

                fd.InitialDirectory =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

                var res = fd.ShowDialog();

                if (res.HasFlag(DialogResult.Cancel))
                    return;

                cmdLine.QfxFile = fd.FileName;
            }

            if (string.IsNullOrEmpty(cmdLine.QboFile))
                cmdLine.QboFile = Path.ChangeExtension(cmdLine.QfxFile, "qbo");


            if (!File.Exists(cmdLine.QfxFile))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input file doesn't exist.");
                Console.ForegroundColor = color;
                return;
            }
            
            string content = File.ReadAllText(cmdLine.QfxFile);

            string block = StringUtils.ExtractString(content, "<INTU.BID>", "\n",returnDelimiters: true);
            
            content = content.Replace(block, $"<INTU.BID>{cmdLine.BankId}\n");

            File.WriteAllText(cmdLine.QboFile, content);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Qbo file created: " + cmdLine.QboFile);

            
            ShellUtils.GoUrl(cmdLine.QboFile);

            Console.ForegroundColor = color;
            
        }
    }
}

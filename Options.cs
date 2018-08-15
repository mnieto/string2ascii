using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace string2ascii {
    class Options {

        [Option('x', "hex", HelpText = "Output format to hexadecimal", SetName = "OutputFormat")]
        public bool Hexadecimal { get; set; }

        [Option('d', "dec", HelpText = "Output format to decimal (default)", SetName = "OutputFormat")]
        public bool Decimal { get; set; }

        [Value(0, HelpText = "Input string. If not specified, taken from stdin")]
        public IEnumerable<string> Phrase { get; set; }

        [Option('w', "wait", HelpText = "Wait message to finish")]
        public bool Wait { get; set; }


        public string OutputFormat {
            get {
                if (!Decimal && !Hexadecimal) {
                    Decimal = true;
                }
                return Decimal ? "D" : "X";
            }
        }

        public string GetPhrase() {
            string phrase = string.Join(' ', Phrase);
            if (string.IsNullOrEmpty(phrase)) {
                if (Console.IsInputRedirected) {
                    using (var sr = new StreamReader(Console.OpenStandardInput())) {
                        phrase = sr.ReadLine();
                    }
                } else {
                    Console.WriteLine("Enter a phrase");
                    phrase = Console.ReadLine();
                }
            }
            return phrase;
        }

    }
}

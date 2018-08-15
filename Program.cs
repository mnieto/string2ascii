using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace string2ascii
{
    class Program
    {

        //syntax: string2ascii [-w][-x|-d][-h] phrase
        // -w wait message to finish
        // -x output format to hexadecimal
        // -d output format to decimal
        // -h this help
        static void Main(string[] args) {

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options => Execute(options))
                .WithNotParsed(errors => HandleErrors(errors));

        }

        private static int Execute(Options options) {
            string phrase = options.GetPhrase();
            int[] ascii = ToIntArray(phrase);

            string format = string.Concat("{0:", options.OutputFormat, "}");
            WriteBytes(ascii, format);

            if (options.Wait) {
                Wait();
            }
            return 0;
        }

        private static int HandleErrors(IEnumerable<Error> errors) {
            return -1;
        }

        private static int[] ToIntArray(string phrase) {
            int[] ascii = new int[phrase.Length];
            for (int i = 0; i < phrase.Length; i++) {
                ascii[i] = phrase[i];
            }
            return ascii;
        }

        private static void WriteBytes(int[] ascii, string format) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ascii.Length; i++) {
                sb.AppendFormat(format, ascii[i]);
                sb.Append(' ');
            }
            Console.WriteLine(sb.ToString(0, sb.Length - 1));
        }

        private static void Wait() {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

    }
}

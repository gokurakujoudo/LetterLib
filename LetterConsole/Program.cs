using System;
using System.IO;
using LetterLib.Data;
using LetterLib.Utility;
using SharpHelper.Util;

namespace LetterConsole {
    class Program {
        static void Main(string[] args) {
            var tempPath = args?[0];
            if (!File.Exists(tempPath)) return;
            Util.WriteLine($"Loading File {tempPath}");
            var temp = XmlSerializationHelper.FromXmlFile<DocTemplate>(tempPath);



            temp.BuildFile();
            WordHelper.SafeQuit();


            //temp.Resources.Add(
            //    new ParaResource {ParaName = "PN", ResourceName = "RN", ParaValue = "PV"});
            temp.ToXmlFile(tempPath);

            Util.WriteLine("AllDone");
            Console.Read();
        }
    }
}

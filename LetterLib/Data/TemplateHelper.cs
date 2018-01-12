using System;
using LetterLib.Utility;
using SharpHelper.Util;

namespace LetterLib.Data {
    public static class TemplateHelper {
        public static DocTemplate CurrenTemplate;
        public static void Save(string fpath) => CurrenTemplate.Save(fpath);
        public static void Init(string fpath) => CurrenTemplate = Load(fpath);

        public static void Save(this DocTemplate template, string fpath, int level = 0) {
            Util.WriteLine("Saving template file", level);
            Util.WriteLine($"Saving path {fpath}", level);
            try {
                template.ToXmlFile(fpath);
            }
            catch (Exception e) {
                Util.WriteLine(e.ToString(), level + 1);
                throw;
            }
            Util.WriteLine("Template file saved", level);
        }

        public static DocTemplate Load(string fpath, int level = 0) {
            Util.WriteLine("Loading template file", level);
            try {
                Util.WriteLine($"Template file path: {fpath}", level);
                var template = XmlSerializationHelper.FromXmlFile<DocTemplate>(fpath);
                Util.WriteLine("Template file loaded", level);
                return template;
            }
            catch (Exception e) {
                Util.WriteLine(e.ToString(), level + 1);
                throw;
            }

        }
    }
}

using System.IO;
using System.Linq;
using System.Text;
using LetterLib.Data;
using Microsoft.Office.Interop.Word;

namespace LetterLib.Utility {
    public static class ReplaceHelper {
        public static string GetFindString(this FieldSlot fieldSlot) => $"`{fieldSlot.FieldName}`";
        public static string GetFindString(this ParaSlot para) => $"[{para.ParaName}-{para.ParaSubName}]";
        public static string GetFindString(this ParaResource resource) => $"${resource.ResourceName}$";

        public static bool IsValidFieldName(string name) {
            if (string.IsNullOrWhiteSpace(name)) return false;
            if (name.Contains("[") || name.Contains("]")) return false;
            return true;
        }

        public static bool IsValidParaSlotName(string name) {
            if (string.IsNullOrWhiteSpace(name)) return false;
            if (name.Contains("[") || name.Contains("]") || name.Contains("-")) return false;
            return true;
        }

        public static void BuildFile(this DocTemplate template, bool pdf = true, bool overwrite = true, int level = 0) {
            Util.WriteLine("Start building file", level);
            if (template == null) {
                Util.WriteLine("Template not found, quit", level + 1);
                return;
            }
            Util.WriteLine($"Template file path: {template.TemplateFilePath}", level);
            if (!File.Exists(template.TemplateFilePath)) {
                Util.WriteLine("File not found, quit", level + 1);
                return;
            }
            var fileName = template.GetOutputFileName(PreDefinedFields.DOCX_EXT);
            var fullName = Util.ToFullPath(fileName);
            if (File.Exists(fullName) && !overwrite) {
                Util.WriteLine($"File does exist: {fullName}, quit", level + 1);
                return;
            }
            File.Copy(template.TemplateFilePath, fullName, overwrite);
            var doc = WordHelper.OpenWordFile(fullName);
            doc.Replace(template, level + 1);
            Util.WriteLine($"Export Word file {fileName}", level + 1);
            doc.Save();
            var pdfFullName = string.Empty;
            if (pdf) {
                var pdfName = template.GetOutputFileName(PreDefinedFields.PDF_EXT);
                pdfFullName = Util.ToFullPath(pdfName);
                if (File.Exists(pdfFullName)) File.Delete(pdfFullName);
                doc.ExportPdf(pdfFullName);
                Util.WriteLine($"Export PDF file {pdfName}", level + 1);
            }
            Util.WriteLine("End building file", level);
            Util.WriteLine($"Output Word file path: {fullName}", level);
            if (pdf) Util.WriteLine($"Output PDF file path: {pdfFullName}", level);
            doc.Close(true);
        }

        public static string GetOutputFileName(this DocTemplate template, string ext) {
            var sb = new StringBuilder(template.OutputFileNameTemplate);
            foreach (var field in template.Fields)
                sb.Replace(field.GetFindString(), field.FieldValue);
            foreach (var field in PreDefinedFields.DefinedFields)
                sb.Replace(field.Key, field.Value());
            sb.Replace(PreDefinedFields.EXT_FIELD, ext);
            return sb.ToString();
        }

        public static void Replace(this Document doc, DocTemplate template, int level=0) {
            Util.WriteLine("Start replacing", level);
            if (doc == null || template == null) return;
            doc.Activate();
            doc.ReplaceParaSlots(template, level + 1);
            doc.ReplaceCustomFields(template, level + 1);
            doc.ReplaceDefinedFields(template, level + 1);
            Util.WriteLine("Finish replacing", level);

        }

        public static void ReplaceParaSlots(this Document doc, DocTemplate template, int level = 0) {
            if (!template.ParaSlots.Any()) return;
            Util.WriteLine("Start replacing paragraph slots", level);
            foreach (var slot in template.ParaSlots) {
                Util.WriteLine($"Start replacing paragraph {slot.GetFindString()}", level + 1);
                var replaceText = new StringBuilder(slot.ParaValue.Trim());
                if (template.ResourceList().TryGetValue(slot.ParaName, out var resources)) {
                    foreach (var res in resources) {
                        Util.WriteLine($"Replacing resource {res.ResourceName}", level + 2);
                        replaceText.Replace(res.GetFindString(), res.ParaValue);
                    }
                }
                doc.FindAndReplaceLong(slot.GetFindString(), replaceText.ToString());
            }
            Util.WriteLine("Finish replacing paragraph slots", level);
        }

        public static void ReplaceCustomFields(this Document doc, DocTemplate template, int level = 0)
        {
            if (!template.Fields.Any()) return;
            Util.WriteLine("Start replacing custom field slots", level);
            foreach (var field in template.Fields)
            {
                Util.WriteLine($"Replacing field {field.FieldName} -> {field.FieldValue}", level + 1);
                doc.FindAndReplaceLong(field.GetFindString(), field.FieldValue);
            }
            Util.WriteLine("Finish replacing custom field slots", level);
        }

        public static void ReplaceDefinedFields(this Document doc, DocTemplate template, int level = 0) {
            Util.WriteLine("Start replacing custom field slots", level);
            //Util.WriteLine("Current unavailable", level);
            var dict = PreDefinedFields.DefinedFields;
            foreach (var pair in dict) {
                var value = pair.Value();
                Util.WriteLine($"Replacing pre-defined field {pair.Key} -> {value}", level + 1);
                doc.FindAndReplaceLong(pair.Key, value);
            }

            Util.WriteLine("Finish replacing custom field slots", level);
        }
    }
}

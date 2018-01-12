using System;
using System.IO;
using Microsoft.Office.Interop.Word;

namespace LetterLib.Utility {
    public static class WordHelper {
        public static readonly Application App = OpenWord();

        private static Application OpenWord() {
            Util.WriteLine("Starting new Word application");
            var app =  new Application { Visible = false };
            Util.WriteLine("New Word application loaded");
            return app;
        }

        public static void SafeQuit() {
            Util.WriteLine("Closing Word application");
            try {
                App.Quit(false);
            }
            catch (Exception e) {
                Util.WriteLine(e.ToString());
                throw;
            }
            Util.WriteLine("Word application closed");
        }

        public static Document OpenWordFile(string fpath) {
            //var fullPath = Path.Combine(Directory, fpath);
            if (!File.Exists(fpath)) throw new FileNotFoundException("File not found", fpath);
            var doc = App.Documents.Open(fpath, ReadOnly: false, Visible: true);
            return doc;
        }

        //public static void CloseWordFile(this Document doc, bool save = false) { doc.Close(save); }

        private static void FindAndReplace(this Document doc, object findText, object replaceWithText) {
            //options
            object matchCaseO = false;
            object matchWholeWordO = false;
            object matchWildCardsO = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object replace = 2;
            object wrap = 1;
            //execute find and replace
            doc.Activate();
            App.Selection.Find.Execute(ref findText, ref matchCaseO, ref matchWholeWordO,
                                       ref matchWildCardsO, ref matchSoundsLike, ref matchAllWordForms, ref forward,
                                       ref wrap, ref format, ref replaceWithText, ref replace,
                                       ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }

        public static void FindAndReplaceLong(this Document doc, object findText, object replaceText) {
            int len = replaceText.ToString().Length; //要替换的文字长度
            int cnt = len / 220; //不超过220个字
            if (len < 220) //小于220字直接替换
            {
                FindAndReplace(doc, findText, replaceText);
            }
            else {
                for (int i = 0; i <= cnt; i++) {
                    string newstr;
                    if (i != cnt)
                        newstr = replaceText.ToString().Substring(i * 220, 220) + findText; //新的替换字符串
                    else
                        newstr = replaceText.ToString().Substring(i * 220, len - i * 220); //最后一段需要替换的文字
                    var newStrs = (object) newstr;
                    FindAndReplace(doc, findText, newStrs); //进行替换
                }
            }
        }

        public static void ExportPdf(this Document doc, string fpath, bool open = true) {
            if (doc == null) return;
            const WdExportFormat paramExportFormat = WdExportFormat.wdExportFormatPDF;
            const WdExportOptimizeFor paramExportOptimizeFor = WdExportOptimizeFor.wdExportOptimizeForPrint;
            const WdExportRange paramExportRange = WdExportRange.wdExportAllDocument;
            const int paramStartPage = 0;
            const int paramEndPage = 0;
            const WdExportItem paramExportItem = WdExportItem.wdExportDocumentContent;
            const WdExportCreateBookmarks paramCreateBookmarks = WdExportCreateBookmarks.wdExportCreateWordBookmarks;
            var paramMissing = Type.Missing;

            doc.ExportAsFixedFormat(fpath,
                                    paramExportFormat, open,
                                    paramExportOptimizeFor, paramExportRange, paramStartPage,
                                    paramEndPage, paramExportItem, true,
                                    true, paramCreateBookmarks, true,
                                    true, false,
                                    ref paramMissing);
        }
    }
}

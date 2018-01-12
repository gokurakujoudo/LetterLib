using System;
using System.Collections.Generic;

namespace LetterLib.Utility {
    public static class PreDefinedFields {
        public const string EXT_FIELD = "`EXT`";
        public const string DOCX_EXT = ".docx";
        public const string PDF_EXT = ".pdf";

        public static readonly Dictionary<string, Func<string>> DefinedFields = new Dictionary<string, Func<string>> {
            {"`DATE`", () => $"{DateTime.Now:MM-dd-yyyy}"}
        };
    }
}

using System;

namespace TextAssetStandardizer {
    public static class EolConvert {
        
        private static string[] SplitLines(in string text) {
            string[] separater = new string[3] { "\n", "\r", "\r\n" };
            return text.Split(separater, StringSplitOptions.None);
        }
        
        public static string ToLf(in string text, bool insertFinalNewline) {
            var lines = SplitLines(text);

            return $"{string.Join("\n", lines)}"
                + (insertFinalNewline ? "\n" : "");
        }

        public static string ToCr(in string text, bool insertFinalNewline) {
            var lines = SplitLines(text);

            return $"{string.Join("\r", lines)}"
                + (insertFinalNewline ? "\r" : "");
        }

        public static string ToCrlf(in string text, bool insertFinalNewline) {
            var lines = SplitLines(text);

            return $"{string.Join("\r\n", lines)}"
                + (insertFinalNewline? "\r\n" : "");
        }
    }
}

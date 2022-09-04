#if UNITY_EDITOR
using System.IO;
using System.Text;


namespace TextAssetStandardizer {
    public class EncodingUtility {
        public static void Convert(string filePath, CodePage from, CodePage to) {
            // TODO: 変換処理を書く
        }

        public static void Convert(string filePath, CodePage to) {
            // TODO: 変換処理を書く
        }



        public static bool EstimateEncoding(string filePath, out System.Text.Encoding result) {
            byte[] bytes = File.ReadAllBytes(filePath);

            // ----------
            // Unicode 
            // ---------
            if (bytes[0] == 0x00 && bytes[1] == 0x00 && bytes[2] == 0xFE && bytes[3] == 0xFF) { // UTF-32 BEのBOMを検出
                result = new UTF32Encoding(true, true);
                return true;
            }
            else if (bytes[0] == 0xFF && bytes[1] == 0xFE && bytes[2] == 0x00 && bytes[3] == 0x00) {    // UTF-32 LEのBOMを検出
                result = new UTF32Encoding(false, true);
                return true;
            }

            if (bytes[0] == 0xFE && bytes[1] == 0xFF) { // UTF-16 BEのBOMを検出
                result = new UnicodeEncoding(true, true);
                return true;
            }
            else if (bytes[0] == 0xFF && bytes[1] == 0xFE) {   // UTF-16 LEのBOMを検出
                result = new UnicodeEncoding(false, true);
                return true;
            }

            if (bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF) { // UTF-8のBOMを検出
                result = new UTF8Encoding(true, false);
                return true;
            }
           // TODO: UTF-8の判別処理を書く

            // ----------
            // JIS X 0208
            // ----------
            // TODO: Shift-JIS, EUC-JP, ISO-2022-JPの判別処理を書く
            //for(int i = 0; i < bytes.LongLength; i++) {
            //    if()
            //}
            result = null;
            return false;
        }



        public static Encoding GetEncoding(CodePage codePage) {
            return Encoding.GetEncoding(
                (int)codePage,
                new EncoderReplacementFallback("?"),
                new DecoderReplacementFallback("?"));
        }
    }
}
#endif 

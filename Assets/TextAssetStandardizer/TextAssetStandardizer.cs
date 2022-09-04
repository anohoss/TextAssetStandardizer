#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;

namespace TextAssetStandardizer {
    public sealed class TextAssetStandardizer: AssetPostprocessor {
        private readonly List<string> _failedAssetPaths;



        private readonly TextAssetStandardizerSettings _settings;



        public TextAssetStandardizer() : this(TextAssetStandardizerSettingsDefaultObject.Settings) {

        }



        public TextAssetStandardizer(TextAssetStandardizerSettings settings) {
            _settings = settings;
            _failedAssetPaths = new List<string>();
        }



        #region unity functions
        private static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths) {

            TextAssetStandardizer standardizer = new TextAssetStandardizer();
            for(int i = 0; i < importedAssets.Length; i++) {
                string assetPath = importedAssets[i];
                standardizer.Standardize(assetPath);
            }
            standardizer.LogFailedAssets();
        }
        #endregion



        public void LogFailedAssets() {
            if(_failedAssetPaths.Count == 0) {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Standardize Failure:");

            for (int i = 0; i < _failedAssetPaths.Count; i++) {
                sb.AppendLine($"\t{_failedAssetPaths[i]}");
            }

            UnityEngine.Debug.Log(sb.ToString());
        }



        public void Standardize(in string assetPath) {
            string extension = Path.GetExtension(assetPath);

            bool keepEncoding = Array.Exists(
                _settings.EncodingTargetExtensions.Split(new char[';'], StringSplitOptions.RemoveEmptyEntries),
                (value) => $".{value}" == extension);

            bool convertEol = Array.Exists(
                _settings.NewlineTargetExtensions.Split(new char[';'], StringSplitOptions.RemoveEmptyEntries),
                (value) => $".{value}" == extension);

            if (!convertEol && !keepEncoding) {
                return;
            }

            Standardize(assetPath, convertEol, keepEncoding);
        }
        
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="keepEncoding"><see cref="EstimateEncoding(string)"/>で判別できない文字コードが使用されている場合、UTF-8に変換される</param>
        /// <exception cref="NotImplementedException"></exception>
        private void Standardize(
            in string filePath,
            in bool convertEol,
            in bool keepEncoding) {

            // file
            if (!EncodingUtility.EstimateEncoding(filePath, out Encoding encoding)) {
                _failedAssetPaths.Add(filePath);
            }

            if (convertEol) {
                FileEolConvert(filePath, encoding);
            }

            if (keepEncoding) {
                FileEncodingConvert(
                    filePath,
                    encoding,
                    _settings.GetEncoding());
            }
        }



        private void FileEolConvert(
            in string filePath,
            in Encoding encoding) {

            string text = File.ReadAllText(filePath, encoding);

            string result = _settings.Newline switch {
                Newline.LF => EolConvert.ToLf(text, _settings.InsertFinalNewline),
                Newline.CR => EolConvert.ToCr(text, _settings.InsertFinalNewline),
                Newline.CRLF => EolConvert.ToCrlf(text, _settings.InsertFinalNewline),
                _ => throw new NotImplementedException(),
            };

            File.WriteAllText(
                filePath,
                result,
                encoding);
        }



        private void FileEncodingConvert(
            in string filePath, 
            in Encoding from,
            in Encoding to) {

            string text = File.ReadAllText(filePath, from);
            File.WriteAllText(filePath, text, to);
        }
    }
}
#endif

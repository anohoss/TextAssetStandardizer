#if UNITY_EDITOR
using System;
using UnityEngine;

namespace TextAssetStandardizer {
    public class TextAssetStandardizerSettings: ScriptableObject {
        [SerializeField]
        private Newline _newline = Newline.LF;



        public Newline Newline {
            get => _newline;
            set {
                if (!Enum.IsDefined(typeof(Newline), value)) {
                    return;
                }

                _newline = value;
            }
        }



        [SerializeField]
        private string _newlineTargetExtensions = string.Empty;



        public string NewlineTargetExtensions {
            get => _newlineTargetExtensions;
            set {
                _newlineTargetExtensions = value != null ? value : string.Empty;
            }
        }



        [SerializeField]
        private bool _insertFinalNewline = true;



        public bool InsertFinalNewline {
            get => _insertFinalNewline;
            set => _insertFinalNewline = value;
        }

        [field: SerializeField]
        private CodePage _codePage;



        public CodePage CodePage {
            get => _codePage;
            set {
                if (!Enum.IsDefined(typeof(CodePage), value)) {
                    return;
                }

                _codePage = value;
            }
        }



        [field: SerializeField]
        public string _encodingTargetExtensions;



        public string EncodingTargetExtensions {
            get => _encodingTargetExtensions;
            set {
                _encodingTargetExtensions = value != null ? value : string.Empty;
            }
        }



        public System.Text.Encoding GetEncoding() {
            return EncodingUtility.GetEncoding(CodePage);
        }
    }
}
#endif

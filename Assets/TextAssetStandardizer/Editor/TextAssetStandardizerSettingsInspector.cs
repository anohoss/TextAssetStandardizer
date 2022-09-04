using UnityEditor;
using UnityEngine;

namespace TextAssetStandardizer {
    [CustomEditor(typeof(TextAssetStandardizerSettings))]
    public class TextAssetStandardizerSettingsInspector: Editor {
        private TextAssetStandardizerSettings _settings;

        private GUIStyle _header;

        public override void OnInspectorGUI() {
            EditorGUILayout.LabelField("Newline", _header);

            _settings.Newline = (Newline) EditorGUILayout.EnumPopup(
                new GUIContent("Code"), 
                _settings.Newline);

            _settings.NewlineTargetExtensions = EditorGUILayout.DelayedTextField(
                new GUIContent("Target Extensions"), 
                _settings.NewlineTargetExtensions);

            _settings.InsertFinalNewline = EditorGUILayout.Toggle(
                new GUIContent("Insert Final Newline"), 
                _settings.InsertFinalNewline);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Encoding", _header);

            _settings.CodePage = (CodePage) EditorGUILayout.EnumPopup(
                new GUIContent("Code Page"),
                _settings.CodePage);

            _settings.EncodingTargetExtensions = EditorGUILayout.DelayedTextField(
                new GUIContent("Target Extensions"),
                _settings.EncodingTargetExtensions);
        }

        private void OnEnable() {
            _settings = (TextAssetStandardizerSettings) target;

            _header = new GUIStyle();
            _header.normal.textColor = new Color(0.8f, 0.8f, 0.8f);
            _header.fontStyle = FontStyle.Bold;
        }
    }
}
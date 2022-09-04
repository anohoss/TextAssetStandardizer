using TextAssetStandardizer;
using UnityEditor;
using UnityEngine;

public static class TextAssetStandardizerSettingsDefaultObject 
    {

    private static TextAssetStandardizerSettings s_cachedSettings;



    public static TextAssetStandardizerSettings Settings {
        get {
            if (s_cachedSettings == null) {
                const string CONFIG_OBJECT_NAME = "com.anoho.text-asset-standardizer";
                

                if (!EditorBuildSettings.TryGetConfigObject<TextAssetStandardizerSettings>(CONFIG_OBJECT_NAME, out s_cachedSettings)) {
                    const string SETTINGS_ASSET_PATH = "Assets/TextAssetStandardizer/TextAssetStandardizerSettings.asset";

                    s_cachedSettings = ScriptableObject.CreateInstance<TextAssetStandardizerSettings>();
                    AssetDatabase.CreateAsset(s_cachedSettings, SETTINGS_ASSET_PATH);
                    EditorUtility.SetDirty(s_cachedSettings);
                    AssetDatabase.SaveAssets();

                    EditorBuildSettings.AddConfigObject(CONFIG_OBJECT_NAME, s_cachedSettings, true);
                }
            }

            return s_cachedSettings;
        }
    }
}

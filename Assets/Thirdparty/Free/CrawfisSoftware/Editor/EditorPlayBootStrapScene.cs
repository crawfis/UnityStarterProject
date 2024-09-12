using UnityEditor;
using UnityEditor.SceneManagement;

using UnityEngine.SceneManagement;

namespace CrawfisSoftware.EditorUtil
{
    /// <summary>
    /// This script overrides the default behavior when pressing Play to mimic it as if you loaded the first scene in the Build index and then hit Play.
    /// It is useful when you have a "bootstrap" scene or need to always load the Main Menu first.
    /// </summary>
    public static class EditorPlayBootStrapScene
    {
        private const string BOOTSTRAP_MENU_ITEM = "Crawfis/EditorTools/Use Bootstrap";
        private const string PLUGIN_LOG_NAME = "Editor Play Bootstrap Scene";
        private const string IS_ENABLED_KEY = "use_bootstrap:is_enabled";
        private static bool _isEnabled;

        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            if (!_isEnabled) return;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

            // Ensure at least one build scene exist.
            if (EditorBuildSettings.scenes.Length == 0)
                return;

            // SetProperties Play Mode scene to first scene defined in build settings.
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
        }
        #region Constructor
        static EditorPlayBootStrapScene()
        {
            // delay the call to setup, otherwise the menu items initial state wont be set
            EditorApplication.delayCall += OnSetup;
        }
        #endregion

        /// <summary>
        /// Unity menu item used for enabling or disabling the tool.
        /// </summary>
        [MenuItem(BOOTSTRAP_MENU_ITEM)]
        private static void Toggle()
        {
            _isEnabled = !_isEnabled;

            // save the current setting
            EditorPrefs.SetBool(IS_ENABLED_KEY, _isEnabled);

            // shows whether the tool is enabled or disabled
            Menu.SetChecked(BOOTSTRAP_MENU_ITEM, _isEnabled);

            // Set the start scene
            if (_isEnabled)
            {
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
            }
            else
            {
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(SceneManager.GetActiveScene().path);
            }
        }

        private static void OnSetup()
        {
            EditorApplication.delayCall -= OnSetup;

            // set the initial state of the tool
            _isEnabled = EditorPrefs.GetBool(IS_ENABLED_KEY, false);

            // shows whether the tool is enabled or disabled
            Menu.SetChecked(BOOTSTRAP_MENU_ITEM, _isEnabled);
        }
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:
                    Scene openInEditor = SceneManager.GetActiveScene();
                    if (!_isEnabled || openInEditor.buildIndex == 0)
                    {
                        return;
                    }
                    // Save off the current scene so it will be reloaded after the Play session is over.
                    EditorPrefs.SetString("DefaultScene", openInEditor.name);
                    /// Debug.Log("SetProperties DefaultScene pref to " + openInEditor.name);
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    EditorPrefs.SetString("DefaultScene", "");
                    /// Debug.Log("SetProperties DefaultScene pref to \"\"");
                    break;
            }
        }
    }
}
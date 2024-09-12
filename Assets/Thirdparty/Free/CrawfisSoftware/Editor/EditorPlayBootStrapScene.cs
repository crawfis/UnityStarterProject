using UnityEditor;
using UnityEditor.SceneManagement;

using UnityEngine.SceneManagement;

namespace CrawfisSoftware.EditorUtil
{
    /// <summary>
    /// This script overrides the default behavior when pressing Play to mimic it as if you loaded the first scene in the Build index and then hit Play.
    /// It is useful when you have a "bootstrap" scene or need to always load the Main Menu first. It can be toggled on or off with a menu setting.
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
            EditorSceneManager.activeSceneChangedInEditMode += SetStartScene;
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

            SetStartScene();
        }

        private static void OnSetup()
        {
            EditorApplication.delayCall -= OnSetup;

            // set the initial state of the tool
            _isEnabled = EditorPrefs.GetBool(IS_ENABLED_KEY, false);

            // shows whether the tool is enabled or disabled
            Menu.SetChecked(BOOTSTRAP_MENU_ITEM, _isEnabled);
            SetStartScene();
        }

        private static void SetStartScene()
        {
            Scene openInEditor = SceneManager.GetActiveScene();
            if (!_isEnabled || EditorBuildSettings.scenes.Length == 0 || openInEditor.buildIndex == 0)
            {
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(openInEditor.path);
            }
            else
            {
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
            }
        }
        private static void SetStartScene(Scene oldScene, Scene newScene)
        {
            Scene openInEditor = SceneManager.GetActiveScene();
            if (!_isEnabled || EditorBuildSettings.scenes.Length == 0 || openInEditor.buildIndex == 0)
            {
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(openInEditor.path);
            }
            else
            {
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[0].path);
            }
        }
    }
}
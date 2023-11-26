using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VeUnityBuild.Editor.Contexts;
using VeUnityBuild.Editor.Domains;
using VeUnityBuild.Editor.UseCases;

namespace VeUnityBuild.Editor.Presentations
{
    public class AndroidWindow : EditorWindow
    {
        private string _buildMode;

        [MenuItem("Tools/VeUnityBuild/Build/Android")]
        public static void ShowExample()
        {
            var wnd = GetWindow<AndroidWindow>();
            wnd.titleContent = new GUIContent("Android Build Window");
        }
        
        [MenuItem("Tools/VeUnityBuild/CreateBuildConfig/Android")]
        public static void Create()
        {
            var dir = Path.GetDirectoryName(Constant.AndroidBuildConfigPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var asset = CreateInstance<AndroidBuildConfig>();
            AssetDatabase.CreateAsset(asset, Constant.AndroidBuildConfigPath);
            AssetDatabase.Refresh();
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            var root = rootVisualElement;

            var dropdown = new DropdownField
            {
                label = "Build Mode",
                choices =
                {
                    Constant.BuildModeDebug,
                    Constant.BuildModeRelease
                },
                tooltip = "Build Mode"
            };

            root.Add(dropdown);

            dropdown.RegisterValueChangedCallback(evt =>
            {
                _buildMode = evt.newValue;
                Debug.Log($"BuildMode: {_buildMode}");
            });

            var buildButton = new Button
            {
                text = "Build",
                clickable = new Clickable(() =>
                {
                    Debug.Log("Start Android Building in Editor.");

                    var parameterContext = new ParameterContext
                    {
                        BuildMode = _buildMode
                    };

                    var returnCode = BuildAndroidUseCase.Build(parameterContext);
                    Debug.Log($"Finish Android Building in Editor. ReturnCode: {returnCode}");
                }),
                tooltip = "Execute Android Build"
            };

            root.Add(buildButton);
        }
    }
}
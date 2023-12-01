using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VeUnityBuild.Editor.Domains;
using VeUnityBuild.Editor.UseCases;

namespace VeUnityBuild.Editor.Presentations
{
    public class IOSWindow : EditorWindow
    {
        string _buildMode;

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            var root = rootVisualElement;

            var dropdown = new DropdownField
            {
                label = "Build Mode",
                choices = { Constant.BuildModeDebug, Constant.BuildModeRelease },
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
                    Debug.Log("Start iOS Building in Editor.");

                    var parameterContext = new BuildParameter { BuildMode = _buildMode };

                    var returnCode = BuildIOSUseCase.Build(parameterContext);
                    Debug.Log($"Finish iOS Building in Editor. ReturnCode: {returnCode}");
                }),
                tooltip = "Execute iOS Build"
            };

            root.Add(buildButton);
        }

        [MenuItem("Window/VeUnityBuild/Build/iOS")]
        public static void ShowExample()
        {
            var wnd = GetWindow<IOSWindow>();
            wnd.titleContent = new GUIContent("iOS Build Window");
        }
        
        [MenuItem("Window/VeUnityBuild/CreateBuildConfig/iOS")]
        public static void Create()
        {
            var folderPath = BrowseUseCase.Browse();
            if (string.IsNullOrEmpty(folderPath))
            {
                return;
            }

            var buildConfigPath = $"{folderPath}/{Constant.IOSBuildConfigPath}";
            var dirPath = Path.GetDirectoryName(buildConfigPath);
            if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            var buildConfigAsset = CreateInstance<IOSBuildConfig>();
            AssetDatabase.CreateAsset(buildConfigAsset, buildConfigPath);
            AssetDatabase.Refresh();
            
            Debug.Log($"Create iOS Build Config. Path: {buildConfigPath}");
        }
    }
}

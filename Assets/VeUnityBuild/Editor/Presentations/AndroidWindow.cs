using System.IO;
using UnityEditor;
// using UnityEditor.Search;
// using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VeUnityBuild.Editor.Domains;
using VeUnityBuild.Editor.UseCases;

namespace VeUnityBuild.Editor.Presentations
{
    public class AndroidWindow : EditorWindow
    {
        private string _buildMode;
        private TextField _buildConfigPathTextField;
        private AndroidBuildConfig _buildConfig;

        [MenuItem("Window/VeUnityBuild/Build/Android")]
        public static void Build()
        {
            var wnd = GetWindow<AndroidWindow>();
            wnd.titleContent = new GUIContent("Android Build Window");
        }

        [MenuItem("Window/VeUnityBuild/CreateBuildConfig/Android")]
        public static void Create()
        {
            var folderPath = Browse();
            if (string.IsNullOrEmpty(folderPath))
            {
                return;
            }

            var buildConfigPath = $"{folderPath}/{Constant.AndroidBuildConfigPath}";
            var dirPath = Path.GetDirectoryName(buildConfigPath);
            if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            var buildConfigAsset = CreateInstance<AndroidBuildConfig>();
            AssetDatabase.CreateAsset(buildConfigAsset, buildConfigPath);
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

            var objectField = new UnityEditor.UIElements.ObjectField("Android Build Config")
            {
                objectType = typeof(AndroidBuildConfig)
            };
            
            objectField.RegisterValueChangedCallback(evt =>
            {
                _buildConfig = evt.newValue as AndroidBuildConfig;
            });

            root.Add(objectField);

            var buildButton = new Button
            {
                text = "Build",
                clickable = new Clickable(() =>
                {
                    Debug.Log("Start Android Building in Editor.");

                    var parameterContext = new BuildParameter
                    {
                        BuildMode = _buildMode
                    };

                    var returnCode = BuildAndroidUseCase.Build(parameterContext, _buildConfig);
                    Debug.Log($"Finish Android Building in Editor. ReturnCode: {returnCode}");
                }),
                tooltip = "Execute Android Build"
            };

            root.Add(buildButton);
        }

        private static string Browse()
        {
            var selectedPath = EditorUtility.OpenFolderPanel("Select Folder", "", "");
            if (string.IsNullOrEmpty(selectedPath))
            {
                return string.Empty;
            }

            var dataPath = Application.dataPath;
            if (selectedPath.StartsWith(dataPath))
            {
                return $"Assets{selectedPath[dataPath.Length..]}";
            }

            Debug.LogError("The selected folder is not part of the Assets folder.");
            return string.Empty;
        }
    }
}
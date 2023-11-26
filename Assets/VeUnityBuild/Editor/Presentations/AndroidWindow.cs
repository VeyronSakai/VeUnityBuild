using System.IO;
using UnityEditor;
using UnityEditor.Search;
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

        [MenuItem("Window/VeUnityBuild/Build/Android")]
        public static void Build()
        {
            var wnd = GetWindow<AndroidWindow>();
            wnd.titleContent = new GUIContent("Android Build Window");
        }

        [MenuItem("Window/VeUnityBuild/CreateBuildConfig/Android")]
        public static void Create()
        {
            var path = Browse();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            var buildConfigPath = $"{path}/{Constant.AndroidBuildConfigPath}";
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

            _buildConfigPathTextField = new TextField("Android Build Config Path");
            root.Add(_buildConfigPathTextField);

            var f = new ObjectField("Select Android Build Config")
            {
                objectType = typeof(AndroidBuildConfig)
            };
            // ObjectFieldが変更されたときのイベントリスナーを追加
            f.RegisterValueChangedCallback(evt =>
            {
                var selectedObject = evt.newValue as AndroidBuildConfig;
                // ここで選択されたオブジェクトに対する処理を行う
            });

            root.Add(f);

            // var browseButton = new Button(BrowseFolder) { text = "Browse" };
            // root.Add(browseButton);
            //
            // var createButton = new Button(CreateDirectory) { text = "Create Directory" };
            // root.Add(createButton);

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

                    var returnCode = BuildAndroidUseCase.Build(parameterContext);
                    Debug.Log($"Finish Android Building in Editor. ReturnCode: {returnCode}");
                }),
                tooltip = "Execute Android Build"
            };

            root.Add(buildButton);
        }

        private static string Browse()
        {
            var absolutePath = EditorUtility.OpenFolderPanel("Select Folder", "", "");
            if(string.IsNullOrEmpty(absolutePath))
            {
                return string.Empty;
            }
            
            var dataPath = Application.dataPath;
            if (absolutePath.StartsWith(dataPath))
            {
                return $"Assets{absolutePath[dataPath.Length..]}";
            }

            Debug.LogError("The selected folder is not part of the Assets folder.");
            return string.Empty;
        }
    }
}
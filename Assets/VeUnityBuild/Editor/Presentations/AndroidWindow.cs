using UnityEditor;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VeUnityBuild.Editor.Domains;
using VeUnityBuild.Editor.Infrastructures;
using VeUnityBuild.Editor.UseCases;

namespace VeUnityBuild.Editor.Presentations
{
    public class AndroidWindow : EditorWindow
    {
        AndroidBuildConfig _buildConfig;
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

            var objectField = new ObjectField("Android Build Config")
            {
                objectType = typeof(AndroidBuildConfig), allowSceneObjects = false
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

                    var parameterContext = new BuildParameter { BuildMode = _buildMode };

                    var returnCode = BuildAndroidUseCase.Build(new IContextObject[] { parameterContext, _buildConfig });
                    Debug.Log($"Finish Android Building in Editor. ReturnCode: {returnCode}");
                }),
                tooltip = "Execute Android Build"
            };

            root.Add(buildButton);
        }

        [MenuItem("Window/VeUnityBuild/Build/Android")]
        public static void Build()
        {
            var wnd = GetWindow<AndroidWindow>();
            wnd.titleContent = new GUIContent("Android Build Window");
        }

        [MenuItem("Window/VeUnityBuild/CreateBuildConfig/Android")]
        public static void Create()
        {
            var outputDirPath = BrowseUseCase.Browse();
            if (string.IsNullOrEmpty(outputDirPath))
            {
                return;
            }

            BuildConfigRepository.Save<AndroidBuildConfig>(outputDirPath, Constant.AndroidBuildConfigName);
        }
    }
}

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
    public class IOSWindow : EditorWindow
    {
        IOSBuildConfig _buildConfig;
        string _buildMode;
        
#if UNITY_IOS
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

            var objectField = new ObjectField("iOS Build Config")
            {
                objectType = typeof(IOSBuildConfig), allowSceneObjects = false
            };
            objectField.RegisterValueChangedCallback(evt =>
            {
                _buildConfig = evt.newValue as IOSBuildConfig;
            });

            root.Add(objectField);

            var buildButton = new Button
            {
                text = "Build",
                clickable = new Clickable(() =>
                {
                    Debug.Log("Start iOS Building in Editor.");
                    var parameterContext = new BuildParameter { BuildMode = _buildMode };
                    var returnCode = BuildIOSUseCase.Build(new IContextObject[] { parameterContext, _buildConfig });
                    Debug.Log($"Finish iOS Building in Editor. ReturnCode: {returnCode}");
                }),
                tooltip = "Execute iOS Build"
            };

            root.Add(buildButton);
        }

        [MenuItem("Window/VeUnityBuild/Build/iOS")]
        public static void ShowBuildWindow()
        {
            var wnd = GetWindow<IOSWindow>();
            wnd.titleContent = new GUIContent("iOS Build Window");
        }
#endif

        [MenuItem("Window/VeUnityBuild/CreateBuildConfig/iOS")]
        public static void Create()
        {
            var outputDirPath = BrowseUseCase.Browse();
            if (string.IsNullOrEmpty(outputDirPath))
            {
                return;
            }

            BuildConfigRepository.SaveBuildConfig<IOSBuildConfig>(outputDirPath, Constant.IOSBuildConfigName);
        }
    }
}

using System.Linq;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using VeUnityBuild.Editor.Domains;
using VeUnityBuild.Editor.Infrastructures;
using VeUnityBuild.Editor.UseCases;

namespace VeUnityBuild.Editor.Presentations
{
    public static class BatchEntryPoint
    {
        public static void Build()
        {
            var activeTarget = EditorUserBuildSettings.activeBuildTarget;
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (activeTarget)
            {
                case BuildTarget.Android:
                    BuildAndroid();
                    break;
                case BuildTarget.iOS:
                    BuildIOS();
                    break;
                default:
                    Debug.LogError($"Unsupported BuildTarget: {activeTarget}");
                    EditorApplication.Exit(1);
                    break;
            }
        }

        static void BuildAndroid()
        {
            Debug.Log("Start Android Building in batch mode.");

            var contextObjects = ContextObjectRepository.GetContextObjectsFromCommandLine<AndroidBuildConfig>();
            var returnCode = BuildAndroidUseCase.Build(contextObjects);
            var isSuccess =
                new[] { ReturnCode.Success, ReturnCode.SuccessCached, ReturnCode.SuccessNotRun }.Contains(returnCode);

            Debug.Log($"Finish Android Building in batch mode. ReturnCode: {returnCode}");
            EditorApplication.Exit(isSuccess ? 0 : 1);
        }

        static void BuildIOS()
        {
            Debug.Log("Start iOS Building in batch mode.");

            var contextObjects = ContextObjectRepository.GetContextObjectsFromCommandLine<IOSBuildConfig>();
            var returnCode = BuildIOSUseCase.Build(contextObjects);
            var isSuccess =
                new[] { ReturnCode.Success, ReturnCode.SuccessCached, ReturnCode.SuccessNotRun }.Contains(returnCode);

            Debug.Log($"Finish iOS Building in batch mode. ReturnCode: {returnCode}");
            EditorApplication.Exit(isSuccess ? 0 : 1);
        }
    }
}

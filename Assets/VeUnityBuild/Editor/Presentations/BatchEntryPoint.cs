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
#if UNITY_IOS
            BuildIOS();
#elif UNITY_ANDROID
            BuildAndroid();
#else
            Debug.LogError($"Unsupported BuildTarget: {EditorUserBuildSettings.activeBuildTarget}");
            EditorApplication.Exit(1);
#endif
        }

#if UNITY_ANDROID
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
#endif

#if UNITY_IOS
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
#endif
    }
}

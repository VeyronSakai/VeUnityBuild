using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using VeUnityBuild.Editor.Domains;
using VeUnityBuild.Editor.UseCases;

namespace VeUnityBuild.Editor.Presentations
{
    public static class BatchEntryPoint
    {
        public static void BuildAndroid()
        {
            Debug.Log("Start Android Building in batch mode.");
            var getCommandLineArgsUseCase = new GetCommandLineArgsUseCase();
            var buildMode = getCommandLineArgsUseCase.GetValue("-buildMode");
            if (string.IsNullOrEmpty(buildMode))
            {
                throw new BuildFailedException("BuildMode is not specified.");
            }

            var parameterContext = new BuildParameter { BuildMode = buildMode };

            var buildConfigPath = getCommandLineArgsUseCase.GetValue("-buildConfig");
            if (string.IsNullOrEmpty(buildConfigPath))
            {
                throw new BuildFailedException($"BuildConfig is not found. Path: {buildConfigPath}");
            }

            var buildConfig = AssetDatabase.LoadAssetAtPath<AndroidBuildConfig>(buildConfigPath);
            if (buildConfig == null)
            {
                throw new BuildFailedException($"BuildConfig is not found. Path: {buildConfigPath}");
            }

            var returnCode = BuildAndroidUseCase.Build(parameterContext, buildConfig);
            var isSuccess = new[] { ReturnCode.Success, ReturnCode.SuccessCached, ReturnCode.SuccessNotRun }.Contains(returnCode);

            Debug.Log($"Finish Android Building in batch mode. ReturnCode: {returnCode}");
            EditorApplication.Exit(isSuccess ? 0 : 1);
        }

        public static void BuildIOS()
        {
            Debug.Log("Start iOS Building in batch mode.");

            var args = new GetCommandLineArgsUseCase();
            var buildMode = args.GetValue("-buildMode");
            var parameterContext = new BuildParameter { BuildMode = buildMode };

            var returnCode = BuildIOSUseCase.Build(parameterContext);
            var isSuccess =
                new[] { ReturnCode.Success, ReturnCode.SuccessCached, ReturnCode.SuccessNotRun }.Contains(returnCode);

            Debug.Log($"Finish iOS Building in batch mode. ReturnCode: {returnCode}");
            EditorApplication.Exit(isSuccess ? 0 : 1);
        }
    }
}

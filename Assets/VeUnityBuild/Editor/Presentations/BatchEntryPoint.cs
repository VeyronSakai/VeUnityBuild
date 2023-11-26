using System.Linq;
using UnityEditor;
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
            var args = new CommandLineArgs();
            var buildMode = args.GetValue("-buildMode");
            var parameterContext = new ParameterContext
            {
                BuildMode = buildMode
            };

            var returnCode = BuildAndroidUseCase.Build(parameterContext);
            var isSuccess = new[]
            {
                ReturnCode.Success, ReturnCode.SuccessCached, ReturnCode.SuccessNotRun
            }.Contains(returnCode);

            Debug.Log($"Finish Android Building in batch mode. ReturnCode: {returnCode}");
            EditorApplication.Exit(isSuccess ? 0 : 1);
        }

        public static void BuildIOS()
        {
            Debug.Log("Start iOS Building in batch mode.");

            var args = new CommandLineArgs();
            var buildMode = args.GetValue("-buildMode");
            var parameterContext = new ParameterContext
            {
                BuildMode = buildMode
            };

            var returnCode = BuildIOSUseCase.BuildIOS(parameterContext);
            var isSuccess = new[]
            {
                ReturnCode.Success, ReturnCode.SuccessCached, ReturnCode.SuccessNotRun
            }.Contains(returnCode);

            Debug.Log($"Finish iOS Building in batch mode. ReturnCode: {returnCode}");
            EditorApplication.Exit(isSuccess ? 0 : 1);
        }
    }
}
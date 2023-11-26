using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;
using VeUnityBuild.Editor.Contexts;
using VeUnityBuild.Editor.Tasks;

namespace VeUnityBuild.Editor
{
    public static class EntryPoint
    {
        public static void BatchAndroidBuildMain()
        {
            Debug.Log("Start Android Building in batch mode.");
            var args = new CommandLineArgs();
            var buildMode = args.GetValue("-buildMode");
            var parameterContext = new ParameterContext
            {
                BuildMode = buildMode
            };

            var returnCode = BuildAndroid(parameterContext);
            var isSuccess = new[]
            {
                ReturnCode.Success, ReturnCode.SuccessCached, ReturnCode.SuccessNotRun
            }.Contains(returnCode);

            Debug.Log($"Finish Android Building in batch mode. ReturnCode: {returnCode}");
            EditorApplication.Exit(isSuccess ? 0 : 1);
        }

        public static void BatchIOSBuildMain()
        {
            Debug.Log("Start iOS Building in batch mode.");

            var args = new CommandLineArgs();
            var buildMode = args.GetValue("-buildMode");
            var parameterContext = new ParameterContext
            {
                BuildMode = buildMode
            };

            var returnCode = BuildIOS(parameterContext);
            var isSuccess = new[]
            {
                ReturnCode.Success, ReturnCode.SuccessCached, ReturnCode.SuccessNotRun
            }.Contains(returnCode);

            Debug.Log($"Finish iOS Building in batch mode. ReturnCode: {returnCode}");
            EditorApplication.Exit(isSuccess ? 0 : 1);
        }

        public static ReturnCode BuildAndroid(IContextObject parameterContext)
        {
            var tasks = new List<IBuildTask>
            {
                new AndroidBuildTask(),
            };

            var config =
                AssetDatabase.LoadAssetAtPath<AndroidBuildConfig>(Constant.AndroidBuildConfigPath);

            var contexts = new BuildContext();
            contexts.SetContextObject(config);
            contexts.SetContextObject(parameterContext);

            return BuildTasksRunner.Run(tasks, contexts);
        }

        public static ReturnCode BuildIOS(IContextObject parameterContext)
        {
            var tasks = new List<IBuildTask>
            {
                new IOSBuildTask(),
                new XcodeSettingsTask()
            };

            var config =
                AssetDatabase.LoadAssetAtPath<IOSBuildConfig>(Constant.IOSBuildConfigPath);

            var contexts = new BuildContext();
            contexts.SetContextObject(config);
            contexts.SetContextObject(parameterContext);

            var returnCode = BuildTasksRunner.Run(tasks, contexts);
            return returnCode;
        }
    }
}
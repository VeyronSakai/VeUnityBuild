using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using VeUnityBuild.Editor.Domains;
using VeUnityBuild.Editor.Domains.Tasks;
using VeUnityBuild.Editor.Presentations;

namespace VeUnityBuild.Editor.UseCases
{
    public sealed class BuildAndroidUseCase
    {
        public static ReturnCode Build(IContextObject parameterContext)
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
    }
}
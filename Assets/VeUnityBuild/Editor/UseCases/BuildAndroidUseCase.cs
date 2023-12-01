using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using VeUnityBuild.Editor.Domains;
using VeUnityBuild.Editor.Domains.Tasks;

namespace VeUnityBuild.Editor.UseCases
{
    public sealed class BuildAndroidUseCase
    {
        public static ReturnCode Build(IContextObject parameterContext, AndroidBuildConfig androidBuildConfig)
        {
            var tasks = new List<IBuildTask> { new AndroidBuildTask() };

            var contexts = new BuildContext();
            contexts.SetContextObject(androidBuildConfig);
            contexts.SetContextObject(parameterContext);

            return BuildTasksRunner.Run(tasks, contexts);
        }
    }
}

using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;

namespace VeUnityBuild.Editor.Android
{
    public sealed class BuildAndroidUseCase
    {
        public static ReturnCode Build(IContextObject[] contextObjects)
        {
            var tasks = new List<IBuildTask> { new AndroidBuildTask() };
            var contexts = new BuildContext(contextObjects);
            return BuildTasksRunner.Run(tasks, contexts);
        }
    }
}

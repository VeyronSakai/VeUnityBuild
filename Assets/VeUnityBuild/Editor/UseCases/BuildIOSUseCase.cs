using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using VeUnityBuild.Editor.Domains.Tasks;

namespace VeUnityBuild.Editor.UseCases
{
    public static class BuildIOSUseCase
    {
        public static ReturnCode Build(IContextObject[] contextObjects)
        {
            var tasks = new List<IBuildTask> { new IOSBuildTask(), new XcodeProjectSetTask() };
            var contexts = new BuildContext(contextObjects);
            var returnCode = BuildTasksRunner.Run(tasks, contexts);
            return returnCode;
        }
    }
}

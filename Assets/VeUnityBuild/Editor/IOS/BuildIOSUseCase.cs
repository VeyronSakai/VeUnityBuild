using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;

namespace VeUnityBuild.Editor.IOS
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

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using VeUnityBuild.Editor.Domains;
using VeUnityBuild.Editor.Domains.Tasks;

namespace VeUnityBuild.Editor.UseCases
{
    public static class BuildIOSUseCase
    {
        public static ReturnCode Build(IContextObject parameterContext, IOSBuildConfig buildConfig)
        {
            var tasks = new List<IBuildTask> { new IOSBuildTask(), new XcodeProjectSetTask() };
            var contexts = new BuildContext();
            contexts.SetContextObject(buildConfig);
            contexts.SetContextObject(parameterContext);

            var returnCode = BuildTasksRunner.Run(tasks, contexts);
            return returnCode;
        }
    }
}

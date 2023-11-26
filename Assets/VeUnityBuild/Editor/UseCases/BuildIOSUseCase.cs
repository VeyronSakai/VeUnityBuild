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
        public static ReturnCode BuildIOS(IContextObject parameterContext)
        {
            var tasks = new List<IBuildTask>
            {
                new IOSBuildTask(),
                new XcodeProjectSetTask()
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
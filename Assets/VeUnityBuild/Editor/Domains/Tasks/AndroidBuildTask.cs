using System;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Injector;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;
using VeUnityBuild.Editor.Contexts;
using VeUnityBuild.Editor.Presentations;

namespace VeUnityBuild.Editor.Domains.Tasks
{
    public class AndroidBuildTask : IBuildTask
    {
        [InjectContext(ContextUsage.In)] private readonly AndroidBuildConfig _buildConfig;
        [InjectContext(ContextUsage.In)] private readonly ParameterContext _parameterContext;

        public int Version => Constant.Version;

        public ReturnCode Run()
        {
            var buildOption = _parameterContext.BuildMode switch
            {
                Constant.BuildModeDebug => _buildConfig.debugConfig.buildOptions,
                Constant.BuildModeRelease => _buildConfig.releaseConfig.buildOptions,
                _ => throw new ArgumentException($"Invalid build mode: {_parameterContext.BuildMode}")
            };

            try
            {
                BuildPipeline.BuildPlayer(
                    EditorBuildSettings.scenes,
                    $"{_buildConfig.commonConfig.outputDirPath}/{PlayerSettings.productName}.{_buildConfig.exportFormat.ToString().ToLower()}",
                    BuildTarget.Android,
                    buildOption
                );
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                throw;
            }

            return ReturnCode.Success;
        }
    }
}
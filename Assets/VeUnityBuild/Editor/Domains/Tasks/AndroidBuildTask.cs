#if UNITY_ANDROID
using System;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Injector;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;

namespace VeUnityBuild.Editor.Domains.Tasks
{
    public class AndroidBuildTask : IBuildTask
    {
        [InjectContext(ContextUsage.In)] readonly AndroidBuildConfig _buildConfig;
        [InjectContext(ContextUsage.In)] readonly BuildParameter _buildParameter;

        public int Version => Constant.Version;

        public ReturnCode Run()
        {
            var buildOption = _buildParameter.BuildMode switch
            {
                Constant.BuildModeDebug => _buildConfig.debugConfig.buildOptions,
                Constant.BuildModeRelease => _buildConfig.releaseConfig.buildOptions,
                _ => throw new ArgumentException($"Invalid build mode: {_buildParameter.BuildMode}")
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
#endif

using System;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Injector;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;
using VeUnityBuild.Editor.Presentations;

namespace VeUnityBuild.Editor.Domains.Tasks
{
    public class IOSBuildTask : IBuildTask
    {
        [InjectContext(ContextUsage.In)] private readonly IOSBuildConfig _buildConfig;
        [InjectContext(ContextUsage.In)] private readonly ParameterContext _parameterContext;

        public int Version => Constant.Version;

        public ReturnCode Run()
        {
            PlayerSettings.iOS.appleEnableAutomaticSigning = false;
            BuildOptions buildOption;

            switch (_parameterContext.BuildMode)
            {
                case Constant.BuildModeDebug:
                    PlayerSettings.iOS.iOSManualProvisioningProfileType = ProvisioningProfileType.Development;
                    PlayerSettings.iOS.iOSManualProvisioningProfileID = _buildConfig.debugConfig.profileId;
                    buildOption = _buildConfig.debugConfig.buildOptions;
                    break;
                case Constant.BuildModeRelease:
                    PlayerSettings.iOS.iOSManualProvisioningProfileType = ProvisioningProfileType.Distribution;
                    PlayerSettings.iOS.iOSManualProvisioningProfileID = _buildConfig.releaseConfig.profileId;
                    buildOption = _buildConfig.releaseConfig.buildOptions;
                    break;
                default:
                    throw new ArgumentException($"Invalid build mode: {_parameterContext.BuildMode}");
            }

            try
            {
                BuildPipeline.BuildPlayer(
                    EditorBuildSettings.scenes,
                    $"{_buildConfig.commonConfig.outputDirPath}/{PlayerSettings.productName}",
                    BuildTarget.iOS,
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
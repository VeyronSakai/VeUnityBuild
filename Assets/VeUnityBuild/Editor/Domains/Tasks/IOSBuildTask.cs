#if UNITY_IOS
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Injector;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;

namespace VeUnityBuild.Editor.Domains.Tasks
{
    public class IOSBuildTask : IBuildTask
    {
        [InjectContext(ContextUsage.In)] readonly IOSBuildConfig _buildConfig;
        [InjectContext(ContextUsage.In)] readonly BuildParameter _buildParameter;

        public int Version => Constant.Version;

        public ReturnCode Run()
        {
            PlayerSettings.iOS.appleEnableAutomaticSigning = false;
            BuildOptions buildOption;

            switch (_buildParameter.BuildMode)
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
                    throw new ArgumentException($"Invalid build mode: {_buildParameter.BuildMode}");
            }

            // Without -buildNumber, the build number in PlayerSettings is used as is.
            if (!string.IsNullOrEmpty(_buildParameter.BuildNumber))
            {
                // Apple accepts only up to three period-separated non-negative integers (e.g. "12" or "12.1")
                // as CFBundleVersion. Fail here so that an invalid value does not surface later in Xcode or on upload.
                var parts = _buildParameter.BuildNumber.Split('.');
                if (parts.Length > 3 || parts.Any(part => !uint.TryParse(part, out _)))
                {
                    throw new ArgumentException($"Invalid build number: {_buildParameter.BuildNumber}");
                }

                PlayerSettings.iOS.buildNumber = _buildParameter.BuildNumber;
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
#endif

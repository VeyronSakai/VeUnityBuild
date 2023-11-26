using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Injector;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using VeUnityBuild.Editor.Contexts;

namespace VeUnityBuild.Editor.Tasks
{
    public class XcodeSettingsTask : IBuildTask
    {
        [InjectContext(ContextUsage.In)] private readonly IOSBuildConfig _buildConfig;

        [InjectContext(ContextUsage.In)] private readonly ParameterContext _parameterContext;

        public int Version => 1;

        public ReturnCode Run()
        {
            try
            {
                var xcodeProjPath =
                    PBXProject.GetPBXProjectPath(
                        $"{_buildConfig.commonConfig.outputDirPath}/{PlayerSettings.productName}");

                FixXcodeProjSettings(xcodeProjPath);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                throw;
            }

            return ReturnCode.Success;
        }

        private void FixXcodeProjSettings(string xcodeProjPath)
        {
            var xcodeProj = new PBXProject();
            xcodeProj.ReadFromString(File.ReadAllText(xcodeProjPath));

            var targetGuid = xcodeProj.GetUnityMainTargetGuid();
            var projectGuid = xcodeProj.ProjectGuid();

            xcodeProj.SetBuildProperty(projectGuid, "ENABLE_BITCODE", "NO");
            xcodeProj.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");

            xcodeProj.SetBuildProperty(targetGuid, "CODE_SIGN_STYLE", "Manual");
            xcodeProj.SetBuildProperty(projectGuid, "CODE_SIGN_STYLE", "Manual");

            var profileId = _parameterContext.BuildMode switch
            {
                Constant.BuildModeDebug => _buildConfig.debugConfig.profileId,
                Constant.BuildModeRelease => _buildConfig.releaseConfig.profileId,
                _ => throw new ArgumentException($"Invalid build mode: {_parameterContext.BuildMode}")
            };

            xcodeProj.SetBuildProperty(targetGuid, "PROVISIONING_PROFILE_SPECIFIER", profileId);

            xcodeProj.SetBuildProperty(targetGuid, "DEVELOPMENT_TEAM", PlayerSettings.iOS.appleDeveloperTeamID);

            File.WriteAllText(xcodeProjPath, xcodeProj.WriteToString());
        }
    }
}
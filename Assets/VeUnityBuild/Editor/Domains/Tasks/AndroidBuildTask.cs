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

            // The exportFormat only changes the output file extension. EditorUserBuildSettings.buildAppBundle
            // must also be set, otherwise Unity produces an APK even when the file is named ".aab".
            EditorUserBuildSettings.buildAppBundle = _buildConfig.exportFormat == AndroidExportFormat.Aab;

            // Unity's -androidKeystorePass / -androidKeyaliasPass are ignored when building via
            // BuildPipeline.BuildPlayer, so apply VeUnityBuild's own options to PlayerSettings here.
            if (!string.IsNullOrEmpty(_buildParameter.AndroidKeystorePass))
            {
                PlayerSettings.Android.keystorePass = _buildParameter.AndroidKeystorePass;
            }

            if (!string.IsNullOrEmpty(_buildParameter.AndroidKeyaliasPass))
            {
                PlayerSettings.Android.keyaliasPass = _buildParameter.AndroidKeyaliasPass;
            }

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

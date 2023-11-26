using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;

namespace VeUnityBuild.Editor.Contexts
{
    public class IOSBuildConfig : ScriptableObject, IContextObject
    {
        public CommonBuildConfig commonConfig;

        public IOSDetailConfig debugConfig = new(buildOptions: BuildOptions.Development,
            profileType: ProvisioningProfileType.Development);

        public IOSDetailConfig releaseConfig =
            new(buildOptions: BuildOptions.None, profileType: ProvisioningProfileType.Distribution);

        [MenuItem("Tools/VeUnityBuild/CreateBuildConfig/iOS")]
        public static void Create()
        {
            var dir = Path.GetDirectoryName(Constant.IOSBuildConfigPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var asset = CreateInstance<IOSBuildConfig>();
            AssetDatabase.CreateAsset(asset, Constant.IOSBuildConfigPath);
            AssetDatabase.Refresh();
        }
    }

    [Serializable]
    public class IOSDetailConfig
    {
        public BuildOptions buildOptions;
        public string profileId;
        public ProvisioningProfileType profileType;

        public IOSDetailConfig(BuildOptions buildOptions, ProvisioningProfileType profileType)
        {
            this.buildOptions = buildOptions;
            this.profileType = profileType;
        }
    }
}
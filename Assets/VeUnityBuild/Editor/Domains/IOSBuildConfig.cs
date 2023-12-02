using UnityEditor;

namespace VeUnityBuild.Editor.Domains
{
    public class IOSBuildConfig : BuildConfig
    {
        public CommonBuildConfig commonConfig;

        public IOSDetailConfig debugConfig = new(BuildOptions.Development,
            ProvisioningProfileType.Development);

        public IOSDetailConfig releaseConfig =
            new(BuildOptions.None, ProvisioningProfileType.Distribution);
    }
}

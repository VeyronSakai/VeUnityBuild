using UnityEditor;
using VeUnityBuild.Editor.Common;

namespace VeUnityBuild.Editor.IOS
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

using UnityEditor;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;

namespace VeUnityBuild.Editor.Domains
{
    public class IOSBuildConfig : ScriptableObject, IContextObject
    {
        public CommonBuildConfig commonConfig;

        public IOSDetailConfig debugConfig = new(BuildOptions.Development,
            ProvisioningProfileType.Development);

        public IOSDetailConfig releaseConfig =
            new(BuildOptions.None, ProvisioningProfileType.Distribution);
    }
}

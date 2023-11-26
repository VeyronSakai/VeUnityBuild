using UnityEditor;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;
using VeUnityBuild.Editor.Contexts;

namespace VeUnityBuild.Editor.Domains
{
    public class IOSBuildConfig : ScriptableObject, IContextObject
    {
        public CommonBuildConfig commonConfig;

        public IOSDetailConfig debugConfig = new(buildOptions: BuildOptions.Development,
            profileType: ProvisioningProfileType.Development);

        public IOSDetailConfig releaseConfig =
            new(buildOptions: BuildOptions.None, profileType: ProvisioningProfileType.Distribution);

    }
}
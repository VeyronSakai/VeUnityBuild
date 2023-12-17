using System;
using UnityEditor;

namespace VeUnityBuild.Editor.Domains
{
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

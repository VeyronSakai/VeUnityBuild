using System;
using UnityEditor;

namespace VeUnityBuild.Editor.Domains
{
    [Serializable]
    public class AndroidBuildConfig : BuildConfig
    {
        public CommonBuildConfig commonConfig;
        public AndroidDetailConfig debugConfig = new(BuildOptions.Development);
        public AndroidDetailConfig releaseConfig = new(BuildOptions.None);
        public AndroidExportFormat exportFormat;
    }
}

using System;
using UnityEditor;
using VeUnityBuild.Editor.Common;

namespace VeUnityBuild.Editor.Android
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

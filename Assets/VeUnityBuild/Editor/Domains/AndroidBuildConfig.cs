using UnityEditor;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;

namespace VeUnityBuild.Editor.Domains
{
    public class AndroidBuildConfig : ScriptableObject, IContextObject
    {
        public CommonBuildConfig commonConfig;
        public AndroidDetailConfig debugConfig = new(buildOptions: BuildOptions.Development);
        public AndroidDetailConfig releaseConfig = new(buildOptions: BuildOptions.None);
        public AndroidExportFormat exportFormat;
    }
}
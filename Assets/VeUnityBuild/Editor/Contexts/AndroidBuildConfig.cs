using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;

namespace VeUnityBuild.Editor.Contexts
{
    public class AndroidBuildConfig : ScriptableObject, IContextObject
    {
        public CommonBuildConfig commonConfig;
        public AndroidDetailConfig debugConfig = new(buildOptions: BuildOptions.Development);
        public AndroidDetailConfig releaseConfig = new(buildOptions: BuildOptions.None);
        public AndroidExportFormat exportFormat;

        [MenuItem("Tools/VeUnityBuild/CreateBuildConfig/Android")]
        public static void Create()
        {
            var dir = Path.GetDirectoryName(Constant.AndroidBuildConfigPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var asset = CreateInstance<AndroidBuildConfig>();
            AssetDatabase.CreateAsset(asset, Constant.AndroidBuildConfigPath);
            AssetDatabase.Refresh();
        }
    }

    public enum AndroidExportFormat
    {
        Apk,
        Aab
    }

    [Serializable]
    public class AndroidDetailConfig
    {
        public BuildOptions buildOptions;

        public AndroidDetailConfig(BuildOptions buildOptions)
        {
            this.buildOptions = buildOptions;
        }
    }
}
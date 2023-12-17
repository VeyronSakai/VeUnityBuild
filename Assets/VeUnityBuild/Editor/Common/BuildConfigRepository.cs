using UnityEditor;
using UnityEngine;

namespace VeUnityBuild.Editor.Common
{
    public class BuildConfigRepository
    {
        public static void SaveBuildConfig<T>(string outputDirPath, string buildConfigName) where T : BuildConfig
        {
            var buildConfigPath = $"{outputDirPath}/{buildConfigName}";
            var buildConfigAsset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(buildConfigAsset, buildConfigPath);
            AssetDatabase.Refresh();

            Debug.Log($"Create Build Config. Path: {buildConfigPath}");
        }
    }
}

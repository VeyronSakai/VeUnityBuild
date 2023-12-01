using UnityEditor;
using UnityEngine;
using VeUnityBuild.Editor.Domains;

namespace VeUnityBuild.Editor.Infrastructures
{
    public class BuildConfigRepository
    {
        public static void Save<T>(string outputDirPath, string buildConfigName) where T : BuildConfig
        {
            var buildConfigPath = $"{outputDirPath}/{buildConfigName}";
            var buildConfigAsset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(buildConfigAsset, buildConfigPath);
            AssetDatabase.Refresh();

            Debug.Log($"Create Build Config. Path: {buildConfigPath}");
        }
    }
}

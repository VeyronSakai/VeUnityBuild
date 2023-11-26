using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;

namespace VeUnityBuild.Editor.Tasks
{
    public class LogPlatformTask : IBuildTask
    {
        public int Version => 1;

        public ReturnCode Run()
        {
            Debug.Log(EditorUserBuildSettings.activeBuildTarget);
            return ReturnCode.Success;
        }
    }
}
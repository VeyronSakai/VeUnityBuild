using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;

namespace VeUnityBuild.Editor.Common
{
    public class LogPlatformTask : IBuildTask
    {
        public int Version => Constant.Version;

        public ReturnCode Run()
        {
            Debug.Log(EditorUserBuildSettings.activeBuildTarget);
            return ReturnCode.Success;
        }
    }
}

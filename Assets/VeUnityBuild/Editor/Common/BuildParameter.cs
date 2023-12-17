using UnityEditor.Build.Pipeline.Interfaces;

namespace VeUnityBuild.Editor.Common
{
    public class BuildParameter : IContextObject
    {
        public string BuildMode { get; set; }
    }
}

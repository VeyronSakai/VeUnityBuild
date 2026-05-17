using UnityEditor.Build.Pipeline.Interfaces;

namespace VeUnityBuild.Editor.Domains
{
    public class BuildParameter : IContextObject
    {
        public string BuildMode { get; set; }
        public string AndroidKeystorePass { get; set; }
        public string AndroidKeyaliasPass { get; set; }
    }
}

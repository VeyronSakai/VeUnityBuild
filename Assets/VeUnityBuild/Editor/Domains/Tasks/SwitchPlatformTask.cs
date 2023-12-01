using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Injector;
using UnityEditor.Build.Pipeline.Interfaces;

namespace VeUnityBuild.Editor.Domains.Tasks
{
    public class SwitchPlatformTask : IBuildTask
    {
        [InjectContext(ContextUsage.In)] readonly ISwitchPlatformContext _context = null;

        public int Version => Constant.Version;

        public ReturnCode Run()
        {
            return EditorUserBuildSettings.SwitchActiveBuildTarget(_context.Group, _context.Target)
                ? ReturnCode.Success
                : ReturnCode.Error;
        }
    }

    public interface ISwitchPlatformContext : IContextObject
    {
        BuildTargetGroup Group { get; }
        BuildTarget Target { get; }
    }

    public class SwitchPlatformContext : ISwitchPlatformContext
    {
        public SwitchPlatformContext(BuildTargetGroup group, BuildTarget target)
        {
            Group = group;
            Target = target;
        }

        public BuildTargetGroup Group { get; }
        public BuildTarget Target { get; }
    }
}

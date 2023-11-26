using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Injector;
using UnityEditor.Build.Pipeline.Interfaces;

namespace VeUnityBuild.Editor.Tasks
{
    public class SwitchPlatformTask : IBuildTask
    {
        [InjectContext(ContextUsage.In)] private readonly ISwitchPlatformContext _context = null;

        public int Version => 1;

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
        public BuildTargetGroup Group { get; }
        public BuildTarget Target { get; }

        public SwitchPlatformContext(BuildTargetGroup group, BuildTarget target)
        {
            Group = group;
            Target = target;
        }
    }
}
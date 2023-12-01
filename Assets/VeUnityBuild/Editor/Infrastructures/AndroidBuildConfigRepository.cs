using UnityEngine;
using VeUnityBuild.Editor.Domains;

namespace VeUnityBuild.Editor.Infrastructures
{
    public class AndroidBuildConfigRepository
    {
        AndroidBuildConfig _buildConfig;

        public void Save(AndroidBuildConfig buildConfig)
        {
            _buildConfig = buildConfig;
        }

        public AndroidBuildConfig Find()
        {
            Debug.Assert(_buildConfig != null);
            return _buildConfig;
        }
    }
}

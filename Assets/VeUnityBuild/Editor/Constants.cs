namespace VeUnityBuild.Editor
{
    public static class Constant
    {
        private const string BuildConfigDirName = "VeUnityBuildConfig";
        public const string AndroidBuildConfigPath = "Assets/" + BuildConfigDirName + "/AndroidBuildConfig.asset";
        public const string IOSBuildConfigPath = "Assets/" + BuildConfigDirName + "/iOSBuildConfig.asset";
        public const string BuildModeDebug = "debug";
        public const string BuildModeRelease = "release";
        public const int Version = 1;
    }
}
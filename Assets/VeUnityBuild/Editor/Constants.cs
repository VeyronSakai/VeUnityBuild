namespace VeUnityBuild.Editor
{
    public static class Constant
    {
        public const string BuildConfigDirName = "VeUnityBuildConfig";
        public const string AndroidBuildConfigPath = BuildConfigDirName + "/AndroidBuildConfig.asset";
        public const string IOSBuildConfigPath = BuildConfigDirName + "/iOSBuildConfig.asset";
        public const string ObsoleteAndroidBuildConfigPath = "Assets/" + BuildConfigDirName + "/AndroidBuildConfig.asset";
        public const string BuildModeDebug = "debug";
        public const string BuildModeRelease = "release";
        public const int Version = 1;
    }
}
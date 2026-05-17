namespace VeUnityBuild.Editor
{
    public static class Constant
    {
        public const string AndroidBuildConfigName = "AndroidBuildConfig.asset";
        public const string IOSBuildConfigName = "iOSBuildConfig.asset";
        public const string BuildModeDebug = "debug";
        public const string BuildModeRelease = "release";

        public const string BuildModeOptionKey = "-buildMode";
        public const string BuildConfigOptionKey = "-buildConfig";

        // Unity's -androidKeystorePass / -androidKeyaliasPass are only honored by the Build
        // Settings flow, not by BuildPipeline.BuildPlayer invoked via -executeMethod. We reuse
        // the same option names and apply them to PlayerSettings ourselves; Unity simply ignores
        // them for this build path, so there is no conflict.
        public const string AndroidKeystorePassOptionKey = "-androidKeystorePass";
        public const string AndroidKeyaliasPassOptionKey = "-androidKeyaliasPass";

        public const int Version = 1;
    }
}

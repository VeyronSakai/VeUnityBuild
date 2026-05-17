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

        // Unity's built-in -androidKeystorePass / -androidKeyaliasPass are only honored by the
        // Build Settings flow, not by BuildPipeline.BuildPlayer invoked via -executeMethod.
        // VeUnityBuild parses its own dedicated options and applies them to PlayerSettings.
        public const string AndroidKeystorePassOptionKey = "-veAndroidKeystorePass";
        public const string AndroidKeyaliasPassOptionKey = "-veAndroidKeyaliasPass";

        public const int Version = 1;
    }
}

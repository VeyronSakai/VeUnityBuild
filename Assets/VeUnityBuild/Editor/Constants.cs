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

        // -androidKeystorePass / -androidKeyaliasPass are not part of Unity's official
        // command-line interface; they are a de-facto naming convention used by CI tooling.
        // VeUnityBuild defines its own options under the same conventional names and applies
        // them via the PlayerSettings.Android API, because Unity does not process these
        // arguments for BuildPipeline.BuildPlayer invoked via -executeMethod.
        public const string AndroidKeystorePassOptionKey = "-androidKeystorePass";
        public const string AndroidKeyaliasPassOptionKey = "-androidKeyaliasPass";

        // Overrides the build number (iOS: CFBundleVersion, Android: versionCode) for this build.
        // CI passes a value that changes on every build, because distribution services such as
        // Firebase App Distribution treat a binary with the same version and build number as
        // an update of the existing release instead of a new release.
        public const string BuildNumberOptionKey = "-buildNumber";

        public const int Version = 1;
    }
}

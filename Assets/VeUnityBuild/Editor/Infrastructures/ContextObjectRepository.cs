using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Pipeline.Interfaces;
using VeUnityBuild.Editor.Domains;
using VeUnityBuild.Editor.UseCases;

namespace VeUnityBuild.Editor.Infrastructures
{
    public class ContextObjectRepository
    {
        public static IContextObject[] GetContextObjectsFromCommandLine<T>() where T : BuildConfig
        {
            var getCommandLineArgsUseCase = new GetCommandLineArgsUseCase();
            var buildConfigPath = getCommandLineArgsUseCase.GetValue(Constant.BuildConfigOptionKey);
            if (string.IsNullOrEmpty(buildConfigPath))
            {
                throw new BuildFailedException($"BuildConfig is not found. Path: {buildConfigPath}");
            }

            var buildConfig = AssetDatabase.LoadAssetAtPath<T>(buildConfigPath);
            if (buildConfig == null)
            {
                throw new BuildFailedException($"BuildConfig is not found. Path: {buildConfigPath}");
            }

            var buildMode = getCommandLineArgsUseCase.GetValue(Constant.BuildModeOptionKey);
            var parameterContext = new BuildParameter { BuildMode = buildMode };
            return new IContextObject[] { buildConfig, parameterContext };
        }
    }
}

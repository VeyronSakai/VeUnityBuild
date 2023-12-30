# VeUnityBuild
Custom build pipeline by using scriptable build pipeline.

## Requirements

- Unity 2022.3.14f1 or later
- Scriptable Build Pipeline 1.21.21 or later

## Installation

1. Open the Package Manager from `Window` > `Package Manager`
2. "+" button > `Install package from git URL` 
3. Input `https://github.com/VeyronSakai/VeUnityBuild.git?path=Assets/VeUnityBuild` > `Install`
4. Fill in the required information in the generated config file (.asset).

## Usage

### Set Build Config

1. `window` > `VeUnityBuild` > `CreateBuildConfig`
2. Select target platform.
3. Explorer or Finder will open. Select the directory where you would like to place the configuration files.
4. 

### Execute Build from GUI

### Execute Build from CLI

```sh
/path/to/Unity \
  -quit \
  -batchmode \
  -nographics \
  -projectPath /path/to/Project \
  -buildTarget Android \
  -executeMethod VeUnityBuild.Editor.Presentations.BatchEntryPoint.Build \
  -buildMode debug \
  -buildConfig /path/to/BuildConfig # Specify a path starting with Assets. ex: Assets/Sample/VeUnityBuildConfig/AndroidBuildConfig.asset
```

## Trouble Shooting

```
Building Library\Bee\artifacts\Android\AsyncPluginsFromLinker failed with output:
UnityEditor.Build.BuildFailedException: Burst compiler (1.8.10) failed running

stdout:
Starting 1 library requests
Error: Burst internal compiler error: Burst.Compiler.IL.Aot.AotLinkerException: Burst requires the Android NDK to be correctly installed (it can be installed via the unity installer add component) in order to build a standalone player for Android with ARMV7A_NEON32
The environment variable ANDROID_NDK_ROOT is undefined or empty, is the Android NDK correctly installed?
   �ꏊ Burst.Compiler.IL.Aot.AotNativeLink.ValidateExternalToolChain(AotCompilerOptions options)
   �ꏊ Burst.Compiler.IL.Aot.AotCompiler..ctor(ExtendedCompilerBackend backend, AotCompilerOptions coreOptions)
   �ꏊ Burst.Compiler.IL.Server.LibraryCompiler.LinkAndFinalize(CompilationJob request, SharedLibraryCompilationState sharedState, Int32 methodGroupIndex, NativeCompiler nativeCompiler, AotCompilerOptions defaultOptions, AotModuleGroup aotModuleGroup)
...
```

If you encounter the above errors, try the following:

- In the case of executing a build with a GUI,
  - Check if the Android NDK is installed.
  - Check if Preferences > External Tools > Android > Android NDK path is set correctly.
  - **Uncheck the "Android NDK Installed with Unity (recommended)" checkbox.
    Then turn it back on and try to build Android.**
- In case of executing a build from the command line (including execution with CI)
  - Check if the environment variable ANDROID_NDK_ROOT is set correctly.


using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// Builds the game for the browser into Build/web. Also callable from the command line:
///   unity build Athanor --target WebGL --execute-method WebBuild.Build
/// </summary>
public static class WebBuild
{
    public const string Output = "Build/web", DevOutput = "Build/web-dev";

    [MenuItem("Athanor/Build for Web")]
    public static void Build() => Run(true);

    /// <summary>Same game, built in a fraction of the time and a little larger: for checking changes in a browser.</summary>
    [MenuItem("Athanor/Build for Web (quick)")]
    public static void BuildQuick() => Run(false);

    /// <summary>
    /// The quick build as a development build, where F9 hands the bench to the scripted player (tools/web/autoplay.mjs).
    /// It goes to a folder of its own, so it never replaces the build that is to be published.
    /// </summary>
    [MenuItem("Athanor/Build for Web (quick, with autoplay)")]
    public static void BuildDev() => Run(false, BuildOptions.Development, DevOutput);

    static void Run(bool release, BuildOptions options = BuildOptions.None, string output = Output)
    {
        var target = NamedBuildTarget.WebGL;
        PlayerSettings.SetIl2CppCodeGeneration(target, Il2CppCodeGeneration.OptimizeSize);
        PlayerSettings.SetManagedStrippingLevel(target, ManagedStrippingLevel.High);
        PlayerSettings.stripEngineCode = true;
        PlayerSettings.stripUnusedMeshComponents = true;
        PlayerSettings.WebGL.dataCaching = true;
        // Gzip with the built-in fallback loads from any static host; a host that sends Content-Encoding can switch to Brotli
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;
        PlayerSettings.WebGL.decompressionFallback = true;
        // the journal loader relies on catching the exception a damaged save throws
        PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.ExplicitlyThrownExceptionsOnly;
        PlayerSettings.WebGL.debugSymbolMode = WebGLDebugSymbolMode.Off;
        PlayerSettings.WebGL.wasm2023 = true;
        PlayerSettings.WebGL.template = "PROJECT:Athanor";
        UnityEditor.WebGL.UserBuildSettings.codeOptimization =
            release ? UnityEditor.WebGL.WasmCodeOptimization.DiskSizeLTO : UnityEditor.WebGL.WasmCodeOptimization.BuildTimes;
        AssetDatabase.SaveAssets();

        var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            scenes = new[] { "Assets/Scenes/Main.unity" }, locationPathName = output, target = BuildTarget.WebGL, options = options,
        });
        var s = report.summary;
        string line = "Web build " + s.result + ": " + (s.totalSize / 1024 / 1024) + " MB in " + (int)s.totalTime.TotalSeconds + " s, " + s.totalErrors + " errors -> " + output;
        if (s.result == BuildResult.Succeeded) Debug.Log(line);
        else { Debug.LogError(line); if (Application.isBatchMode) EditorApplication.Exit(1); }
    }
}

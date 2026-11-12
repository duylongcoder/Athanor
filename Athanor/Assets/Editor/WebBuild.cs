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
    public const string Output = "Build/web";

    [MenuItem("Athanor/Build for Web")]
    public static void Build() => Run(true);

    /// <summary>Same game, built in a fraction of the time and a little larger: for checking changes in a browser.</summary>
    [MenuItem("Athanor/Build for Web (quick)")]
    public static void BuildQuick() => Run(false);

    static void Run(bool release)
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
            scenes = new[] { "Assets/Scenes/Main.unity" }, locationPathName = Output, target = BuildTarget.WebGL, options = BuildOptions.None,
        });
        var s = report.summary;
        string line = "Web build " + s.result + ": " + (s.totalSize / 1024 / 1024) + " MB in " + (int)s.totalTime.TotalSeconds + " s, " + s.totalErrors + " errors -> " + Output;
        if (s.result == BuildResult.Succeeded) Debug.Log(line);
        else { Debug.LogError(line); if (Application.isBatchMode) EditorApplication.Exit(1); }
    }
}

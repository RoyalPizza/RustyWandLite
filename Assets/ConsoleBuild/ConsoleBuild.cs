using UnityEditor;
class ConsoleBuild
{
    static void PerformBuild()
    {
        string[] scenes = { "Assets/Scenes/NetworkTestScene.unity" };
        BuildPipeline.BuildPlayer(scenes, "C:\\RustyWand\\Build", BuildTarget.StandaloneWindows, BuildOptions.None);
    }
}
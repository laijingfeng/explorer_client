using UnityEngine;
using UnityEditor;
using System.Collections;

public class Pack : Editor
{
    public static void Build()
    {
        BuildPipeline.BuildPlayer(new string[] { "Assets/Client.unity" }, "D:/GitHub/explorer_game", BuildTarget.WebPlayer, BuildOptions.AcceptExternalModificationsToPlayer);
    }
}

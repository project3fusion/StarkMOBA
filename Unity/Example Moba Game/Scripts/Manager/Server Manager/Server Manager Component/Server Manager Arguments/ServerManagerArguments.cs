using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerManagerArguments
{
    public static string[] args;
    public static string sceneName;
    public static bool isServerStart;
    public static int serverPort;

    public static void Get()
    {
        args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "-server") isServerStart = true;
            else if (args[i] == "-scene" && args.Length > i + 1) sceneName = args[i + 1];
            else if (args[i] == "-port" && args.Length > i + 1) int.TryParse(args[i + 1], out serverPort);
    }
}
